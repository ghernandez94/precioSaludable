using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Tipousuario
    {
        public Tipousuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdTipoUsuario { get; set; }
        public string NombreTipoUsuario { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
