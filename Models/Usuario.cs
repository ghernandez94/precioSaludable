using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Detalleprecio = new HashSet<Detalleprecio>();
        }

        public long IdUsuario { get; set; }
        public string NombresUsuario { get; set; }
        public string ApPaternoUsuario { get; set; }
        public string ApMaternoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string PasswordUsuario { get; set; }
        public int TipoUsuarioIdTipoUsuario { get; set; }
        public bool? Estado { get; set; }

        public virtual Tipousuario TipoUsuarioIdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<Detalleprecio> Detalleprecio { get; set; }
    }
}
