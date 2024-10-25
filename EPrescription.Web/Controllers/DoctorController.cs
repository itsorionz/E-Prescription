using EPrescription.Entities;
using EPrescription.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View(new DoctorModel());
        }
        public ActionResult AddOrEdit()
        {
            return View(new DoctorModel().GetFirstDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(Doctor doctor)
        {

            new DoctorModel().AddOrEdit(doctor);
            return RedirectToAction("Index");
        }
    }
}