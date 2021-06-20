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
    //[Authorize]
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
            Cliente cliente = new Cliente();
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
                if (validaPago(nombre_cliente, calle, colonia, estado, cp, tarjeta, fecha, cvv))
                {
                    cliente.id_cliente = id;
                    cliente.nombre_cliente = nombre_cliente;
                    cliente.app_cliente = app_cliente;
                    cliente.apm_cliente = apm_cliente;
                    cliente.telefono_cliente = telefono_cliente;
                    cliente.correo_cliente = correo_cliente;
                    cliente.num_tdc = tarjeta;
                    cliente.cvv = int.Parse(cvv);
                    cliente.fecha_vencimiento = Convert.ToDateTime(fecha);

                    Direccion dir_cliente = new Direccion();
                    dir_cliente.estado = estado;
                    dir_cliente.municipio = municipio;
                    dir_cliente.cp = cp;
                    dir_cliente.calle = calle;
                    dir_cliente.no_exterior = noExt;
                    dir_cliente.colonia = colonia;
                    dir_cliente.id_cliente = cliente.id_cliente;

                    db.Cliente.Add(cliente);
                    db.Direccion.Add(dir_cliente);
                    db.SaveChanges();

                    string[] nombres = cliente.nombre_cliente.ToString().Split(' ');
                    Session["name"] = nombres[0];
                    Session["usr"] = cliente.nombre_cliente;

                    if(Session["crearOrden"] != null)
                    {
                        if (Session["crearOrden"].Equals("pendiente"))
                        {
                            return RedirectToAction("Crearorden", "Pago");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Invalido");
                }
            }
            else
            {
                return RedirectToAction("Invalido");
            }

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
            var direccion = (from dir in db.Direccion
                         where dir.id_cliente == id
                         select dir).ToList();
            ViewBag.direccion = direccion;
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
        //public ActionResult Edit([Bind(Include = "id_cliente,nombre_cliente,app_cliente,apm_cliente,correo_cliente,telefono_cliente,num_tdc,cvv,fecha_vencimiento")] Cliente cliente)
        public ActionResult Edit(string id_cliente,string nombre_cliente, string app_cliente, string apm_cliente, string telefono_cliente, string correo_cliente,string id_direccion, string estado, string municipio, string calle, string colonia, string cp, string noExt, string num_tdc, string cvv, string fecha, string tipoT)
        {

            if (tarjetaValida(num_tdc, tipoT, fecha, cvv))
            {
                Cliente cliente = new Cliente();
                cliente.id_cliente = int.Parse(id_cliente);
                cliente.nombre_cliente = nombre_cliente;
                cliente.app_cliente = app_cliente;
                cliente.apm_cliente = apm_cliente;
                cliente.telefono_cliente = telefono_cliente;
                cliente.correo_cliente = correo_cliente;
                cliente.num_tdc = num_tdc;
                cliente.cvv = int.Parse(cvv);
                cliente.fecha_vencimiento = Convert.ToDateTime(fecha);




                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ActualizaDireccion", routeValues: new { id_direccion = int.Parse(id_direccion), id_cliente = cliente.id_cliente, estado = estado, calle = calle, colonia = colonia, municipio = municipio, cp = cp, noExt = noExt });

            }
            else
            {
                return RedirectToAction("InvalidoUpdate");
            }
        }


        public ActionResult ActualizaDireccion(int id_direccion, int id_cliente, string estado, string calle, string colonia, string municipio, string cp, string noExt)
        {
            Direccion dir = new Direccion();
            dir.id_direccion = id_direccion;
            dir.id_cliente = id_cliente;
            dir.estado = estado;
            dir.municipio = municipio;
            dir.cp = cp;
            dir.calle = calle;
            dir.colonia = colonia;
            dir.no_exterior = noExt;

            db.Entry(dir).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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

        public ActionResult Invalido()
        {
            return View();
        }

        public ActionResult InvalidoUpdate()
        {
            return View();
        }

        public ActionResult IrIndex()
        {
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult BorrarUsuario()
        {
            return RedirectToAction("Delete", "Account");
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

        private bool validaPago(string nombre, string calle, string colonia, string estado, string cp, string tarjeta, string fecha, string cvv)
        {
            return true;
        }
    }
}
