using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Detallecategoriaterapeutica
    {
        public long IdDetalleCategoria { get; set; }
        public int CategoriaTerapeuticaIdCategoriaTerapeutica { get; set; }
        public long PrincpioActivoIdPrincipioActivo { get; set; }

        public virtual Categoriaterapeutica CategoriaTerapeuticaIdCategoriaTerapeuticaNavigation { get; set; }
        public virtual Principioactivo PrincpioActivoIdPrincipioActivoNavigation { get; set; }
    }
}
