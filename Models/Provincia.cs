using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Provincia
    {
        public Provincia()
        {
            Comuna = new HashSet<Comuna>();
        }

        public long IdProvincia { get; set; }
        public string NombreProvincia { get; set; }
        public long RegionIdRegion { get; set; }

        public virtual Region RegionIdRegionNavigation { get; set; }
        public virtual ICollection<Comuna> Comuna { get; set; }
    }
}
