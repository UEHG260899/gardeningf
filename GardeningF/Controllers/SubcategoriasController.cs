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
    public class SubcategoriasController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();

        // GET: Subcategorias
        public ActionResult Index()
        {
            var subcategoria = db.Subcategoria.Include(s => s.Categoria);
            return View(subcategoria.ToList());
        }

        // GET: Subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // GET: Subcategorias/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria");
            return View();
        }

        // POST: Subcategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_subcategoria,id_categoria,nombre_subcategoria,descripcion_subcategoria")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Subcategoria.Add(subcategoria);
                int id = subcategoria.id_subcategoria;
                var prod = db.Producto.Find(id);
                DateTime hoy = DateTime.Now;
                prod.ultima_actualizacion = hoy;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria", subcategoria.id_categoria);
            return View(subcategoria);
        }

        // GET: Subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria", subcategoria.id_categoria);
            return View(subcategoria);
        }

        // POST: Subcategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_subcategoria,id_categoria,nombre_subcategoria,descripcion_subcategoria")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria", subcategoria.id_categoria);
            return View(subcategoria);
        }

        // GET: Subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // POST: Subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            db.Subcategoria.Remove(subcategoria);
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
