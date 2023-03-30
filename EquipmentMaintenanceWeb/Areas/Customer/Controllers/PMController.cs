using EquipmentMaintenance.DataAccess;
using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Models.ViewModels;
using EquipmentMaintenance.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Security.Claims;

namespace EquipmentMaintenanceWeb.Controllers;

[Area("Customer")]
[Authorize]
public class PMController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public PMController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public IActionResult Index()
    {
        if (User.IsInRole(SD.Role_User))
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var workOrderList = _unitOfWork.WorkOrder.GetAll(u => u.ApplicationUserId == claim.Value);

            HttpContext.Session.SetInt32(SD.SessionOpenPM,
                workOrderList.ToList().FindAll(i => i.CompletedFlag == false).Count());
        }
        return View();
    }

    //GET
    public IActionResult Complete(int id, int routineId)
    {
        PmVM pmVM = new();
        pmVM.WorkOrder = _unitOfWork.WorkOrder.GetFirstOrDefault(u => u.Id == id);
        pmVM.TasksList = _unitOfWork.Tasks.GetAll(u => u.RoutineId == routineId);

        return View(pmVM);
    }

    //GET
    public IActionResult CreateUnplannedWorkOrder()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        PmVM pmVM = new()
        {
            WorkOrder = new()
            {
                ApplicationUserId = claim.Value,
                WorkOrderNumber = DateTime.Now.Year.ToString() + "/" + getThisWeekNumber().ToString() + "/" + getWorkOrderIndex().ToString(),
                WeekNumber = getThisWeekNumber(),
                DateDue = DateTime.Now,
                DateCompleted = DateTime.Now,
                UnplannedFlag = true,
                CompletedFlag = true,

             },

            Equipment = _unitOfWork.Equipment.GetAll().Select(i => new SelectListItem
            {
                Text = i.UniqueIdentifier + " (" + i.Description + ")",
                Value = i.Id.ToString()
            })
        };

        return View(pmVM);
    }

    private int getThisWeekNumber()
    {
        CultureInfo date = CultureInfo.CurrentCulture;
        int weekNumber = date.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        return weekNumber;
    }

    private int getWorkOrderIndex()
    {
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

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Complete(PmVM obj)
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
            _unitOfWork.Save();

            if(HttpContext.Session.GetInt32(SD.SessionOpenPM) != null)
            {
                int sessionValue = (int)(HttpContext.Session.GetInt32(SD.SessionOpenPM) - 1);
                HttpContext.Session.SetInt32(SD.SessionOpenPM, sessionValue);
            }
            
            TempData["success"] = "Work Order completed successfully";
            
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateUnplannedWorkOrder(PmVM obj)
    {

        if (ModelState.IsValid)
        {
            _unitOfWork.WorkOrder.Add(obj.WorkOrder);
            TempData["success"] = "Unplanned work logged successfully";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var workOrderList = _unitOfWork.WorkOrder.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Equipment,ApplicationUser");
        return Json(new { data = workOrderList });
    }
    #endregion
}