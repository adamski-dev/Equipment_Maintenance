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
    public class RoutineIntervalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoutineIntervalController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<RoutineInterval> objIntervalsList = _unitOfWork.Interval.GetAll();
            return View(objIntervalsList);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoutineInterval obj)
        {
            IEnumerable<RoutineInterval> intervalsFromDb = _unitOfWork.Interval.GetAll();
            foreach (var interval in intervalsFromDb)
            {
                if (interval.Interval == obj.Interval)
                {
                    ModelState.AddModelError("Name", "This interval already exists, please type a different interval");
                }
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Interval.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Interval added successfully";
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

            var intervalFromDb = _unitOfWork.Interval.GetFirstOrDefault(u => u.Id == id);

            if(intervalFromDb == null)
            {
                return NotFound();
            }

            return View(intervalFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoutineInterval obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Interval.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Interval updated successfully";
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
            
            var intervalFromDb = _unitOfWork.Interval.GetFirstOrDefault(u => u.Id == id);

            if (intervalFromDb == null)
            {
                return NotFound();
            }
            return View(intervalFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Interval.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Interval.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Interval deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
