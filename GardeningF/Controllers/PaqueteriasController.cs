using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;

namespace GardeningF.Controllers
{
    public class PaqueteriasController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();

        // GET: Paqueterias
        public ActionResult Index()
        {
            return View(db.Paqueteria.ToList());
        }

        // GET: Paqueterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // GET: Paqueterias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paqueterias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_paqueteria,nombre_paqueteria,rfc_paqueteria,telefono_paqueteria,web_paqueteria,direccion_paqueteria,contacto_paqueteria,tel_contacto")] Paqueteria paqueteria)
        {
            if (ModelState.IsValid)
            {
                db.Paqueteria.Add(paqueteria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paqueteria);
        }

        // GET: Paqueterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // POST: Paqueterias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_paqueteria,nombre_paqueteria,rfc_paqueteria,telefono_paqueteria,web_paqueteria,direccion_paqueteria,contacto_paqueteria,tel_contacto")] Paqueteria paqueteria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paqueteria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paqueteria);
        }

        [HttpPost]
        public ActionResult EditPaqueteria(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // GET: Paqueterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // POST: Paqueterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            db.Paqueteria.Remove(paqueteria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeletePaqueteria(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
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
