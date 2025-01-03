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
        public ActionResult Inactive(PatientModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}