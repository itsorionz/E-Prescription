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
    }
}