using EquipmentMaintenance.DataAccess;
using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace EquipmentMaintenanceWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Super_User)]
    public class EquipmentTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentTypeController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<EquipmentType> objEquipmentTypeList = _unitOfWork.EquipmentType.GetAll();
            return View(objEquipmentTypeList);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EquipmentType obj)
        {
            IEnumerable<EquipmentType> equipmentTypeListFromDb = _unitOfWork.EquipmentType.GetAll();
            foreach (var existingName in equipmentTypeListFromDb)
            {
                if (existingName.Name == obj.Name)
                {
                    ModelState.AddModelError("Name", "This type already exists, please type a different name");
                }
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.EquipmentType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Equipment type created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {   
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var equipmentTypeFromDb = _unitOfWork.EquipmentType.GetFirstOrDefault(u => u.Id == id);

            if(equipmentTypeFromDb == null)
            {
                return NotFound();
            }

            return View(equipmentTypeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EquipmentType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.EquipmentType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Equipment type updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var equipmentTypeFromDb = _unitOfWork.EquipmentType.GetFirstOrDefault(u => u.Id == id);

            if (equipmentTypeFromDb == null)
            {
                return NotFound();
            }
            return View(equipmentTypeFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.EquipmentType.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.EquipmentType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Equipment type deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
