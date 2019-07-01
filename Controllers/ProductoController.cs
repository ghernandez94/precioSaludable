using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preciosaludable.Models;

namespace preciosaludable.Controllers
{
    [Route("api/{Controller}")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public ProductoController(preciosaludableContext context)
        {
            _context = context;
        }

        // GET: api/Producto/
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(long id)
        {
            var producto = await _context.Producto
                            .AsNoTracking()
                            .Include(p => p.FarmacoIdFarmacoNavigation)
                                .ThenInclude(f => f.Concentracion)
                                .ThenInclude(c => c.PrincipioActivoIdPrincipioActivoNavigation)
                            .Include(p => p.FarmacoIdFarmacoNavigation)
                                .ThenInclude(f => f.PresentacionIdPresentacionNavigation)
                            .Include(p => p.LaboratorioIdLaboratorioNavigation)
                            .Include(p => p.Detalleprecio)
                                .ThenInclude(dp => dp.SucursalIdSucursalNavigation)
                                    .ThenInclude(s => s.FarmaciaIdFarmaciaNavigation)
                            .Include(p => p.Detalleprecio)
                                .ThenInclude(dp => dp.SucursalIdSucursalNavigation)
                                    .ThenInclude(s => s.ComunaIdComunaNavigation)
                            .SingleOrDefaultAsync(p => p.IdProducto == id);
            
            producto.Detalleprecio = producto.Detalleprecio.OrderBy(dp => dp.PrecioFarmaco).ToList();
            producto.Detalleprecio = producto.Detalleprecio
                .Where(dp => dp.FechaHoraDetalle
                    .Equals(producto.Detalleprecio
                        .Where(aux => dp.SucursalIdSucursal == aux.SucursalIdSucursal)
                        .Max(aux => aux.FechaHoraDetalle)))
                .ToList();
                    

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> AddProducto(Producto producto)
        {
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new {id = producto.IdProducto}, producto);
        }

        // PUT: api/Producto/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(long id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(long id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Estado = false;
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Producto/All
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Producto>>> All()
        {
            return await _context.Producto
                .AsNoTracking()
                .Include(p => p.Detalleprecio)
                .Include(p => p.LaboratorioIdLaboratorioNavigation)
                .Include(p => p.FarmacoIdFarmacoNavigation)
                    .ThenInclude(f => f.PresentacionIdPresentacionNavigation)
                .Where(p => p.Estado.Value)
                .ToListAsync();
        }

        // GET: api/Producto/Buscar/paracetamol
        [HttpGet("Buscar/{s}")]
        public async Task<ActionResult<IEnumerable<Producto>>> Buscar(string s)
        {
            var productos = await _context.Producto
                .AsNoTracking()
                .Where(pro => (pro.NombreComercialProducto.Contains(s) 
                    || pro.FarmacoIdFarmacoNavigation.Concentracion
                        .Where( con => con.PrincipioActivoIdPrincipioActivoNavigation.NombrePrincipioActivo.Contains(s))
                        .Any())
                    && pro.Estado.Value)
                .Select(pro => new Producto{
                    CantidadPresentacion = pro.CantidadPresentacion,
                    Estado = pro.Estado,
                    FarmacoIdFarmaco = pro.FarmacoIdFarmaco,
                    FarmacoIdFarmacoNavigation = pro.FarmacoIdFarmacoNavigation,
                    IdProducto = pro.IdProducto,
                    InverseProductoBioequivalenteNavigation = pro.InverseProductoBioequivalenteNavigation,
                    LaboratorioIdLaboratorio = pro.LaboratorioIdLaboratorio,
                    LaboratorioIdLaboratorioNavigation = pro.LaboratorioIdLaboratorioNavigation,
                    NombreComercialProducto = pro.NombreComercialProducto,
                    ProductoBioequivalente = pro.ProductoBioequivalente,
                    ProductoBioequivalenteNavigation = pro.ProductoBioequivalenteNavigation,
                    Detalleprecio = pro.Detalleprecio
                        .Where(dp => dp.PrecioFarmaco == pro.Detalleprecio
                            .Where(fecha => fecha.FechaHoraDetalle.Equals(pro.Detalleprecio.Max(max => max.FechaHoraDetalle)))
                            .Min(precio => precio.PrecioFarmaco))
                        .Select(dp => new Detalleprecio{
                            IdDetallePrecio = dp.IdDetallePrecio,
                            Estado = dp.Estado,
                            FechaHoraDetalle = dp.FechaHoraDetalle,
                            PrecioFarmaco = dp.PrecioFarmaco,
                            ProductoIdProducto = dp.ProductoIdProducto,
                            SucursalIdSucursal = dp.SucursalIdSucursal,
                            SucursalIdSucursalNavigation = new Sucursal{
                                IdSucursal = dp.SucursalIdSucursalNavigation.IdSucursal,
                                Estado = dp.SucursalIdSucursalNavigation.Estado,
                                FarmaciaIdFarmaciaNavigation = dp.SucursalIdSucursalNavigation.FarmaciaIdFarmaciaNavigation
                            } 
                        })
                        .ToList()
                })
                .ToListAsync();

            // var productos = await _context.Producto
            //     .AsNoTracking()
            //     .Join(_context.Concentracion,
            //         p => p.FarmacoIdFarmaco, 
            //         c => c.FarmacoIdFarmaco,
            //         (p, c) => new {Producto = p, Concentracion = c})
            //     .Where(p => (p.Producto.NombreComercialProducto.Contains(s) 
            //         || p.Concentracion.PrincipioActivoIdPrincipioActivoNavigation.NombrePrincipioActivo.Contains(s))
            //         && p.Producto.Estado.Value)
            //     .Select(p => p.Producto)
            //     .Include(p => p.LaboratorioIdLaboratorioNavigation)
            //     .Include(p => p.FarmacoIdFarmacoNavigation)
            //     .ThenInclude(con => con.Concentracion)
            //     .ThenInclude(pa => pa.PrincipioActivoIdPrincipioActivoNavigation)
            //     .ToListAsync();

            return productos;
        }
    }
}