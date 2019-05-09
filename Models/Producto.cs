using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Detalleprecio = new HashSet<Detalleprecio>();
            InverseProductoBioequivalenteNavigation = new HashSet<Producto>();
        }

        public long IdProducto { get; set; }
        public string NombreComercialProducto { get; set; }
        public long FarmacoIdFarmaco { get; set; }
        public int LaboratorioIdLaboratorio { get; set; }
        public decimal CantidadPresentacion { get; set; }
        public long? ProductoBioequivalente { get; set; }
        public bool? Estado { get; set; }

        public virtual Farmaco FarmacoIdFarmacoNavigation { get; set; }
        public virtual Laboratorio LaboratorioIdLaboratorioNavigation { get; set; }
        public virtual Producto ProductoBioequivalenteNavigation { get; set; }
        public virtual ICollection<Detalleprecio> Detalleprecio { get; set; }
        public virtual ICollection<Producto> InverseProductoBioequivalenteNavigation { get; set; }
    }
}
