using EquipmentMaintenance.DataAccess;
using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Models.ViewModels;
using EquipmentMaintenance.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EquipmentMaintenanceWeb.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Super_User)]
public class RoutineController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _appUsers;
    public RoutineController (IUnitOfWork unitOfWork, UserManager<ApplicationUser> appUsers)
    {
        _unitOfWork = unitOfWork;
        _appUsers = appUsers;
    }
    public IActionResult Index()
    {
        return View();
    }

    //GET
    public IActionResult Upsert(int? id)
    {

        RoutineVM routineVM = new()
        {
            Routine = new() {},


            EquipmentList = _unitOfWork.Equipment.GetAll(u => u.InServiceStatus == true).Select(i => new SelectListItem
            {
                Text = i.UniqueIdentifier + " (" + i.Description + ")",
                Value = i.Id.ToString()
            }),
            ApplicationUsers = _appUsers.Users.Select(i => new SelectListItem
            {
                Text = i.FirstName + " " + i.Surname,
                Value = i.Id.ToString()
            }),
            Intervals = _unitOfWork.Interval.GetAll().Select(i => new SelectListItem
            {
                Text = i.Interval,
                Value = i.Id.ToString()
            })
            
        };

        if (id == null || id == 0)
        {
            return View(routineVM);
        }
        else
        {
            routineVM.Routine = _unitOfWork.Routine.GetFirstOrDefault(u => u.Id == id);
            routineVM.TasksList = _unitOfWork.Tasks.GetAll(u => u.RoutineId == id);
            return View(routineVM);
        }
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(RoutineVM obj)
    {

        if (ModelState.IsValid)
        {
            if (obj.Routine.Id == 0)
            {
                _unitOfWork.Tasks.AddRange(obj.Routine.ListOfTasks);
                _unitOfWork.Routine.Add(obj.Routine);
                TempData["success"] = "Routine created successfully";
            }
            else
            {
                foreach (var task in obj.Routine.ListOfTasks)
                {
                    if (task.Id == 0)
                    {
                        _unitOfWork.Tasks.Add(task);
                    }
                    else
                    {
                       _unitOfWork.Tasks.Update(task);
                    }
                }
                _unitOfWork.Routine.Update(obj.Routine);
                TempData["success"] = "Routine updated successfully";
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    #region API CALLS
    [HttpGet]                                     
    public IActionResult GetAll()
    {
        var routineList = _unitOfWork.Routine.GetAll(includeProperties: "Equipment,ApplicationUser,RoutineInterval");
        return Json(new { data = routineList });
    }

    //POST
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Routine.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        
        _unitOfWork.Routine.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Routine deleted" });

    }

    //POST
    [HttpDelete]
    public IActionResult DeleteTask(int? id)
    {
        var obj = _unitOfWork.Tasks.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        _unitOfWork.Tasks.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Selected task deleted" });
    }
    #endregion
}