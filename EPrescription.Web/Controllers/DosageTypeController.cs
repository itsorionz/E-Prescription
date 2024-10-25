using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class DosageTypeController : Controller
    {
        // GET: DosageType
        public ActionResult Index()
        {
            return View(new DosageTypeModel().GetDosageTypes());
        }
        public ActionResult Add(DosageTypeModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(DosageTypeModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public JsonResult IsDosageTypeExist(string TypeName, string InitialTypeName)
        {
            bool isNotExist = new DosageTypeModel().IsDosageTypeExist(TypeName, InitialTypeName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadDosageType(int id)
        {
            var model = new DosageTypeModel(id);
            return Json(new { TypeName = model.TypeName, Id = model.Id });
        }
    }
}