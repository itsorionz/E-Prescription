using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class ComplaintController : Controller
    {
        // GET: Complaint
        public ActionResult Index()
        {
            return View(new ComplaintModel().GetAllComplaints());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ComplaintModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public JsonResult IsComplaintTypeExist(string ComplaintType, string InitialComplaintType)
        {
            bool isNotExist = new ComplaintModel().IsComplaintTypeExist(ComplaintType, InitialComplaintType);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadComplaint(int id)
        {
            var model = new ComplaintModel(id);
            return Json(new { ComplaintType = model.ComplaintType, Id = model.Id });
        }
        public ActionResult Edit(ComplaintModel model)
        {
            model.Edit();
            return RedirectToAction("Index");
        }
        public ActionResult Inactive(ComplaintModel model)
        {
            model.Inactive();
            return RedirectToAction("Index");
        }
    }
}