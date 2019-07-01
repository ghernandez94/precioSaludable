using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preciosaludable.Models;

namespace preciosaludable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmacoController : ControllerBase
    {
        private readonly preciosaludableContext _context;

        public FarmacoController(preciosaludableContext context)
        {
            _context = context;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Farmaco>>> All()
        {
            return await _context.Farmaco
                .AsNoTracking()
                .Where(p => p.Estado.Value)
                .Select(f => new Farmaco{
                    IdFarmaco = f.IdFarmaco,
                    Estado = f.Estado,
                    PresentacionIdPresentacion = f.PresentacionIdPresentacion,
                    PresentacionIdPresentacionNavigation = new Presentacion{
                        IdPresentacion = f.PresentacionIdPresentacionNavigation.IdPresentacion,
                        Estado = f.PresentacionIdPresentacionNavigation.Estado,
                        NombrePresentacion = f.PresentacionIdPresentacionNavigation.NombrePresentacion,
                        UnidadMedidaIdUnidadMedida = f.PresentacionIdPresentacionNavigation.UnidadMedidaIdUnidadMedida,
                        UnidadMedidaIdUnidadMedidaNavigation = new Unidadmedida{
                            IdUnidadMedida = f.PresentacionIdPresentacionNavigation.UnidadMedidaIdUnidadMedidaNavigation.IdUnidadMedida,
                            Estado = f.PresentacionIdPresentacionNavigation.UnidadMedidaIdUnidadMedidaNavigation.Estado,
                            NombreUnidadMedida = f.PresentacionIdPresentacionNavigation.UnidadMedidaIdUnidadMedidaNavigation.NombreUnidadMedida,
                            SimboloUnidadMedida = f.PresentacionIdPresentacionNavigation.UnidadMedidaIdUnidadMedidaNavigation.SimboloUnidadMedida
                        }
                    },
                    Concentracion = f.Concentracion.Select(c => new Concentracion{
                        IdConcentracion = c.IdConcentracion,
                        Cantidad = c.Cantidad,
                        Estado = c.Estado,
                        FarmacoIdFarmaco = c.FarmacoIdFarmaco,
                        PrincipioActivoIdPrincipioActivo = c.PrincipioActivoIdPrincipioActivo,
                        UnidadMedidaIdUnidadMedida = c.UnidadMedidaIdUnidadMedida,
                        UnidadMedidaIdUnidadMedidaNavigation = new Unidadmedida{
                            IdUnidadMedida = c.UnidadMedidaIdUnidadMedidaNavigation.IdUnidadMedida,
                            Estado = c.UnidadMedidaIdUnidadMedidaNavigation.Estado,
                            NombreUnidadMedida = c.UnidadMedidaIdUnidadMedidaNavigation.NombreUnidadMedida,
                            SimboloUnidadMedida = c.UnidadMedidaIdUnidadMedidaNavigation.SimboloUnidadMedida
                        },
                        PrincipioActivoIdPrincipioActivoNavigation = new Principioactivo{
                            IdPrincipioActivo = c.PrincipioActivoIdPrincipioActivoNavigation.IdPrincipioActivo,
                            NombrePrincipioActivo = c.PrincipioActivoIdPrincipioActivoNavigation.NombrePrincipioActivo,
                            Estado = c.PrincipioActivoIdPrincipioActivoNavigation.Estado
                        }
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}