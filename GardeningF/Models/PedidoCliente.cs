using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GardeningF.Models
{
    public class PedidoCliente
    {
        private contextoGardeningF db = new contextoGardeningF();
        private List<OrdenProducto> detalle_orden;

        public PedidoCliente()
        {
            detalle_orden = db.OrdenProducto.ToList();
        }

        public OrdenCliente Orden
        {
            get;
            set;
        }

        public string Fecha
        {
            get;
            set;
        }

        public string Envio
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Total
        {
            get;
            set;
        }

        public List<OrdenProducto> ordenProductos
        {
            get;
            set;
        }
    }
}