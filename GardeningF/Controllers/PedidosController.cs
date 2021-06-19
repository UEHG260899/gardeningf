using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;
namespace GardeningF.Controllers
{
    public class PedidosController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        // GET: Pedidos
        public ActionResult Index()
        {
            string correo = User.Identity.Name;
            Cliente cl = (from c in db.Cliente
                          where c.correo_cliente == correo
                          select c).ToList().FirstOrDefault<Cliente>();

            int id = cl.id_cliente;

            var query = from o in db.OrdenCliente
                        where o.id_cliente == id
                        orderby o.fecha_creacion ascending
                        select o;

            List<OrdenCliente> ordenes = query.ToList();
            List<PedidoCliente> pedidos = new List<PedidoCliente>();
            PedidoCliente pedido;
            List<OrdenProducto> ordPed;
            List<ItemPedidos> itemPed = new List<ItemPedidos>();

            ItemPedidos iPed;

            foreach (OrdenCliente o in ordenes)
            {
                pedido = new PedidoCliente();
                pedido.Orden = o;
                pedido.Fecha = o.fecha_creacion.GetValueOrDefault().ToShortDateString();
                if (o.fecha_envio.HasValue)
                {
                    pedido.Envio = o.fecha_envio.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    pedido.Envio = "Proximamente";
                }
                if (o.fecha_entrega.HasValue)
                {
                    pedido.Status = "Entregado";
                }
                else
                {
                    pedido.Status = "Sin entregar";
                }
                pedido.Total = o.total.ToString();
                pedidos.Add(pedido);

                ordPed = (from oP in db.OrdenProducto
                          where oP.id_orden == o.id_orden
                          select oP).ToList();
                pedido.ordenProductos = ordPed;
                foreach (OrdenProducto op in ordPed)
                {
                    iPed = new ItemPedidos();
                    iPed.idOrd = op.id_orden;
                    iPed.Product = db.Producto.First(p => p.id_producto == op.id_producto);
                    iPed.Cantidad = op.cantidad;
                    itemPed.Add(iPed);
                }
            }
            Session["misPedidos"] = pedidos;
            Session["Pedido"] = itemPed;
            return View();
        }
        public ActionResult verPedido(int? idP)
        {
            string correo = User.Identity.Name;
            Cliente cl = (from c in db.Cliente
                          where c.correo_cliente == correo
                          select c).ToList().FirstOrDefault<Cliente>();

            int id = cl.id_cliente;

            var query = from o in db.OrdenCliente
                        where o.id_cliente == id
                        orderby o.fecha_creacion ascending
                        select o;

            List<OrdenCliente> ordenes = query.ToList();
            List<PedidoCliente> pedidos = new List<PedidoCliente>();
            PedidoCliente pedido;
            List<OrdenProducto> ordPed;
            List<ItemPedidos> itemPed = new List<ItemPedidos>();

            ItemPedidos iPed;

            foreach (OrdenCliente o in ordenes)
            {
                pedido = new PedidoCliente();
                pedido.Orden = o;
                pedido.Fecha = o.fecha_creacion.GetValueOrDefault().ToShortDateString();
                if (o.fecha_envio.HasValue)
                {
                    pedido.Envio = o.fecha_envio.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    pedido.Envio = "Proximamente";
                }
                if (o.fecha_entrega.HasValue)
                {
                    pedido.Status = "Entregado";
                }
                else
                {
                    pedido.Status = "Sin entregar";
                }
                pedido.Total = o.total.ToString();
                pedidos.Add(pedido);

                ordPed = (from oP in db.OrdenProducto
                          where oP.id_orden == o.id_orden
                          select oP).ToList();
                pedido.ordenProductos = ordPed;
                foreach (OrdenProducto op in ordPed)
                {
                    iPed = new ItemPedidos();
                    iPed.idOrd = op.id_orden;
                    iPed.Product = db.Producto.First(p => p.id_producto == op.id_producto);
                    iPed.Cantidad = op.cantidad;
                    itemPed.Add(iPed);
                }
            }
            Session["misPedidos"] = pedidos;
            Session["Pedido"] = itemPed;
            ViewBag.idp = idP;
            return View();
        }
    }
}