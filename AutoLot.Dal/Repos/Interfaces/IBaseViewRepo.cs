using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Interfaces
{
    public interface IBaseViewRepo<T> : IDisposable where T : class, new()
    {
        ApplicationDbContext Context { get; }
        IEnumerable<T> ExecuteSqlString(string sql);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIgnoreQueryFilters();

       
    }
}
