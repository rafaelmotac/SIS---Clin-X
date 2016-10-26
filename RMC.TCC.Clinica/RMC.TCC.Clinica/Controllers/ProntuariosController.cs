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
    public class ProntuariosController : Controller
    {
        private ClinicaDb db = new ClinicaDb();

        [Authorize(Roles = "Prof.Saude,Admin")]
        public ActionResult Buscar()
        {
            return View();
        }
        [Authorize(Roles = "Prof.Saude,Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult resultadoBuscarProntuario(string cpf)
        {
            Prontuario prontuario = (from p in db.Prontuario where p.Paciente.cpf.Equals(cpf) select p).FirstOrDefault();

            var paciente = from p in db.Paciente where p.cpf.Equals(cpf) select p;

            var resultado = from p in db.Prontuario
                                    where p.paciente_IdPaciente == paciente.FirstOrDefault().idPaciente
                                    select p;

            if(paciente.FirstOrDefault() != null && resultado.FirstOrDefault() == null)
            {
                return PartialView("_prontuarioInexistente");
            }

            //Prontuario resultado = db.Prontuario.Find(idPaciente.FirstOrDefault());

            return PartialView("_resultadoBusca",resultado.FirstOrDefault());
        }

        [Authorize(Roles = "Prof.Saude,Admin")]
        public ActionResult EditarExamesProntuario(int paciente_IdPaciente)
        {
            Prontuario prontuario = db.Prontuario.Find(paciente_IdPaciente);           

            return View("Exames",prontuario);
        }

        [Authorize(Roles = "Prof.Saude,Admin")]
        public ActionResult CadastrarExameProntuario(int paciente_IdPaciente)
        {
            Prontuario prontuario = db.Prontuario.Find(paciente_IdPaciente);

            ViewBag.prontuario = prontuario;

            List<Exame> exames = (from e in db.Exame select e).ToList();

            ViewBag.Exames = new SelectList(exames, "idExame", "Nome");

            return View();
        }

        // GET: Prontuarios
        [Authorize(Roles = "Prof.Saude,Admin")]
        public ActionResult Index()
        {
            return View("Buscar");
        }

        // GET: Prontuarios/Details/5
        [Authorize(Roles = "Prof.Saude,Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prontuario prontuario = db.Prontuario.Find(id);
            if (prontuario == null)
            {
                return HttpNotFound();
            }
            return View(prontuario);
        }

        [Authorize(Roles = "Admin,Prof.Saude,Funcionario")]
        // GET: Prontuarios/Create
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Prontuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Prof.Saude,Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "procedimentos,prescricoes,paciente_IdPaciente,historico")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                db.Prontuario.Add(prontuario);
                db.SaveChanges();
                ViewBag.prontuario = prontuario;
                return PartialView("_resultadoCadastrar");
            }
            return PartialView("_resultadoCadastrar");
        }

        [Authorize(Roles = "Prof.Saude,Admin")]
        // GET: Prontuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prontuario prontuario = db.Prontuario.Find(id);

            Paciente paciente = db.Paciente.Find(prontuario.paciente_IdPaciente);

            if (prontuario == null)
            {
                return HttpNotFound();
            }

            ViewBag.paciente = paciente;
            
            return View(prontuario);
        }

        // POST: Prontuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Prof.Saude,Admin")]
        public ActionResult Edit([Bind(Include = "procedimentos,prescricoes,paciente_IdPaciente,historico")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prontuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "paciente_IdPaciente", "nome", prontuario.paciente_IdPaciente);
            return View(prontuario);
        }

        [Authorize(Roles = "Admin")]
        // GET: Prontuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prontuario prontuario = db.Prontuario.Find(id);
            if (prontuario == null)
            {
                return HttpNotFound();
            }
            return View(prontuario);
        }

        // POST: Prontuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Prontuario prontuario = db.Prontuario.Find(id);
            db.Prontuario.Remove(prontuario);
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
