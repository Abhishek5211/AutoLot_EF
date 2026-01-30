using AutoLot.Dal.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    internal class MakeRepo : TemporalTableBaseRepo<Make>, IMakeRepo
    {
        public MakeRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal MakeRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        internal IOrderedQueryable<Make> BuildQuery()
=> Table.OrderBy(m => m.Name);

        public override IEnumerable<Make> GetAll()
        {
            return BuildQuery();
        }

        public override IEnumerable<Make> GetAllIgnoreQueryFilters()
=> BuildQuery().IgnoreQueryFilters();

    }
}
