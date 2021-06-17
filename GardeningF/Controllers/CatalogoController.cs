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

        public ActionResult Categoria(int id)
        {

            var query_categoria = db.Database.SqlQuery<int>(@"SELECT id_subcategoria
	                                                                        FROM dbo.Subcategoria
	                                                                        WHERE id_categoria = @idCat"
                                                       , new SqlParameter("@idCat", id)).ToArray();

            if (query_categoria.Length == 3)
            {
                var query_prod = db.Producto.SqlQuery(@"SELECT * FROM dbo.Producto WHERE id_subcategoria IN (@id1, @id2, @id3)"
                                                        , new SqlParameter("@id1", query_categoria[0])
                                                        , new SqlParameter("@id2", query_categoria[1])
                                                        , new SqlParameter("@id3", query_categoria[2])).ToList();
                ViewBag.prod = query_prod;
            }
            else
            {
                var query_prod = db.Producto.SqlQuery(@"SELECT * FROM dbo.Producto WHERE id_subcategoria IN (@id1, @id2, @id3, @id4)"
                                                        , new SqlParameter("@id1", query_categoria[0])
                                                        , new SqlParameter("@id2", query_categoria[1])
                                                        , new SqlParameter("@id3", query_categoria[2])
                                                        , new SqlParameter("@id4", query_categoria[3])).ToList();
                ViewBag.prod = query_prod;
            }

            return View();
        }
    }
}