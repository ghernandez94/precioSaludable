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
    public class LaboratorioController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public LaboratorioController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laboratorio>>> GetLaboratorios()
        {
            return await _context.Laboratorio
                .AsNoTracking()
                .Where(p => p.Estado.Value)
                .ToListAsync();
        }
    }
}