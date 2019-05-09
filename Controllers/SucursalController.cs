using System;
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

        //By Name or Address
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales(string s)
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
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursalesByLocation(double latitud, double longitud, int proximidad = 10000)
        {
            Location location = new Location(latitud, longitud);
            
            return await _context.Sucursal
                .AsNoTracking()
                .Include(dp => dp.FarmaciaIdFarmaciaNavigation)
                .Where(dp => location.GetDistancia(new Location(dp.Latitud, dp.Longitud)) < proximidad
                    && dp.Estado.Value)
                .ToListAsync();
        }
    }
}