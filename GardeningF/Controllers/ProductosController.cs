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
    public class ProductosController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();

        // GET: Productoes
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Subcategoria);
            return View(producto.ToList());
        }


        public ActionResult Vista_producto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            Subcategoria subcategoria = db.Subcategoria.Find(producto.id_subcategoria);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }


        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.id_subcategoria = new SelectList(db.Subcategoria, "id_subcategoria", "nombre_subcategoria");
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto,id_subcategoria,nombre_producto,descripcion_producto,precio_producto,ultima_actualizacion,imagen_producto,cantidad_existencia,stock,descuento")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                int id = producto.id_producto;
                var prod = db.Producto.Find(id);
                DateTime hoy = DateTime.Now;
                prod.ultima_actualizacion = hoy;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_subcategoria = new SelectList(db.Subcategoria, "id_subcategoria", "nombre_subcategoria", producto.id_subcategoria);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_subcategoria = new SelectList(db.Subcategoria, "id_subcategoria", "nombre_subcategoria", producto.id_subcategoria);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto,id_subcategoria,nombre_producto,descripcion_producto,precio_producto,ultima_actualizacion,imagen_producto,cantidad_existencia,stock,descuento")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_subcategoria = new SelectList(db.Subcategoria, "id_subcategoria", "nombre_subcategoria", producto.id_subcategoria);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
