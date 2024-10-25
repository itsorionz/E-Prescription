using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class DiseaseController : Controller
    {
        // GET: Disease
        public ActionResult Index()
        {
            return View(new DiseaseModel().GetAllDiseases());
        }
        public ActionResult Add(DiseaseModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(DiseaseModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public JsonResult IsDiseaseNameExist(string DiseaseName, string InitialDiseaseName)
        {
            bool isNotExist = new DiseaseModel().IsDiseaseNameExist(DiseaseName, InitialDiseaseName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadDisease(int id)
        {
            var model = new DiseaseModel(id);
            return Json(new { DiseaseName = model.DiseaseName, Id = model.Id });
        }
    }
}