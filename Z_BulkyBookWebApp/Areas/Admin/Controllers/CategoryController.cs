﻿using Microsoft.AspNetCore.Mvc;
using Z_BulkyBook.DataAccess.Repository.IRepository;
using Z_BulkyBook.Models;

namespace Z_BulkyBookWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //ctor
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //this is for create
                return View(category);
            }

            //this is for edit
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                //TempData["Error"] = "Error deleting Category";
                return Json(new { success = false, message = "Error while deleting !" });
            }

            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();

            //TempData["Success"] = "Category successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
        //
    }
}
