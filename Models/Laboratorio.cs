using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Laboratorio
    {
        public Laboratorio()
        {
            Producto = new HashSet<Producto>();
        }

        public int IdLaboratorio { get; set; }
        public string Rutlaboratorio { get; set; }
        public string NombreLaboratorio { get; set; }
        public string TelefonoLaboratorio { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
