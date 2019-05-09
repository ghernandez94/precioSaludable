using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Presentacion
    {
        public Presentacion()
        {
            Farmaco = new HashSet<Farmaco>();
        }

        public int IdPresentacion { get; set; }
        public string NombrePresentacion { get; set; }
        public int UnidadMedidaIdUnidadMedida { get; set; }
        public bool? Estado { get; set; }

        public virtual Unidadmedida UnidadMedidaIdUnidadMedidaNavigation { get; set; }
        public virtual ICollection<Farmaco> Farmaco { get; set; }
    }
}
