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
                jobVM.Job = _unitOfWork.Job.GetFirstOrDefault(j => j.Id == id);
                return View(jobVM);
            }

            return View(jobVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(JobVM obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Job.Id == 0)
                {
                    _unitOfWork.Job.Add(obj.Job);
                    _unitOfWork.Save();
                    TempData["success"] = "New Job Created";
                }
                else
                {
                    _unitOfWork.Job.Update(obj.Job);
                    _unitOfWork.Save();
                    TempData["success"] = "Job Updated";
                }
                return RedirectToAction("Index");
            }
            return View(obj);
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

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var job = _unitOfWork.Job.GetFirstOrDefault(j => j.Id == id);

            if (job == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }

            _unitOfWork.Job.Remove(job);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = "Job Successfully Deleted"
            });
        }
        #endregion
    }
}
