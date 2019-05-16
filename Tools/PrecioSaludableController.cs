// using System;
// using System.Linq;
// using System.Collections.Generic;
// //using System.Data.Entity;
// using Microsoft.EntityFrameworkCore;
// using System.Linq.Expressions;
// using preciosaludable.Models;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using System.Reflection;
// using System.ComponentModel.DataAnnotations;

// namespace preciosaludable.Controllers
// {
//     public class PrecioSaludableController<TEntity> : ControllerBase where TEntity : class, new()
//     {
//         private readonly preciosaludableContext _dbcontext;

//         protected preciosaludableContext dbcontext { 
//             get{
//                 return _dbcontext;
//             }
//         }

//         protected PrecioSaludableController(preciosaludableContext dbc){
//             _dbcontext = dbc;
//         }

//         [HttpGet]
//         [Route("api/Laboratorio/{id}")]   
//         public async Task<ActionResult<TEntity>> Get(long id)
//         {
//             var entity = await _dbcontext.Set<TEntity>().FindAsync(id);

//             if (entity == null)
//             {
//                 return NotFound();
//             }

//             return entity;
//         }

//         [HttpPost]
//         protected async Task<ActionResult<TEntity>> Add(TEntity entity)
//         {
//             _dbcontext.Set<TEntity>().Add(entity);
//             await _dbcontext.SaveChangesAsync();

//             return CreatedAtAction(nameof(TEntity), entity);
//         }

//         [HttpPut("{id}")]
//         protected async Task<IActionResult> Update(object id, TEntity entity)
//         {
//             PropertyInfo[] properties = entity.GetType().GetProperties();

//             foreach (PropertyInfo property in properties)
//             {
//                 var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute))
//                     as KeyAttribute;

//                 if (attribute != null) // This property has a KeyAttribute
//                 {
//                     // Do something, to read from the property:
//                     object val = property.GetValue(entity);
//                     if (id != val)
//                     {
//                         return BadRequest();
//                     }
//                 }
//             }

//             _dbcontext.Entry(entity).State = EntityState.Modified;
//             await _dbcontext.SaveChangesAsync();

//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         protected async Task<IActionResult> Delete(object id)
//         {
//             var entity = await _dbcontext.Set<TEntity>().FindAsync(id);

//             if (entity == null)
//             {
//                 return NotFound();
//             }

//             entity.GetType().GetProperty("Estado").SetValue(entity, false); 
//             _dbcontext.Entry(entity).State = EntityState.Modified;
//             await _dbcontext.SaveChangesAsync();

//             return NoContent();
//         }
//     }
// }
