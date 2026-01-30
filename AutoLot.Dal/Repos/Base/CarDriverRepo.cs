using AutoLot.Dal.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    public class CarDriverRepo : TemporalTableBaseRepo<CarDriver>, ICarDriverRepo
    {
        public CarDriverRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal CarDriverRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
