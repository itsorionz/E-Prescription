using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class MedicineManufacturerController : Controller
    {
        // GET: MedicineManufacturer
        public ActionResult Index()
        {
            return View(new MedicineManufacturerModel().GetAllMedicineManufacturers()) ;
        }
        public ActionResult Add(MedicineManufacturerModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public JsonResult IsCompanyNameExist(string companyName, string InitialCompanyName)
        {
            bool isNotExist = new MedicineManufacturerModel().IsCompanyNameExist(companyName, InitialCompanyName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadManufacturer(int id)
        {
            var model = new MedicineManufacturerModel(id);
            return Json(new { CompanyName = model.CompanyName, Id = model.Id, Address = model.Address, Email = model.Email, ContactNumber = model.ContactNumber });
        }
        public ActionResult Edit(MedicineManufacturerModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public ActionResult Inactive(MedicineManufacturerModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}