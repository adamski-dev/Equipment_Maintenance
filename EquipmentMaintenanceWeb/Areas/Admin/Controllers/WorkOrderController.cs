using EquipmentMaintenance.DataAccess;
using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Models.ViewModels;
using EquipmentMaintenance.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Security.Claims;

namespace EquipmentMaintenanceWeb.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Super_User)]
public class WorkOrderController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _appUsers;
    private readonly IEmailSender _emailSender;
    public WorkOrderController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> appUsers, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _appUsers = appUsers;
        _emailSender = emailSender;
    }
    public IActionResult Index()
    {
        return View();
    }

    //GET
    public IActionResult Edit(int id, int equipmentId)
    {
        WorkOrderVM workOrderVM = new()
        {
            WorkOrder = new(),

            ApplicationUsers = _appUsers.Users.Select(i => new SelectListItem
            {
                Text = i.FirstName + " " + i.Surname,
                Value = i.Id.ToString()
            }),

            Equipment = _unitOfWork.Equipment.GetAll(u => u.Id == equipmentId).Select(i => new SelectListItem
            {
                Text = i.UniqueIdentifier,
                Value = i.Id.ToString()
            })
        };

        workOrderVM.WorkOrder = _unitOfWork.WorkOrder.GetFirstOrDefault(u => u.Id == id);
        return View(workOrderVM);
    }

    //GET
    public IActionResult Complete(int id, int routineId)
    {
        WorkOrderVM workOrderVM = new()
        {
            WorkOrder = new(),
        };

        workOrderVM.WorkOrder = _unitOfWork.WorkOrder.GetFirstOrDefault(u => u.Id == id);
        workOrderVM.TasksList = _unitOfWork.Tasks.GetAll(u => u.RoutineId == routineId);

        return View(workOrderVM);
    }

    //GET
    public IActionResult GenerateWorkOrders()
    {
        WorkOrderVM workOrderVM = new()
        {
            WorkOrder = new() { DateDue = DateTime.Now.AddDays(1)}
        };

        return View(workOrderVM);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(WorkOrderVM obj)
    {

        if (ModelState.IsValid)
        {
           _unitOfWork.WorkOrder.Update(obj.WorkOrder);
           TempData["success"] = "Work Order updated successfully";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Complete(WorkOrderVM obj)
    {

        if (ModelState.IsValid)
        {
            Routine routine = new();
            routine = _unitOfWork.Routine.GetFirstOrDefault(u => u.Id == obj.WorkOrder.WorkOrderRoutineId);
            routine.DateLastCompleted = DateTime.Now;
            switch (routine.IntervalId)
            {
                case 1: routine.NextDueDate = DateTime.Now.AddDays(routine.Frequency); break;
                case 2: routine.NextDueDate = DateTime.Now.AddDays(7 * routine.Frequency); break;
                case 3: routine.NextDueDate = DateTime.Now.AddMonths(routine.Frequency); break;
                case 4: routine.NextDueDate = DateTime.Now.AddYears(routine.Frequency); break;
            }

            _unitOfWork.Routine.Update(routine);

            obj.WorkOrder.DateCompleted = DateTime.Now;
            _unitOfWork.WorkOrder.Update(obj.WorkOrder);

            TempData["success"] = "Work Order completed successfully";

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GenerateWorkOrders(WorkOrderVM obj)
    {

        if (obj.WorkOrder.DateDue > DateTime.Now)
        {
            var routineList = _unitOfWork.Routine.GetAll(u => u.NextDueDate < obj.WorkOrder.DateDue);

            if (routineList.Count() > 0)
            {
                var listOfOpenWorkOrders = _unitOfWork.WorkOrder.GetAll(u => u.CompletedFlag == false);
                var listOfNewWorkOrders = new List<WorkOrder>();
                int workOrderIndex = getWorkOrderIndex();

                foreach (var routine in routineList)
                {
                    var workOrderOpen = listOfOpenWorkOrders.ToList().Exists(x => x.WorkOrderRoutineId == routine.Id) ? true : false;

                    if (!workOrderOpen)
                    {
                        WorkOrder newWorkOrder = new();

                        newWorkOrder.WorkOrderNumber = DateTime.Now.Year.ToString() + "/" + getThisWeekNumber().ToString() + "/" + workOrderIndex.ToString();
                        newWorkOrder.WorkOrderRoutineId = routine.Id;
                        newWorkOrder.EquipmentId = routine.EquipmentId;
                        newWorkOrder.JobDescription = routine.RoutineDescription;
                        newWorkOrder.ApplicationUserId = routine.ApplicationUserId;
                        newWorkOrder.DateDue = routine.NextDueDate;
                        newWorkOrder.DateOverdue = routine.NextDueDate.AddDays(routine.DaysToOverdue);
                        newWorkOrder.WeekNumber = getThisWeekNumber();

                        listOfNewWorkOrders.Add(newWorkOrder);
                        workOrderIndex++;
                    }
                }

                if (listOfNewWorkOrders.Count > 0)
                {
                    _unitOfWork.WorkOrder.AddRange(listOfNewWorkOrders);
                    TempData["success"] = "Work Orders created successfully";
                    _unitOfWork.Save();

                    foreach(var newWorkOrder in listOfNewWorkOrders)
                    {
                        var workOrder = _unitOfWork.WorkOrder.GetFirstOrDefault(u => u.WorkOrderNumber == newWorkOrder.WorkOrderNumber, includeProperties: "ApplicationUser");
                        _emailSender.SendEmailAsync(workOrder.ApplicationUser.Email
                                                    , "New PM - Equipment Maintenance"
                                                    , "<p>A new PM Work Order: " + workOrder.WorkOrderNumber
                                                    + " has been generated for you.</p>");
                    }
                    return RedirectToAction("Index");

                } else { TempData["warning"] = "No work orders created, check in open work workders if there are any not completed"; }


            } else { TempData["info"] = "No routines are due before the date selected"; }
        }

        return View(obj);
    }

    private int getThisWeekNumber()
    {
        CultureInfo date = CultureInfo.CurrentCulture;
        int weekNumber = date.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        return weekNumber;
    }

    private int getWorkOrderIndex()
    {

        if(_unitOfWork.WorkOrder.GetAll().Any<WorkOrder>() == false)
        {
            return 1;
        }

        var lastWorkOrderElement = _unitOfWork.WorkOrder.GetAll().Last();
        var thisWeek = getThisWeekNumber();

        if (lastWorkOrderElement.WeekNumber != thisWeek)
        {
            return 1;
        }
        else
        {
            int startIndex = thisWeek < 10 ? 7 : 8;
            int length = lastWorkOrderElement.WorkOrderNumber.Length - startIndex;
            String stringIndex = (lastWorkOrderElement.WorkOrderNumber).Substring(startIndex, length);

            return Int32.Parse(stringIndex) + 1; 
        }
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var workOrderList = _unitOfWork.WorkOrder.GetAll(includeProperties: "Equipment,ApplicationUser");
        return Json(new { data = workOrderList });
    }

    #endregion
}