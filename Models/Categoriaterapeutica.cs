using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Categoriaterapeutica
    {
        public Categoriaterapeutica()
        {
            Detallecategoriaterapeutica = new HashSet<Detallecategoriaterapeutica>();
        }

        public int IdCategoriaTerapeutica { get; set; }
        public string NombreCategoriaTerapeutica { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Detallecategoriaterapeutica> Detallecategoriaterapeutica { get; set; }
    }
}
