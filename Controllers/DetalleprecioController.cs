using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        // GET: api/Detalleprecio/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalleprecio>> GetDetalleprecio(long id)
        {
            var detalleprecio = await _context.Detalleprecio.FindAsync(id);

            if (detalleprecio == null)
            {
                return NotFound();
            }

            return detalleprecio;
        }

        // POST: api/Detalleprecio
        [HttpPost]
        public async Task<ActionResult<Detalleprecio>> AddDetalleprecio(Detalleprecio detalleprecio)
        {
            detalleprecio.UsuarioIdUsuario = 1;
            detalleprecio.FechaHoraDetalle = DateTime.Now;
            _context.Detalleprecio.Add(detalleprecio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleprecio", new {id = detalleprecio.IdDetallePrecio}, detalleprecio);
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

        [HttpGet("historial/{idProducto}/{idSucursal}")]
        public async Task<ActionResult<IEnumerable<Detalleprecio>>> GetHistorial(long IdProducto, long IdSucursal){
            return await _context.Detalleprecio
                .AsNoTracking()
                .Include(dp => dp.SucursalIdSucursalNavigation)
                    .ThenInclude(suc => suc.FarmaciaIdFarmaciaNavigation)
                .Include(dp => dp.SucursalIdSucursalNavigation)
                    .ThenInclude(suc => suc.ComunaIdComunaNavigation)
                .Where(dp => dp.ProductoIdProducto == IdProducto
                    && dp.SucursalIdSucursal == IdSucursal
                    && dp.Estado.Value)
                .OrderBy(dp => dp.FechaHoraDetalle)
                .ToListAsync();
        }
    }
}