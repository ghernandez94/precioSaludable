using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preciosaludable.Models;
using preciosaludable.Tools.Geolocation;


namespace preciosaludable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public SucursalController(preciosaludableContext context)
        {
            _context = context;
        }

        // GET: api/Sucursal/All
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Sucursal>>> All()
        {
            return await _context.Sucursal
                .AsNoTracking()
                .Where(p => p.Estado.Value)
                .ToListAsync();
        }

        // GET: api/Sucursal/All/1
        [HttpGet("All/{idFarmacia}")]
        public async Task<ActionResult<IEnumerable<Sucursal>>> ByDrugstore(int idFarmacia)
        {
            return await _context.Sucursal
                .AsNoTracking()
                .Include(s => s.ComunaIdComunaNavigation)
                .Where(s => s.FarmaciaIdFarmacia == idFarmacia && s.Estado.Value)
                .ToListAsync();
        }

        //By Name or Address
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Sucursal>>> Buscar(string s)
        {
            List<Sucursal> result = new List<Sucursal>();

            if(s != null){
                var query = _context.Sucursal
                    .AsNoTracking()
                    .Include(dp => dp.FarmaciaIdFarmaciaNavigation)
                    .Where(dp => dp.Estado.Value);

                if(s.Trim().Length > 2){
                    query = query.Where(dp => dp.FarmaciaIdFarmaciaNavigation.NombreFarmacia.Contains(s)
                        || dp.DireccionSucursal.Contains(s));
                }else{
                    query = query.Where(dp => dp.FarmaciaIdFarmaciaNavigation.NombreFarmacia.StartsWith(s)
                        || dp.DireccionSucursal.StartsWith(s));
                }
                
                result = await query
                    .ToListAsync();
            }
            return result;
        }

        //By Coords
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Sucursal>>> ByLocation(double latitud, double longitud, int proximidad = 10000)
        {
            Location location = new Location(latitud, longitud);
            
            return await _context.Sucursal
                .AsNoTracking()
                .Include(dp => dp.FarmaciaIdFarmaciaNavigation)
                .Include(dp => dp.ComunaIdComunaNavigation)
                .Where(dp => location.GetDistancia(new Location(dp.Latitud, dp.Longitud)) < proximidad
                    && dp.Estado.Value)
                .ToListAsync();
        }
    }
}