// using System;
// using System.Linq;
// using System.Collections.Generic;
// //using System.Data.Entity;
// using Microsoft.EntityFrameworkCore;
// using System.Linq.Expressions;
// using preciosaludable.Models;


// namespace preciosaludable.Tools
// {
//     public class GenericDAO<TEntity> where TEntity : class, new()
//     {
//         private readonly DbContext _dbcontext;
//         public GenericDAO(DbContext dbcontext){
//             _dbcontext = dbcontext;
//         }

//         public TEntity Agregar(TEntity te)
//         {
//             object result = null;

//             try
//             {
//                 //_dbcontext.Entry(t).State = EntityState.Added
//                 result = _dbcontext.Set<TEntity>().Add(te);
//                 _dbcontext.SaveChanges();
//             }
//             catch (Exception)
//             {
//                 //Log
//             }

//             return (TEntity)result;
//         }

//         public TEntity Eliminar(TEntity te)
//         {
//             object result = null;

//             try
//             {
//                 te.GetType().GetProperty("Estado").SetValue(te, false);                
//                 //result = _dbcontext.Set<TEntity>().Remove(te);
//                 _dbcontext.SaveChanges();
//             }
//             catch (Exception)
//             {
//                 //Log
//             }

//             return (TEntity)result;
//         }

//         public bool Actualizar(TEntity te)
//         {
//             try
//             {
//                 _dbcontext.Entry(te).State = EntityState.Modified;
//                 _dbcontext.SaveChanges();
//                 return true;
//             }
//             catch (Exception)
//             {
//                 return false;
//                 //Log
//             }
//         }

//         public TEntity Buscar(params object[] keyValues)
//         {
//             object result = null;
//             try
//             {
//                 result = _dbcontext.Set<TEntity>().Find(keyValues);
//             }
//             catch (Exception)
//             {
//                 //Log
//             }

//             return (TEntity)result;

//         }

//         public virtual IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
//         {
//             List<TEntity> list;

//             IQueryable<TEntity> dbQuery = _dbcontext.Set<TEntity>();

//             //Apply eager loading
//             foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
//                 dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

//             list = dbQuery
//                 //.AsNoTracking()
//                 .ToList<TEntity>();
//             return list;
//         }

//         public virtual IList<TEntity> GetList(Func<TEntity, bool> where,
//              params Expression<Func<TEntity, object>>[] navigationProperties)
//         {
//             List<TEntity> list;

//             IQueryable<TEntity> dbQuery = _dbcontext.Set<TEntity>();

//             //Apply eager loading
//             foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
//                 dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

//             list = dbQuery
//                 .AsNoTracking()
//                 .Where(where)
//                 .ToList<TEntity>();
//             return list;
//         }

//         public virtual TEntity GetSingle(Func<TEntity, bool> where,
//              params Expression<Func<TEntity, object>>[] navigationProperties)
//         {
//             TEntity item = null;

//             IQueryable<TEntity> dbQuery = _dbcontext.Set<TEntity>();

//             //Apply eager loading
//             foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
//                 dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

//             item = dbQuery
//                 .AsNoTracking() //Don't track any changes for the selected item
//                 .FirstOrDefault(where); //Apply where clause
//             return item;
//         }


//         //Aún no se usan, pero pueden ser de utilidad.
//         // private string[] GetKeyNames(DbContext context)
//         // {
//         //     ObjectSet<TEntity> objectSet = ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<TEntity>();
//         //     string[] keyNames = objectSet.EntitySet.ElementType.KeyMembers
//         //                                                        .Select(k => k.Name)
//         //                                                        .ToArray();
//         //     return keyNames;
//         // }

//         // private object[] GetKeys(TEntity entity, DbContext context)
//         // {
//         //     var keyNames = GetKeyNames(context);
//         //     Type type = typeof(TEntity);

//         //     object[] keys = new object[keyNames.Length];
//         //     for (int i = 0; i < keyNames.Length; i++)
//         //     {
//         //         keys[i] = type.GetProperty(keyNames[i]).GetValue(entity, null);
//         //     }
//         //     return keys;
//         // }
//     }
// }
