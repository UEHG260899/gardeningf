using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GardeningF.Models
{
    public class ProdCarro
    {
        private contextoGardeningF db = new contextoGardeningF();
        private List<Producto> products;

        public ProdCarro()
        {
            products = db.Producto.ToList();
        }

        public List<Producto> findAll()
        {
            return this.products;
        }

        public Producto find(int id)
        {
            Producto pp = this.products.Single(p => p.id_producto.Equals(id));
            return pp;

        }
    }
}