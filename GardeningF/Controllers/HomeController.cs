using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using GardeningF.Models;

namespace GardeningF.Controllers
{
    public class HomeController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        public ActionResult Index()
        {
            //Items del carrito
            if (Session["itemCarro"] == null)
            {
                Session["itemCarro"] = 0;
            }
            //Código para los productos
            Random ran = new Random();
            int num = ran.Next(1, 60);
            int num2 = ran.Next(1, 60);
            int num3 = ran.Next(1, 60);
            int num4 = ran.Next(1, 60);
            var producto = db.Producto.SqlQuery(
                @"SELECT * FROM dbo.Producto WHERE id_producto IN (@id1,@id2,@id3,@id4)",
                new SqlParameter("@id1", num), new SqlParameter("@id2", num2), new SqlParameter("@id3", num3), new SqlParameter("@id4", num4)).ToList();
            //Categorias
            var categorias = db.Categoria.ToList();
            //Viewbag
            ViewBag.categorias = categorias;
            ViewBag.prod = producto;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}