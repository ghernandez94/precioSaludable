using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Comuna
    {
        public Comuna()
        {
            Sucursal = new HashSet<Sucursal>();
        }

        public long IdComuna { get; set; }
        public string NombreComuna { get; set; }
        public long ProvinciaIdProvincia { get; set; }

        public virtual Provincia ProvinciaIdProvinciaNavigation { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }
    }
}
