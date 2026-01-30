using System;
using System.Collections.Generic;
using System.Text;
using AutoLot.Dal.Repos.Interfaces;

namespace AutoLot.Dal.Repos.Base
{
    public abstract class BaseViewRepo<T> : IBaseViewRepo<T> where T : class, new()
    {
        private readonly bool _disposeContext;
        private bool isDisposed;
        public DbSet<T> Table;
        public ApplicationDbContext Context { get; }

        public BaseViewRepo(ApplicationDbContext context)
        {
            Context = context;
            Table = context.Set<T>();  
            _disposeContext = false;
        }

        internal BaseViewRepo(DbContextOptions<ApplicationDbContext> options) : this (new ApplicationDbContext(options))
        {
            _disposeContext = true;
        }
         public  IEnumerable<T> ExecuteSqlString(string sql)
        {
            return Table.FromSqlRaw(sql);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Table.AsQueryable();
        }

        public virtual IEnumerable<T> GetAllIgnoreQueryFilters()
        {
            return Table.AsQueryable().IgnoreQueryFilters();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if(_disposeContext)
                    Context.Dispose();
                }
                isDisposed = true;
            }
        }

        ~BaseViewRepo()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
