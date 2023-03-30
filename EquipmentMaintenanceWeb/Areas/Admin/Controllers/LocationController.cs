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
    public class LocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Location> objLocationList = _unitOfWork.Location.GetAll();
            return View(objLocationList);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location obj)
        {
            IEnumerable<Location> locationListFromDb = _unitOfWork.Location.GetAll();
            foreach (var location in locationListFromDb)
            {
                if (location.Name == obj.Name)
                {
                    ModelState.AddModelError("Name", "This location already exists, please type a different location");
                }
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Location.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Location created successfully";
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

            var locationFromDb = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);

            if(locationFromDb == null)
            {
                return NotFound();
            }

            return View(locationFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Location obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Location.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Location updated successfully";
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
            
            var locationFromDb = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);

            if (locationFromDb == null)
            {
                return NotFound();
            }
            return View(locationFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Location.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Equipment type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
