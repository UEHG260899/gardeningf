using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;
using System.Security.Cryptography;


namespace GardeningF.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        private string NumConfirPago;
        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Crearorden()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["CrearOrden"] = "pend";
                return RedirectToAction("Login", "Account");
            }

            string correo = User.Identity.Name;

            string fechaCreacion = DateTime.Today.ToShortDateString();
            string fechaProbEntrega = DateTime.Today.AddDays(3).ToShortDateString();
            var cliente = (from c in db.Cliente
                           where c.correo_cliente == correo
                           select c).ToList().FirstOrDefault<Cliente>();
            var direccion = (from d in db.Direccion
                             where d.id_cliente == cliente.id_cliente
                             select d).ToList().FirstOrDefault<Direccion>();

            Session["dirCliente"] = direccion.calle + " " + direccion.colonia + " " + direccion.cp + " " + direccion.estado;
            Session["fechaOrden"] = fechaCreacion;
            Session["fpEntrega"] = fechaProbEntrega;
            Session["usr"] = cliente.nombre_cliente+" "+cliente.app_cliente+" "+cliente.apm_cliente;

            if (cliente.num_tdc.StartsWith("4"))
            {
                Session["tTarjeta"] = "1";
            }
            else if (cliente.num_tdc.StartsWith("5"))
            {
                Session["tTarjeta"] = "2";
            }
            else
            {
                Session["tTarjeta"] = "3";
            }
            Session["noTarjeta"] = cliente.num_tdc;
            return View();
        }
    }
}