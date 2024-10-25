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
    }
}