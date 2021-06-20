using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using GardeningF.Models;

namespace GardeningF.Controllers
{
    public class AdminController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        // GET: Admin
        public ActionResult Index()
        {
            var empleado = db.Empleado.Include(e => e.Departamento);
            var productos = db.Producto.Include(e => e.Subcategoria);
            var categorias = db.Categoria.ToList();
            var subcategorias = db.Subcategoria.Include(e => e.Categoria);
            ViewBag.totalEmpleados = empleado.Count();
            ViewBag.totalProductos = productos.Count();
            ViewBag.totalCategorias = categorias.Count();
            ViewBag.totalSubcategorias = subcategorias.Count();
            ViewBag.empleados = empleado.ToList();
            ViewBag.productos = productos.Take(10);
            ViewBag.categorias = categorias;
            ViewBag.subcategorias = subcategorias.ToList();
            
            return View();
        }
    }
}