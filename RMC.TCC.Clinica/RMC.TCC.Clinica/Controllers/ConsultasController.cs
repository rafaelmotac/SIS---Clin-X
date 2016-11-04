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
    public class ConsultasController : Controller
    {
        private ClinicaDb db = new ClinicaDb();

        public ActionResult verificaConsulta(int profSaude_idProfSaude, int paciente_IdPaciente, TimeSpan horaConsulta,DateTime dtConsulta)
        {
            Consulta verificaPaciente = (from c in db.Consulta
                                         where (c.dtConsulta.Equals(dtConsulta)) && (c.horaConsulta.Equals(horaConsulta))
                                          && (c.paciente_IdPaciente.Equals(paciente_IdPaciente))
                                         select c).FirstOrDefault();

            Consulta verificaProfSaude = (from c in db.Consulta
                                          where (c.dtConsulta.Equals(dtConsulta)) && (c.horaConsulta.Equals(horaConsulta))
                                           && (c.profSaude_idProfSaude.Equals(profSaude_idProfSaude))
                                          select c).FirstOrDefault();

            if(verificaPaciente != null && verificaProfSaude != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            if (verificaPaciente == null)
            {
                if(verificaProfSaude == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }     
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Buscar()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult BuscarPorPaciente()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Funcionario,Prof.Saude")]
        public ActionResult BuscarPorMedico()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult BuscarPorDia()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Funcionario")]
        [ValidateAntiForgeryToken]
        public ActionResult resultadoBuscaPaciente(string cpf, DateTime dataConsulta)
        {
            var paciente = from p in db.Paciente where p.cpf.Equals(cpf) select p.idPaciente;

            var resultado = from c in db.Consulta
                            where c.paciente_IdPaciente.Equals(paciente.FirstOrDefault()) && c.dtConsulta == dataConsulta
                            select c;

            return PartialView("_resultadoBusca", resultado.ToList());
        }

        [Authorize(Roles = "Admin,Funcionario,Prof.Saude")]
        [ValidateAntiForgeryToken]
        public ActionResult resultadoBuscaMedico(string cpf, DateTime dataConsulta)
        {
            var resultado = from c in db.Consulta
                            where c.profSaude_idProfSaude.Equals((
                            from m in db.ProfSaude where m.cpf.Equals(cpf) select m.idProfSaude).FirstOrDefault()) && c.dtConsulta.Equals(dataConsulta)
                            select c;

            return PartialView("_resultadoBusca", resultado.ToList());

        }

        [Authorize(Roles = "Admin,Funcionario")]
        [ValidateAntiForgeryToken]
        public ActionResult resultadoBuscaDia(DateTime dataConsulta)
        {
            var resultado = from c in db.Consulta where c.dtConsulta.Equals(dataConsulta) select c;

            return PartialView("_resultadoBusca", resultado.ToList());
        }


        // GET: Consultas
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Index()
        {
            var consulta = db.Consulta.Include(c => c.Paciente).Include(c => c.ProfSaude);
            return View(consulta.ToList());
        }

        // GET: Consultas/Details/5
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consultas/Create
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Create()
        {
            ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome");
            ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Create([Bind(Include = "idConsulta,dtConsulta,profSaude_idProfSaude,paciente_IdPaciente,horaConsulta")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {

                Consulta verificaPaciente = (from c in db.Consulta
                                    where (c.dtConsulta.Equals(consulta.dtConsulta)) && (c.horaConsulta.Equals(consulta.horaConsulta))
                                     && (c.paciente_IdPaciente.Equals(consulta.paciente_IdPaciente))
                                    select c).FirstOrDefault();

                Consulta verificaProfSaude = (from c in db.Consulta
                                             where (c.dtConsulta.Equals(consulta.dtConsulta)) && (c.horaConsulta.Equals(consulta.horaConsulta))
                                              && (c.profSaude_idProfSaude.Equals(consulta.profSaude_idProfSaude))
                                             select c).FirstOrDefault();

                if(verificaPaciente != null && verificaProfSaude != null)
                {
                    ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome", consulta.paciente_IdPaciente);
                    ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", consulta.profSaude_idProfSaude);
                    ModelState.AddModelError("paciente_IdPaciente", "Paciente já possui uma consulta nesse mesmo dia e horário!");
                    ModelState.AddModelError("profSaude_idProfSaude", "Prof.Saude já possui uma consulta nesse mesmo dia e horário!");
                    return View(consulta);

                }
                if (verificaPaciente == null)
                {
                    if (verificaProfSaude == null)
                    {
                        db.Consulta.Add(consulta);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome", consulta.paciente_IdPaciente);
                    ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", consulta.profSaude_idProfSaude);
                    ModelState.AddModelError("profSaude_idProfSaude", "Prof.Saude já possui uma consulta nesse mesmo dia e horário!");
                    return View(consulta);
                }
                ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome", consulta.paciente_IdPaciente);
                ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", consulta.profSaude_idProfSaude);
                ModelState.AddModelError("paciente_IdPaciente", "Paciente já possui uma consulta nesse mesmo dia e horário!");
                return View(consulta);

            }

            ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome", consulta.paciente_IdPaciente);
            ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", consulta.profSaude_idProfSaude);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome", consulta.paciente_IdPaciente);
            ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", consulta.profSaude_idProfSaude);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Edit([Bind(Include = "idConsulta,dtConsulta,profSaude_idProfSaude,paciente_IdPaciente,horaConsulta")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                Consulta verificaPaciente = (from c in db.Consulta
                                             where (c.dtConsulta.Equals(consulta.dtConsulta)) && (c.horaConsulta.Equals(consulta.horaConsulta))
                                              && (c.paciente_IdPaciente.Equals(consulta.paciente_IdPaciente))
                                             select c).FirstOrDefault();

                Consulta verificaProfSaude = (from c in db.Consulta
                                              where (c.dtConsulta.Equals(consulta.dtConsulta)) && (c.horaConsulta.Equals(consulta.horaConsulta))
                                               && (c.profSaude_idProfSaude.Equals(consulta.profSaude_idProfSaude))
                                              select c).FirstOrDefault();

                if(verificaPaciente == null && verificaProfSaude == null)
                {
                    //db.Entry(consulta).State = EntityState.Modified;
                    Consulta atualiza = db.Consulta.Find(consulta.idConsulta);
                    atualiza.horaConsulta = consulta.horaConsulta;
                    atualiza.paciente_IdPaciente = consulta.paciente_IdPaciente;
                    atualiza.profSaude_idProfSaude = consulta.profSaude_idProfSaude;
                    atualiza.dtConsulta = consulta.dtConsulta;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }

                if(verificaPaciente == null || verificaPaciente.idConsulta == consulta.idConsulta)
                {
                    if(verificaProfSaude == null || verificaProfSaude.idConsulta == consulta.idConsulta)
                    {
                        //db.Entry(consulta).State = EntityState.Modified;
                        Consulta atualiza =  db.Consulta.Find(consulta.idConsulta);
                        atualiza.horaConsulta = consulta.horaConsulta;
                        atualiza.paciente_IdPaciente = consulta.paciente_IdPaciente;
                        atualiza.profSaude_idProfSaude = consulta.profSaude_idProfSaude;
                        atualiza.dtConsulta = consulta.dtConsulta;
                        db.SaveChanges();
                        return RedirectToAction("index");
                    }else
                    {
                        ModelState.AddModelError("profSaude_idProfSaude", "Prof.Saude já possui uma consulta nesse mesmo dia e horário!");
                    }
                }else
                {
                    ModelState.AddModelError("paciente_IdPaciente", "Paciente já possui uma consulta nesse mesmo dia e horário!");

                    if (verificaProfSaude != null && verificaProfSaude.idConsulta != consulta.idConsulta)
                    {
                        ModelState.AddModelError("profSaude_idProfSaude", "Prof.Saude já possui uma consulta nesse mesmo dia e horário!");
                    }
                }

                ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome", consulta.paciente_IdPaciente);
                ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", consulta.profSaude_idProfSaude);
                return View(consulta);

            }
            ViewBag.paciente_IdPaciente = new SelectList(db.Paciente, "idPaciente", "nome", consulta.paciente_IdPaciente);
            ViewBag.profSaude_idProfSaude = new SelectList(db.ProfSaude, "idProfSaude", "nome", consulta.profSaude_idProfSaude);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult DeleteConfirmed(int id)
        {
            Consulta consulta = db.Consulta.Find(id);
            db.Consulta.Remove(consulta);
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
