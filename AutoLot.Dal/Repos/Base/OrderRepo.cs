using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    internal class OrderRepo : TemporalTableBaseRepo<Order>, IOrderRepo
    {
        public OrderRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal OrderRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
