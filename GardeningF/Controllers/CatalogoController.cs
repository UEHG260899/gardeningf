using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;
using System.Data.SqlClient;

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

        public ActionResult Subcategoria(int id)
        {
            var productos = db.Producto.SqlQuery(
                    @"SELECT *
                        FROM dbo.Producto
                        WHERE id_subcategoria = @idSub",
                    new SqlParameter("@idSub", id)
                ).ToList();
            var subcatego = db.Subcategoria.SqlQuery(@"SELECT * FROM dbo.Subcategoria WHERE id_subcategoria = @idSub"
                                                       , new SqlParameter("@idSub", id)).ToList();
            Subcategoria subCatego = subcatego.FirstOrDefault<Subcategoria>();
            string nombreSub = subCatego.nombre_subcategoria;
            ViewBag.prod = productos;
            ViewBag.sub = nombreSub;
            return View();
        }
    }
}