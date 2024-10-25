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
    }
}