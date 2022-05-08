using JobFinder.DataAccess;
using JobFinder.DataAccess.Repository.IRepository;
using JobFinder.Models;
using JobFinder.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobFinderWeb.Controllers
{
    [Area("Admin")]
    public class JobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Job> jobs = _unitOfWork.Job.GetAll();
            return View(jobs);
        }

        public IActionResult Upsert(int? id)
        {
            JobVM jobVM = new()
            {
                Job = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                WorkingMethodList = _unitOfWork.WorkingMethod.GetAll().Select(w => new SelectListItem
                {
                    Text = w.Name,
                    Value = w.Id.ToString()
                })
            };

            if(id == null || id == 0)
            {
                // create job
                return View(jobVM);
            }
            else
            {
                // update job
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(JobVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Job.Add(obj.Job);
                _unitOfWork.Save();
                TempData["success"] = "New Job Created";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if(category == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobs = _unitOfWork.Job.GetAll(includeProperties: "Category,WorkingMethod");
            return Json(new
            {
                data = jobs
            });
        }
        #endregion
    }
}
