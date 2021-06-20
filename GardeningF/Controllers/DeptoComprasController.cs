using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GardeningF.Models;


namespace Gardening.Controllers
{
    
    public class DeptoComprasController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        // GET: DeptoCompras
        public ActionResult Index()
        {
            //lista de productos, categorias y subcategorias
            var producto = db.Producto.Include(p => p.Subcategoria);
            ViewBag.productos = producto;

            var categoria = db.Categoria.ToList();
            ViewBag.categorias = categoria;

            var subcategoria = db.Subcategoria.Include(e => e.Categoria);
            ViewBag.subcategorias = subcategoria.ToList();


            //counts de productos, categorias y subcategorias
            var countCate = ViewBag.categorias.Count;
            ViewBag.categoriasLen = countCate;

            var countSubc = ViewBag.subcategorias.Count;
            ViewBag.subcategoriasLen = countSubc;

            var countproducto = db.Producto.ToList();
            ViewBag.productosLen = countproducto.Count;

            return View();
        }
    }
}