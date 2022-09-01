using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PG180715P1.Models;

namespace PG180715P1.Controllers
{
    public class EmpleadosController : Controller
    {
        private EmpleadoDBContext db = new EmpleadoDBContext();

        // GET: Empleados
        public ActionResult Index(string buscarString, string cargoEmpleados, string sueldoEmpleados)
        {
            decimal SueldoBase;
            var CargoLst = new List<string>();
            var CargoQry = from d in db.Empleados
                            orderby d.Cargo
                            select d.Cargo;
            CargoLst.AddRange(CargoQry.Distinct());
            ViewBag.cargoEmpleados = new SelectList(CargoLst);


            var empleados = from p in db.Empleados
                            select p;

            var sueldo = from p in db.Empleados
                            select p;


            if (!String.IsNullOrEmpty(buscarString))
            {
                empleados = empleados.Where(s => s.Apellidos.Contains(buscarString));
            }

            if (!string.IsNullOrEmpty(cargoEmpleados))
            {
                empleados = empleados.Where(x => x.Cargo == cargoEmpleados);
            }

            if (Decimal.TryParse(sueldoEmpleados, out SueldoBase))  
            {
                empleados = empleados.Where(s => ((s.SueldoBase - SueldoBase <= 20 && s.SueldoBase - SueldoBase >= -20)));
            }

            return View(empleados);
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombres,Apellidos,FechaNacimiento,Direccion,Telefono,Cargo,SueldoBase")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombres,Apellidos,FechaNacimiento,Direccion,Telefono,Cargo,SueldoBase")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
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
