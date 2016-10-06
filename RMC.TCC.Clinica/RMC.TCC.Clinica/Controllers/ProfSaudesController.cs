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
    public class ProfSaudesController : Controller
    {
        private ClinicaDb db = new ClinicaDb();

        // GET: ProfSaudes
        public ActionResult Index()
        {
            return View(db.ProfSaude.ToList());
        }

        // GET: ProfSaudes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfSaude profSaude = db.ProfSaude.Find(id);
            if (profSaude == null)
            {
                return HttpNotFound();
            }
            return View(profSaude);
        }

        // GET: ProfSaudes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfSaudes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProfSaude,dtNascimento,nome,cpf,telefone,endereco")] ProfSaude profSaude)
        {
            if (ModelState.IsValid)
            {
                db.ProfSaude.Add(profSaude);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profSaude);
        }

        // GET: ProfSaudes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfSaude profSaude = db.ProfSaude.Find(id);
            if (profSaude == null)
            {
                return HttpNotFound();
            }
            return View(profSaude);
        }

        // POST: ProfSaudes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProfSaude,dtNascimento,nome,cpf,telefone,endereco")] ProfSaude profSaude)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profSaude).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profSaude);
        }

        // GET: ProfSaudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfSaude profSaude = db.ProfSaude.Find(id);
            if (profSaude == null)
            {
                return HttpNotFound();
            }
            return View(profSaude);
        }

        // POST: ProfSaudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfSaude profSaude = db.ProfSaude.Find(id);
            db.ProfSaude.Remove(profSaude);
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
