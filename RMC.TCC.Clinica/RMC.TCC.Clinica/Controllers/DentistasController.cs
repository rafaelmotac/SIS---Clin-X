using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RMC.TCC.Clinica.Models;

namespace RMC.TCC.Clinica.Controllers
{
    public class DentistasController : Controller
    {
        private ClinicaDb db = new ClinicaDb();

        // GET: Dentistas
        public ActionResult Index()
        {
            var dentista = db.Dentista.Include(d => d.ProfSaude);
            return View(dentista.ToList());
        }

        // GET: Dentistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dentista dentista = db.Dentista.Find(id);
            if (dentista == null)
            {
                return HttpNotFound();
            }
            return View(dentista);
        }

        // GET: Dentistas/Create
        public ActionResult Create()
        {
            ViewBag.profSaude_IdProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome");
            return View();
        }

        // POST: Dentistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cro,profSaude_IdProfSaude")] Dentista dentista)
        {
            if (ModelState.IsValid)
            {
                db.Dentista.Add(dentista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.profSaude_IdProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", dentista.profSaude_IdProfSaude);
            return View(dentista);
        }

        // GET: Dentistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dentista dentista = db.Dentista.Find(id);
            if (dentista == null)
            {
                return HttpNotFound();
            }
            ViewBag.profSaude_IdProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", dentista.profSaude_IdProfSaude);
            return View(dentista);
        }

        // POST: Dentistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cro,profSaude_IdProfSaude")] Dentista dentista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dentista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.profSaude_IdProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", dentista.profSaude_IdProfSaude);
            return View(dentista);
        }

        // GET: Dentistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dentista dentista = db.Dentista.Find(id);
            if (dentista == null)
            {
                return HttpNotFound();
            }
            return View(dentista);
        }

        // POST: Dentistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dentista dentista = db.Dentista.Find(id);
            db.Dentista.Remove(dentista);
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
