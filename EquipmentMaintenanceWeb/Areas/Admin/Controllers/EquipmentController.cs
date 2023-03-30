using EquipmentMaintenance.DataAccess;
using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Models.ViewModels;
using EquipmentMaintenance.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EquipmentMaintenanceWeb.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Super_User)]
public class EquipmentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public EquipmentController (IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        return View();
    }

    //GET
    public IActionResult Upsert(int? id)
    {
        EquipmentVM equipmentVM = new()
        {
            Equipment = new(),

            EquipmentTypeList = _unitOfWork.EquipmentType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            LocationList = _unitOfWork.Location.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };

        if (id == null || id == 0)
        {
            return View(equipmentVM);
        }
        else
        {
            equipmentVM.Equipment = _unitOfWork.Equipment.GetFirstOrDefault(u => u.Id == id);
            return View(equipmentVM);
        }
        return View(equipmentVM);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(EquipmentVM obj)
    {

        if (ModelState.IsValid)
        {
            if(obj.Equipment.Id == 0)
            {
                _unitOfWork.Equipment.Add(obj.Equipment);
                TempData["success"] = "Equipment added successfully";
            }
            else
            {
                _unitOfWork.Equipment.Update(obj.Equipment);
                TempData["success"] = "Equipment updated successfully";
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
        var equipmentList = _unitOfWork.Equipment.GetAll(includeProperties: "EquipmentType,Location");
        return Json(new { data = equipmentList });
    }

    //POST
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Equipment.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        
        _unitOfWork.Equipment.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });

    }
    #endregion
}
