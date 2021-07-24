using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z_BulkyBook.DataAccess.Repository.IRepository;
using Z_BulkyBook.Models;
using Z_BulkyBook.Utility;

namespace Z_BulkyBookWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //ctor
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                //this is for create
                return View(coverType);
            }

            //this is for edit
            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);

                if (coverType.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter);
                    //_unitOfWork.CoverType.Add(coverType);
                }
                else
                {
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter);

                    //_unitOfWork.CoverType.Update(coverType);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //var allObj = _unitOfWork.CoverType.GetAll();
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);

            //var objFromDb = _unitOfWork.CoverType.Get(id);

            if (objFromDb == null)
            {
                //TempData["Error"] = "Error deleting CoverType";
                return Json(new { success = false, message = "Error while deleting !" });
            }

            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
            //_unitOfWork.CoverType.Remove(objFromDb);
            _unitOfWork.Save();

            //TempData["Success"] = "CoverType successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
        //
    }
}
