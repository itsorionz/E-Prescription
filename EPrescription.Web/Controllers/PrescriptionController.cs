using EPrescription.Web.Models;
using PdfSharp.Pdf;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.IO;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class PrescriptionController : Controller
    {
        // GET: Prescription
        public ActionResult Index(PatientViewModel model)
        {
            model.PatientIPagedList = new PatientModel().GetAllIPagedList(model.Page, model.PageSize, model.Name);
            return View(model);
        }
        public ActionResult Add(int patientId)
        {
            return View(new PrescriptionModel(patientId));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PrescriptionModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(new PrescriptionModel(id));
        }

        //public ActionResult ExportToPdf(int patientId)
        //{
        //    var model = new PrescriptionModel(patientId);
        //    string html = model.GenerateHtmlPrescription(patientId);
        //    PdfDocument pdf = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        pdf.Save(stream, false); // Save PDF to the stream
        //        stream.Position = 0;
        //        return File(stream.ToArray(), "application/pdf", "Prescription.pdf");
        //    }
        //}
    }
}