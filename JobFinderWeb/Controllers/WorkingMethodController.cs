using JobFinder.DataAccess.Repository.IRepository;
using JobFinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobFinderWeb.Controllers
{
    public class WorkingMethodController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkingMethodController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<WorkingMethod> workingMethods = _unitOfWork.WorkingMethod.GetAll();
            return View(workingMethods);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WorkingMethod obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.WorkingMethod.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var workingMethod = _unitOfWork.WorkingMethod.GetFirstOrDefault(w => w.Id == id);

            if(workingMethod == null)
            {
                return NotFound();
            }

            return View(workingMethod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WorkingMethod workingMethod)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.WorkingMethod.Update(workingMethod);
                _unitOfWork.Save();
                TempData["success"] = "Working Method Updated";
                return RedirectToAction("Index");
            }
            return View(workingMethod);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var workingMethod = _unitOfWork.WorkingMethod.GetFirstOrDefault(w => w.Id == id);

            if(workingMethod == null)
            {
                return NotFound();
            }

            return View(workingMethod);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var workingMethod = _unitOfWork.WorkingMethod.GetFirstOrDefault(c => c.Id == id);

            if (workingMethod == null)
            {
                return NotFound();
            }

            _unitOfWork.WorkingMethod.Remove(workingMethod);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted";
            return RedirectToAction("Index");
        }
    }
}
