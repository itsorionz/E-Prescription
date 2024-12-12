using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class GenericNameController : Controller
    {
        // GET: GenericName
        public ActionResult Index()
        {
            return View(new GenericNameModel().GetAllGenericNames());
        }
        public ActionResult Add(GenericNameModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }

        public JsonResult IsGenericTypeExist(string genericType, string InitialGenericType)
        {
            bool isNotExist = new GenericNameModel().IsGenericTypeExist(genericType, InitialGenericType);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadGeneric(int id)
        {
            var model = new GenericNameModel(id);
            return Json(new { GenericType = model.TypeName, Id = model.Id });
        }
        public ActionResult Edit(GenericNameModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public ActionResult Inactive(GenericNameModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}