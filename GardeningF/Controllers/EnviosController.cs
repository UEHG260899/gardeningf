using GardeningF.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardeningF.Controllers
{
    public class EnviosController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();

        // GET: Envios
        public ActionResult Index()
        {
            var query = from p in db.Paqueteria
                        select p;

            List<Paqueteria> paqueterias = query.ToList();

            ViewBag.ttlPaqueterias = paqueterias.Count;

            var query2 = from o in db.OrdenCliente
                         where o.fecha_envio == null
                         select o;

            List<OrdenCliente> sinFechaEnvio = query2.ToList();

            ViewBag.sinFechaEnvio = sinFechaEnvio.Count;

            var query3 = from o in db.OrdenCliente
                         where o.fecha_entrega == null && o.fecha_envio != null
                         select o;


            List<OrdenCliente> sf = query3.ToList();

            ViewBag.sinFechaEntrega = sf.Count;

            return View();
        }
    }
}