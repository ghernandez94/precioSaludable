using System;
using System.Collections.Generic;

namespace preciosaludable.Models
{
    public partial class Detalleprecio
    {
        public long IdDetallePrecio { get; set; }
        public DateTime FechaHoraDetalle { get; set; }
        public long PrecioFarmaco { get; set; }
        public long ProductoIdProducto { get; set; }
        public long SucursalIdSucursal { get; set; }
        public long UsuarioIdUsuario { get; set; }
        public bool? Estado { get; set; }

        public virtual Producto ProductoIdProductoNavigation { get; set; }
        public virtual Sucursal SucursalIdSucursalNavigation { get; set; }
        public virtual Usuario UsuarioIdUsuarioNavigation { get; set; }
    }
}
