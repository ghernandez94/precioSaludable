using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Farmacia
    {
        public Farmacia()
        {
            Sucursal = new HashSet<Sucursal>();
        }

        public int IdFarmacia { get; set; }
        public string Rutfarmacia { get; set; }
        public string NombreFarmacia { get; set; }
        public string TelefonoFarmacia { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Sucursal> Sucursal { get; set; }
    }
}
