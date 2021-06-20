using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GardeningF.Models
{
    public class ItemPedidos
    {
        public int idOrd
        {
            get;
            set;
        }

        public Producto Product
        {
            get;
            set;
        }

        public int Cantidad
        {
            get;
            set;
        }
    }
}