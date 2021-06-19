using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardeningF.Models;

namespace GardeningF.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private contextoGardeningF db = new contextoGardeningF();
        // GET: Usuario
        public ActionResult Index(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                string correo = email;
                string rol = "";

                var query_empleado = (from emp in db.Empleado
                                      where emp.email_empleado == email
                                      select emp).ToList();

                if (query_empleado.Count > 0)
                {
                    var empleado = query_empleado.FirstOrDefault<Empleado>();
                    string[] nombre = empleado.nombre_empleado.ToString().Split(' ');
                    Session["name"] = nombre[0];
                    rol = empleado.rol_empleado.ToString().TrimEnd();
                }
                else
                {
                    var query_cliente = (from cl in db.Cliente
                                         where cl.correo_cliente == correo
                                         select cl).ToList();

                    if (query_cliente.Count > 0)
                    {
                        var cliente = query_cliente.FirstOrDefault<Cliente>();
                        string[] nombre = cliente.nombre_cliente.ToString().Split(' ');
                        Session["name"] = nombre[0];
                        Session["idCliente"] = cliente.id_cliente;
                        Session["email"] = correo;
                        rol = "cliente";
                    }
                }

                switch (rol)
                {
                    case "comprador":
                        return RedirectToAction("Index", "Compras");
                    case "enviador":
                        return RedirectToAction("Index", "Envios");
                    case "admin":
                        return RedirectToAction("Index", "Admin");
                    case "cliente":
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}