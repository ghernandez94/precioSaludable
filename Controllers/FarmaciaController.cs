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
    public class FarmaciaController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public FarmaciaController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Farmacia>>> All()
        {
            return await _context.Farmacia
                .AsNoTracking()
                .Include(f => f.Sucursal)
                .Where(p => p.Estado.Value)
                .ToListAsync();
        }
    }
}