using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GardeningF.Models
{
    public class Item
    {
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