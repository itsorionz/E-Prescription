using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class InvestigationController : Controller
    {
        // GET: Investigation
        public ActionResult Index()
        {
            return View(new InvestigationModel().GetAllInvestigations());
        }
        public ActionResult Add(InvestigationModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public JsonResult IsInvestigationNameExist(string InvestigationName, string InitialInvestigationName)
        {
            bool isNotExist = new InvestigationModel().IsInvestigationNameExist(InvestigationName, InitialInvestigationName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadInvestigation(int id)
        {
            var model = new InvestigationModel(id);
            return Json(new { InvestigationName = model.InvestigationName, Id = model.Id });
        }
        public ActionResult Edit(InvestigationModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public ActionResult Inactive(InvestigationModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}