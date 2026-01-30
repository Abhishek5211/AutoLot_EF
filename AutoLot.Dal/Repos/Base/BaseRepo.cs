using AutoLot.Dal.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    public class BaseRepo<T> :  BaseViewRepo<T>, IBaseRepo<T> where T : BaseEntity, new()
    {
        public BaseRepo(ApplicationDbContext context) : base(context)
        {
        }
        public BaseRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual T Find(int? id)
        {
            return Table.Find(id);
        }

        public T FindAsNoTracking(int id)
        {
            return Table.AsNoTrackingWithIdentityResolution().FirstOrDefault(x => x.Id == id);
        }

        public T FindIgnoreQueryFilters(int id)
        {
            return Table.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);
        }

        public void ExecuteParameterizedQuery(string sql, object[] sqlParametersObjects)
        {
            Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);
        }

        public int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            var entity = new T { Id = id, TimeStamp = timeStamp };
            Context.Entry(entity).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        public int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CustomException("Database error occured", ex);
            }
        }
    }
}
