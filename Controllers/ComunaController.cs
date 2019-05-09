using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preciosaludable.Models;

namespace preciosaludable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunaController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public ComunaController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet("{idProvincia}")]
        public async Task<ActionResult<IEnumerable<Comuna>>> GetComunas(int idProvincia)
        {
            return await _context.Comuna
                .Where(p => p.ProvinciaIdProvincia == idProvincia)
                .ToListAsync();
        }
    }
}