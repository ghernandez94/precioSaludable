using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Detalleprecio = new HashSet<Detalleprecio>();
        }

        public long IdSucursal { get; set; }
        public int FarmaciaIdFarmacia { get; set; }
        public long ComunaIdComuna { get; set; }
        public string DireccionSucursal { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string TelefonoSucursal { get; set; }
        public bool? Estado { get; set; }

        public virtual Comuna ComunaIdComunaNavigation { get; set; }
        public virtual Farmacia FarmaciaIdFarmaciaNavigation { get; set; }
        public virtual ICollection<Detalleprecio> Detalleprecio { get; set; }
    }
}
