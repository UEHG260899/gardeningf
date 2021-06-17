using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;
using System.Text;

namespace GardeningF.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string nombre_cliente, string app_cliente, string apm_cliente, string telefono_cliente, string correo_cliente, string estado, string municipio, string calle, string colonia, string cp, string noExt, string tarjeta, string cvv, string fecha, string tipoT)
        {
            /*Cliente cliente = new Cliente();
            int id = 0;
            if(!(db.Cliente.Max(c => (int?)c.id_cliente) == null))
            {
                id = db.Cliente.Max(c => c.id_cliente);
            }
            else
            {
                id = 0;
            }
            id++;

            if (tarjetaValida(tarjeta, tipoT, fecha, cvv))
            {

            }*/
            return RedirectToAction("Index", "Home");
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,nombre_cliente,app_cliente,apm_cliente,correo_cliente,telefono_cliente,num_tdc,cvv,fecha_vencimiento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //Validación de fecha de tarjeta
        private bool tarjetaValida(string tarjeta, string tipoTarjeta, string fecha, string cvv)
        {
            bool bandera = validar(tarjeta);

            if (bandera)
            {
                if((tarjeta.StartsWith("4")) && (tipoTarjeta.Equals("Visa")))
                {
                    bandera = true;
                }else if ((tarjeta.StartsWith("5")) && (tipoTarjeta.Equals("Master")))
                {
                    bandera = true;
                }else if ((tarjeta.StartsWith("3")) && (tipoTarjeta.Equals("American")))
                {
                    bandera = true;
                }
                else
                {
                    bandera = false;
                }

                DateTime hoy = new DateTime();
                string[] fechaTarjeta = fecha.Split('-');
                if(Convert.ToInt32(fechaTarjeta[0]) >= hoy.Year)
                {
                    if(Convert.ToInt32(fechaTarjeta[1]) > hoy.Month)
                    {
                        bandera = true;
                    }
                    else
                    {
                        bandera = false;
                    }
                }
                else
                {
                    bandera = false;
                }

            }

            return bandera;
        }

        //Algoritmo de Luhn
        private bool validar(string tarjeta)
        {
            bool retorna = true;
            StringBuilder soloDigitos = new StringBuilder();

            foreach(Char c in tarjeta)
            {
                if (Char.IsDigit(c))
                {
                    soloDigitos.Append(c);
                }
            }

            //Hay más o menos digitos de los esperados
            if (soloDigitos.Length > 18 || soloDigitos.Length < 15) return false;

            //varibles auxiliares en el algoritmo
            int sum = 0;
            int digito = 0;
            int addEnd = 0;
            bool porDos = false;

            for(int i = soloDigitos.Length - 1; i >= 0; i--)
            {
                digito = Int32.Parse(soloDigitos.ToString(i, 1));
                if (porDos)
                {
                    addEnd = digito * 2;
                    if(addEnd > 9)
                    {
                        addEnd -= 9;
                    }
                }
                else
                {
                    addEnd = digito;
                }

                sum += addEnd;

                porDos = !porDos;
            }

            retorna = ((sum % 10) == 0);
            return retorna;
        }
    }
}
