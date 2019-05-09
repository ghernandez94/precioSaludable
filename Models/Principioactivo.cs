using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Principioactivo
    {
        public Principioactivo()
        {
            Concentracion = new HashSet<Concentracion>();
            Detallecategoriaterapeutica = new HashSet<Detallecategoriaterapeutica>();
        }

        public long IdPrincipioActivo { get; set; }
        public string NombrePrincipioActivo { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Concentracion> Concentracion { get; set; }
        public virtual ICollection<Detallecategoriaterapeutica> Detallecategoriaterapeutica { get; set; }
    }
}
