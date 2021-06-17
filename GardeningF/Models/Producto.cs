//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GardeningF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.OrdenProducto = new HashSet<OrdenProducto>();
        }
    
        public int id_producto { get; set; }
        public int id_subcategoria { get; set; }
        public string nombre_producto { get; set; }
        public string descripcion_producto { get; set; }
        public Nullable<decimal> precio_producto { get; set; }
        public Nullable<System.DateTime> ultima_actualizacion { get; set; }
        public string imagen_producto { get; set; }
        public Nullable<int> cantidad_existencia { get; set; }
        public Nullable<int> stock { get; set; }
        public Nullable<int> descuento { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenProducto> OrdenProducto { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
    }
}
