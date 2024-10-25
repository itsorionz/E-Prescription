using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    public class ClinicController : Controller
    {
        // GET: Clinic
        public ActionResult Index()
        {
            return View(new ClinicModel().GetAllClinics());
        }
        public ActionResult Add()
        {
            return View(new ClinicModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ClinicModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
    }
}