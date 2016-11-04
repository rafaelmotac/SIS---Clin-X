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
    [Authorize]
    public class PacientesController : Controller
    {
        private ClinicaDb db = new ClinicaDb();

        public ActionResult verificaCpf(string cpf, int? idPaciente)
        {
            var cpfExiste = (from p in db.Paciente where p.cpf.Equals(cpf) select p).FirstOrDefault();
            if(cpfExiste == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }else if(cpfExiste.idPaciente == idPaciente)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Admin,Prof.Saude,Funcionario")]
        // GET: Pacientes
        public ActionResult Index(string searchString)
        {
            var paciente = db.Paciente.Include(p => p.Convenio);

            if (!String.IsNullOrEmpty(searchString))
            {
                paciente = paciente.Where(s => s.cpf.Contains(searchString));
            }
            else
            {
                paciente = db.Paciente.Include(p => p.Convenio);
            }

            return View(paciente.ToList());
        }
        
        [Authorize(Roles = "Admin,Prof.Saude,Funcionario")]
        public ActionResult BuscarPaciente(string cpf)
        {
            Paciente paciente = (from p in db.Paciente where p.cpf.Equals(cpf) select p).FirstOrDefault();

            if(paciente != null)
            {
                Prontuario prontuario = (from p in db.Prontuario
                                         where p.paciente_IdPaciente == paciente.idPaciente
                                         select p).FirstOrDefault();
                if(prontuario != null)
                {
                    ViewBag.prontuario = prontuario;
                    return PartialView("_resultadoPaciente", paciente);

                }else
                {
                    return PartialView("_resultadoPaciente", paciente);
                }
            }else
            {
                return PartialView("_resultadoPaciente", paciente);
            }

           
        }

        [Authorize(Roles = "Admin,Prof.Saude,Funcionario")]
        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        [Authorize(Roles = "Admin,Funcionario")]
        // GET: Pacientes/Create
        public ActionResult Create()
        {
            ViewBag.convenio_idConvenio = new SelectList(db.Convenio, "idConvenio", "numConvenio");
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPaciente,nome,cpf,telefone,endereco,dtNascimento,convenio_idConvenio")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Paciente.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.convenio_idConvenio = new SelectList(db.Convenio, "idConvenio", "numConvenio", paciente.convenio_idConvenio);
            return View(paciente);
        }

        [Authorize(Roles = "Admin,Funcionario")]
        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            //ViewBag.convenio_idConvenio = new SelectList(db.Convenio, "idConvenio", "numConvenio", paciente.convenio_idConvenio);
            return View(paciente);
        }


        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,nome,cpf,telefone,endereco,dtNascimento,convenio_idConvenio")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.convenio_idConvenio = new SelectList(db.Convenio, "idConvenio", "numConvenio", paciente.convenio_idConvenio);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }


        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
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
