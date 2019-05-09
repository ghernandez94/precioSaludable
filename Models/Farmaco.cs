using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Farmaco
    {
        public Farmaco()
        {
            Concentracion = new HashSet<Concentracion>();
            Producto = new HashSet<Producto>();
        }

        public long IdFarmaco { get; set; }
        public int PresentacionIdPresentacion { get; set; }
        public bool? Estado { get; set; }

        public virtual Presentacion PresentacionIdPresentacionNavigation { get; set; }
        public virtual ICollection<Concentracion> Concentracion { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
