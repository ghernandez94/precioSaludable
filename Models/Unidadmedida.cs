using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Unidadmedida
    {
        public Unidadmedida()
        {
            Concentracion = new HashSet<Concentracion>();
            Presentacion = new HashSet<Presentacion>();
        }

        public int IdUnidadMedida { get; set; }
        public string NombreUnidadMedida { get; set; }
        public string SimboloUnidadMedida { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Concentracion> Concentracion { get; set; }
        public virtual ICollection<Presentacion> Presentacion { get; set; }
    }
}
