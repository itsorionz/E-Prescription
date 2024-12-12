using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class MedicineController : Controller
    {
        // GET: Medicine
        public ActionResult Index(MedicineViewModel model)
        {
            model.MedicinePagedList = new MedicineModel().GetAllIPagedMedicine(model.Page, model.PageSize, model.Name, model.CompanyId, model.GenericName);
            return View(model);
        }
        public ActionResult Add()
        {
            return View(new MedicineModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MedicineModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase ExcelFile)
        {  
            string msg= new MedicineModel().UploadExcel(ExcelFile);
            ModelState.AddModelError("", msg);
            return View();
        }

        public JsonResult LoadMedicineName(string name)
        {
            var list = new MedicineModel().GetMedicineNameByStr(name);
            return Json(new { list },JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadAvailablity(string medicineName)
        {
            var list = new MedicineModel().GetAvailablity(medicineName);
            return Json(new { list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadConfig()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadConfig(HttpPostedFileBase ExcelFile)
        {
            new MedicineModel().UploadConfigExcel(ExcelFile);
            return View();
        }
        public ActionResult Inactive(MedicineModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}