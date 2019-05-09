using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using preciosaludable.Models;

namespace preciosaludable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleprecioController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public DetalleprecioController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/{IdProducto}")]
        public async Task<ActionResult<IEnumerable<Detalleprecio>>> GetPreciosActuales(long IdProducto)
        {
            return await _context.Detalleprecio
                .AsNoTracking()
                .Include(dp => dp.SucursalIdSucursalNavigation)
                .ThenInclude(s => s.FarmaciaIdFarmaciaNavigation)
                .Where(dp => dp.ProductoIdProducto == IdProducto
                    && dp.Estado.Value)
                .GroupBy(dp => dp.SucursalIdSucursal, 
                    (key, group) => group
                        .Where(tmp => tmp.FechaHoraDetalle.Equals(group.Max(dp => dp.FechaHoraDetalle)))
                        .FirstOrDefault())
                .ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Detalleprecio>>> GetHistorial(long IdProducto, long IdSucursal){
            return await _context.Detalleprecio
                .AsNoTracking()
                .Include(dp => dp.SucursalIdSucursalNavigation)
                .Where(dp => dp.ProductoIdProducto == IdProducto
                    && dp.SucursalIdSucursal == IdSucursal
                    && dp.Estado.Value)
                .OrderByDescending(dp => dp.FechaHoraDetalle)
                .ToListAsync();
        }
    }
}