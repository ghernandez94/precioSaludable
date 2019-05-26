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
    [Route("api/{Controller}")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public ProductoController(preciosaludableContext context)
        {
            _context = context;
        }

        // GET: api/Producto/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(long id)
        {
            var producto = await _context.Producto.FindAsync(id);

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
            var prd = _context.Producto.Add(producto);
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
                .Where(p => p.Estado.Value)
                .ToListAsync();
        }

        // GET: api/Producto/Buscar/paracetamol
        [HttpGet("Buscar/{s}")]
        public async Task<ActionResult<IEnumerable<Producto>>> Buscar(string s)
        {
            var productos = await _context.Producto
                .AsNoTracking()
                .Join(_context.Concentracion,
                    p => p.FarmacoIdFarmaco, 
                    c => c.FarmacoIdFarmaco,
                    (p, c) => new {Producto = p, Concentracion = c})
                .Where(p => (p.Producto.NombreComercialProducto.Contains(s) 
                    || p.Concentracion.PrincipioActivoIdPrincipioActivoNavigation.NombrePrincipioActivo.Contains(s))
                    && p.Producto.Estado.Value)
                .Select(p => p.Producto)
                .Include(p => p.LaboratorioIdLaboratorioNavigation)
                .Include(p => p.FarmacoIdFarmacoNavigation)
                .ThenInclude(con => con.Concentracion)
                .ThenInclude(pa => pa.PrincipioActivoIdPrincipioActivoNavigation)
                .ToListAsync();

            return productos;
        }
    }
}