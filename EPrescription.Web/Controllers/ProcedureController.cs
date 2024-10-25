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
    }
}