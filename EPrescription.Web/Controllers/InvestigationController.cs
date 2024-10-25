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
    }
}