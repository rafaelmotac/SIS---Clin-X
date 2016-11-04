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
    [Authorize(Roles = "Admin,Funcionario")]
    public class ConveniosController : Controller
    {
        private ClinicaDb db = new ClinicaDb();

        public ActionResult verificaConvenio(string numConvenio, string nomeConvenio ,int? paciente_IdPaciente)
        {
            var convenioExiste = (from p in db.Convenio where p.numConvenio.Equals(numConvenio) && p.nomeConvenio.Equals(nomeConvenio) select p ).FirstOrDefault();
            if (convenioExiste == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else if (convenioExiste.paciente_IdPaciente == paciente_IdPaciente)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CadastrarConvenio(int idPaciente)
        {
            Paciente paciente = db.Paciente.Find(idPaciente);

            ViewBag.Paciente = paciente;

            //Convenio convenio = db.Convenio.Find(idPaciente);

            if(paciente.Convenio == null)
            {
                return View();
            }else
            {
                return View("convenioExistente");
            }

            
        }


        // GET: Convenios
        public ActionResult Index()
        {
            return View(db.Convenio.ToList());
        }

        // GET: Convenios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convenio convenio = db.Convenio.Find(id);
            if (convenio == null)
            {
                return HttpNotFound();
            }
            return View(convenio);
        }

        // POST: Convenios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numConvenio,nomeConvenio,paciente_IdPaciente")] Convenio convenio)
        {
            if (ModelState.IsValid)
            {
                db.Convenio.Add(convenio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Paciente = db.Paciente.Find(convenio.paciente_IdPaciente);

            return View("CadastrarConvenio",convenio);
        }

        // GET: Convenios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convenio convenio = db.Convenio.Find(id);
            if (convenio == null)
            {
                return HttpNotFound();
            }
            return View(convenio);
        }

        // POST: Convenios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "paciente_IdPaciente,numConvenio,nomeConvenio")] Convenio convenio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(convenio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(convenio);
        }

        // GET: Convenios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convenio convenio = db.Convenio.Find(id);
            if (convenio == null)
            {
                return HttpNotFound();
            }
            return View(convenio);
        }

        // POST: Convenios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Convenio convenio = db.Convenio.Find(id);
            db.Convenio.Remove(convenio);
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
