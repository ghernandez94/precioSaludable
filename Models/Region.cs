using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Region
    {
        public Region()
        {
            Provincia = new HashSet<Provincia>();
        }

        public long IdRegion { get; set; }
        public string NombreRegion { get; set; }

        public virtual ICollection<Provincia> Provincia { get; set; }
    }
}
