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
    public class PrincipioactivoController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public PrincipioactivoController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Principioactivo>>> GetPrincipiosactivos()
        {
            return await _context.Principioactivo
                .AsNoTracking()
                .Where(p => p.Estado.Value)
                .ToListAsync();
        }
    }
}