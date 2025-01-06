using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
   
{
    [Authorize]
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index(PatientViewModel model)
        {
            model.PatientIPagedList = new PatientModel().GetAllIPagedList(model.Page, model.PageSize, model.Name);
            return View(model);
        }
        public ActionResult Add()
        {
            return View(new PatientModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PatientModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var patientModel = new PatientModel().Edit(id);
            if (patientModel == null)
            {
                return HttpNotFound();
            }
            return View(patientModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientModel model)
        {
            if (ModelState.IsValid)
            {
                model.Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Inactive(PatientModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}