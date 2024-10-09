using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles ="admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.Categories.GetAll());
        }

        [HttpGet]
        public ActionResult Upsert(int? id)
        {
            if (id == null)
            {
                Category category = null;
                return View(category);
            }
            else
            {
                return View(_unitOfWork.Categories.GetByID(id.Value));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(int? id, Category category)
        {
            if (id == null)
            {
                if (ModelState.IsValid && !_unitOfWork.Categories.IsExist(category.Name))
                {
                    _unitOfWork.Categories.Add(category);
                    _unitOfWork.ApplyChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Name", "Name Was Added Before");
                return View(category);
            }
            else
            {
                var item = _unitOfWork.Categories.GetByID(id.Value);
                item.Name = category.Name;
                _unitOfWork.ApplyChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_unitOfWork.Categories.GetByID(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category category)
        {
            if(_unitOfWork.Categories.Delete(category.Id)>0)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
