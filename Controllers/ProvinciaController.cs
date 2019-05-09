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
    public class ProvinciaController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public ProvinciaController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet("{idRegion}")]
        public async Task<ActionResult<IEnumerable<Provincia>>> GetProvincias(int idRegion)
        {
            return await _context.Provincia
                .Where(p => p.RegionIdRegion == idRegion)
                .ToListAsync();
        }
    }
}