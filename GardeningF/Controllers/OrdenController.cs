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
    public class OrdenController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();

        // GET: Orden
        public ActionResult Index()
        {
            var orden_cliente = db.OrdenCliente.Where(o => o.fecha_envio == null).OrderByDescending(o => o.fecha_creacion).Include(o => o.Cliente).Include(o => o.Paqueteria);
            return View(orden_cliente.ToList());
        }

        public ActionResult Index1()
        {
            var orden = db.OrdenCliente.Where(o => o.fecha_entrega == null && o.fecha_envio != null).OrderByDescending(o => o.fecha_creacion).Include(o => o.Cliente).Include(o => o.Paqueteria);
            return View(orden.ToList());
        }

        // GET: Orden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenCliente ordenCliente = db.OrdenCliente.Find(id);
            if (ordenCliente == null)
            {
                return HttpNotFound();
            }
            return View(ordenCliente);
        }

        // GET: Orden/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nombre_cliente");
            ViewBag.id_dir_entrega = new SelectList(db.Direccion, "id_direccion", "calle");
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria");
            return View();
        }

        // POST: Orden/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden,fecha_creacion,num_confirmacion,total,id_cliente,id_dir_entrega,id_paqueteria,num_guia,fecha_envio,fecha_entrega")] OrdenCliente ordenCliente)
        {
            if (ModelState.IsValid)
            {
                db.OrdenCliente.Add(ordenCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nombre_cliente", ordenCliente.id_cliente);
            ViewBag.id_dir_entrega = new SelectList(db.Direccion, "id_direccion", "calle", ordenCliente.id_dir_entrega);
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria", ordenCliente.id_paqueteria);
            return View(ordenCliente);
        }

        // GET: Orden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenCliente ordenCliente = db.OrdenCliente.Find(id);
            if (ordenCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nombre_cliente", ordenCliente.id_cliente);
            ViewBag.id_dir_entrega = new SelectList(db.Direccion, "id_direccion", "calle", ordenCliente.id_dir_entrega);
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria", ordenCliente.id_paqueteria);
            return View(ordenCliente);
        }

        // POST: Orden/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_orden,fecha_creacion,num_confirmacion,total,id_cliente,id_dir_entrega,id_paqueteria,num_guia,fecha_envio,fecha_entrega")] OrdenCliente ordenCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nombre_cliente", ordenCliente.id_cliente);
            ViewBag.id_dir_entrega = new SelectList(db.Direccion, "id_direccion", "calle", ordenCliente.id_dir_entrega);
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria", ordenCliente.id_paqueteria);
            return View(ordenCliente);
        }

        // GET: Orden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenCliente ordenCliente = db.OrdenCliente.Find(id);
            if (ordenCliente == null)
            {
                return HttpNotFound();
            }
            return View(ordenCliente);
        }

        // POST: Orden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdenCliente ordenCliente = db.OrdenCliente.Find(id);
            db.OrdenCliente.Remove(ordenCliente);
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
