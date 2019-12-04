using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientCard.Model;
using PatientCard.Web.ViewModels;

namespace PatientCard.Web.Controllers
{
    public class PatientsController : Controller
    {
        private PatiendCardContext db = new PatiendCardContext();

        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var jvm = new JournalViewModel
            {
                Patient = new PatientViewModel
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Iin = patient.Iin
                },
            };

            ViewBag.Journals = db.Journals.Where(j => j.Patient.Id == id).ToList();

            return View(jvm);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Signup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var jvm = new JournalViewModel
            {
                Patient = new PatientViewModel 
                { 
                    Id=patient.Id,
                    Name=patient.Name,
                    Iin=patient.Iin
                },
            };
            return View(jvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(JournalViewModel journalViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = db.Patients.Find(journalViewModel.Patient.Id);
                //Patient patient = new Patient()
                //{
                //    Name=journalViewModel.Patient.Name,
                //    Id=journalViewModel.Patient.Id,
                //    Iin=journalViewModel.Patient.Iin
                //};
                var newJournal = new Journal()
                {
                   Diagnosis=journalViewModel.Diagnosis,
                   DateVisit=journalViewModel.DateVisit,
                   Patient = patient
                };
                db.Journals.Add(newJournal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(journalViewModel);
        }



        // POST: Patients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Iin")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Iin")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
