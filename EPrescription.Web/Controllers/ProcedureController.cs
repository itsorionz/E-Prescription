using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class ProcedureController : Controller
    {
        // GET: Procedure
        public ActionResult Index()
        {
            return View(new ProcedureModel().GetAllProcedures());
        }
        public ActionResult Add(ProcedureModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public JsonResult IsProcedureNameExist(string ProcedureName, string InitialProcedureNmae)
        {
            bool isNotExist = new ProcedureModel().IsProcedureNameExist(ProcedureName, InitialProcedureNmae);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadProcedure(int id)
        {
            var model = new ProcedureModel(id);
            return Json(new { ProcedureName = model.ProcedureName, Id = model.Id });
        }
        public ActionResult Edit(ProcedureModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public ActionResult Inactive(ProcedureModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}