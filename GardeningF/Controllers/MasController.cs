using GardeningF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;

namespace GardeningF.Controllers
{
    [Authorize]
    public class MasController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        // GET: Mas
        [AllowAnonymous]
        public ActionResult Tarjeta()
        {
            return View();
        }
        public ActionResult Solicitar()
        {


            string correo = User.Identity.Name;

            string fechaCreacion = DateTime.Today.ToShortDateString();
            string fechaProbEntrega = DateTime.Today.AddDays(3).ToShortDateString();
            var cliente = (from c in db.Cliente
                           where c.correo_cliente == correo
                           select c).ToList().FirstOrDefault<Cliente>();
            var direccion = (from d in db.Direccion
                             where d.id_cliente == cliente.id_cliente
                             select d).ToList().FirstOrDefault<Direccion>();

            Session["calle"] = direccion.calle + " " + direccion.no_exterior;
            Session["colonia"] = direccion.colonia;
            Session["cp"] = direccion.cp;
            Session["estado"] = direccion.estado;
            Session["fechaOrden"] = fechaCreacion;
            Session["fpEntrega"] = fechaProbEntrega;
            Session["usr"] = cliente.nombre_cliente + " " + cliente.app_cliente + " " + cliente.apm_cliente;
            Session["correo"] = cliente.correo_cliente;
            Session["telefono"] = cliente.telefono_cliente;



            return View();
        }
        public ActionResult Confirmar()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Empresas()
        {

            var productos = db.Producto;
            ViewBag.productos = productos.ToList().Take(4);

            return View();
        }
    }
}
