using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using GardeningF.Models;

namespace GardeningF.Controllers
{
    public class CarroController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();

        // GET: Carro
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
            //Viewbag
            ViewBag.prod = producto;
            return View();
        }

        public ActionResult Agregar(int id)
        {
            ProdCarro carro = new ProdCarro();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();

                Producto p = carro.find(id);
                string name = p.nombre_producto;
                cart.Add(new Item { Product = carro.find(id), Cantidad = 1 });
                Session["cart"] = cart;
                Session["itemTotal"] = cart.Count;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExists(id);
                if (index != -1)
                {
                    cart[index].Cantidad++;
                }
                else
                {
                    cart.Add(new Item { Product = carro.find(id), Cantidad = 1 });
                }
                Session["cart"] = cart;
                Session["itemTotal"] = cart.Count;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Restar(int id)
        {
            ProdCarro carro = new ProdCarro();

            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExists(id);
            if (index != -1)
            {
                cart[index].Cantidad--;
                if (cart[index].Cantidad == 0)
                {
                    var itemToRemove = cart.Single(r => r.Product.id_producto == id);
                    cart.Remove(itemToRemove);
                }
                Session["cart"] = cart;
                Session["itemTotal"] = cart.Count;
            }



            return RedirectToAction("Index");
        }
        private int isExists(int id)
        {
            List<Item> carro = (List<Item>)Session["cart"];
            for (int i = 0; i < carro.Count; i++)
            {
                if (carro[i].Product.id_producto.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult Quitar(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExists(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            Session["itemTotal"] = (int)Session["itemTotal"] - 1;
            return RedirectToAction("Index");
        }
    }
}