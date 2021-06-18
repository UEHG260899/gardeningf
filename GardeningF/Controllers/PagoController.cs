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

            Session["calle"] = direccion.calle + " " + direccion.no_exterior;
            Session["colonia"] = direccion.colonia;
            Session["cp"] = direccion.cp;
            Session["estado"] = direccion.estado;
            Session["fechaOrden"] = fechaCreacion;
            Session["fpEntrega"] = fechaProbEntrega;
            Session["usr"] = cliente.nombre_cliente+" "+cliente.app_cliente+" "+cliente.apm_cliente;
            Session["correo"] = cliente.correo_cliente;
            Session["telefono"] = cliente.telefono_cliente;


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

        public ActionResult Pagar(string tipoPago)
        {
            string correo = User.Identity.Name;

            DateTime fechaCreacion = DateTime.Today;
            DateTime fechaProbEntrega = fechaCreacion.AddDays(3);
            var cliente = (from c in db.Cliente
                           where c.correo_cliente == correo
                           select c).ToList().FirstOrDefault<Cliente>();
            int id = cliente.id_cliente;

            if (tipoPago.Equals("T"))
            {
                if (!validaPago(cliente))
                {
                    return RedirectToAction("pagoNoAceptado");
                }
                else
                {
                    var dirEnt = (from d in db.Direccion
                                  where d.id_cliente == id
                                  select d).ToList().FirstOrDefault<Direccion>();
                    int idDir = dirEnt.id_direccion;
                    return RedirectToAction("pagoAceptado", routeValues: new { idC = id, idDir = idDir });
                }
            }

            if (tipoPago.Equals("P"))
            {
                var dirEnt = (from d in db.Direccion
                              where d.id_cliente == cliente.id_cliente
                              select d).ToList().FirstOrDefault<Direccion>();

                int idDir = dirEnt.id_direccion;
                validaPago(cliente);
                return RedirectToAction("pagoPaypal", routeValues: new { idC = id, idDir = idDir });
            }
            return View();
        }

        public ActionResult pagoAceptado(int idC, int idDir)
        {
            OrdenCliente orden = new OrdenCliente();
            int id = 0;
            if (!(db.OrdenCliente.Max(o => (int?)o.id_orden) == null))
            {
                id = db.OrdenCliente.Max(o => o.id_orden);
            }
            else
            {
                id = 0;
            }

            id++;
            orden.id_orden = id;
            orden.fecha_creacion = DateTime.Today;
            orden.num_confirmacion = Session["nConfirma"].ToString();
            var carro = Session["cart"] as List<Item>;
            var total = carro.Sum(item => item.Product.precio_producto * item.Cantidad);
            orden.total = Convert.ToInt32(total);
            orden.id_cliente = idC;
            orden.id_dir_entrega = idDir;
            db.OrdenCliente.Add(orden);
            db.SaveChanges();

            OrdenProducto ordenProd;
            List<OrdenProducto> listaProd = new List<OrdenProducto>();
            foreach (Item linea in carro)
            {
                ordenProd = new OrdenProducto();
                ordenProd.id_orden = orden.id_orden;
                ordenProd.id_producto = linea.Product.id_producto;
                ordenProd.cantidad = linea.Cantidad;
                db.OrdenProducto.Add(ordenProd);
            }

            db.SaveChanges();
            Session["cart"] = null;
            Session["nConfirma"] = null;
            Session["itemTotal"] = 0;
            return View();
        }

        private bool validaPago(Cliente cliente)
        {
            bool retorna = true;

            int randomValue;

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] val = new byte[6];
                crypto.GetBytes(val);
                randomValue = BitConverter.ToInt32(val, 1);
            }

            NumConfirPago = Math.Abs(randomValue * 1000).ToString();
            Session["nConfirma"] = NumConfirPago;
            return retorna;
        }
    }
}