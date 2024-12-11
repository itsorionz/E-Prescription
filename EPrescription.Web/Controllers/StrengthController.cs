using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class StrengthController : Controller
    {
        // GET: Strength
        public ActionResult Index()
        {
            return View(new StrengthModel().GetAllStrengths());
        }
        public ActionResult Add(StrengthModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public JsonResult IsStrengthNameExist(string StrengthName, string InitialStrengthName)
        {
            bool isNotExist = new ComplaintModel().IsComplaintTypeExist(StrengthName, InitialStrengthName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadStrength(int id)
        {
            var model = new StrengthModel(id);
            return Json(new { StrengthName = model.StrengthName, Id = model.Id });
        }
        public ActionResult Edit(StrengthModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public ActionResult Inactive(StrengthModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}