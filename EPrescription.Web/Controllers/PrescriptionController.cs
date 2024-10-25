using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class PrescriptionController : Controller
    {
        // GET: Prescription
        public ActionResult Index(PatientViewModel model)
        {
            model.PatientIPagedList = new PatientModel().GetAllIPagedList(model.Page, model.PageSize, model.Name);
            return View(model);
        }
        public ActionResult Add(int patientId)
        {
            return View(new PrescriptionModel(patientId));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PrescriptionModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(new PrescriptionModel(id));
        }
    }
}