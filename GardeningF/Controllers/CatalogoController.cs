using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;

namespace GardeningF.Controllers
{
    public class CatalogoController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        // GET: Catalogo
        public ActionResult Index()
        {
            var catego = db.Categoria.OrderBy(cat => cat.nombre_categoria).ToList();
            var sub = db.Subcategoria.OrderBy(subc => subc.nombre_subcategoria).ToList();
            var productos = db.Producto.ToList();
            ViewBag.catego = catego;
            ViewBag.sub = sub;
            ViewBag.prod = productos;
            return View();
        }
    }
}