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
    public class ProductoController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public ProductoController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/{s}")]
        public async Task<ActionResult<IEnumerable<Producto>>> Buscar(string s)
        {
            return await _context.Producto
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
        }
    }
}