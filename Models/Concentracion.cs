using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Concentracion
    {
        public long IdConcentracion { get; set; }
        public decimal Cantidad { get; set; }
        public int UnidadMedidaIdUnidadMedida { get; set; }
        public long PrincipioActivoIdPrincipioActivo { get; set; }
        public long FarmacoIdFarmaco { get; set; }
        public bool? Estado { get; set; }

        public virtual Farmaco FarmacoIdFarmacoNavigation { get; set; }
        public virtual Principioactivo PrincipioActivoIdPrincipioActivoNavigation { get; set; }
        public virtual Unidadmedida UnidadMedidaIdUnidadMedidaNavigation { get; set; }
    }
}
