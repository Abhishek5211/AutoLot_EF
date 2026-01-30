using AutoLot.Dal.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    public class CarRepo : TemporalTableBaseRepo<Car>, ICarRepo
    {
        public CarRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal CarRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public IEnumerable<Car> GetAllBy(int makeId)
        {
            return BuildQuery().Where(x => x.MakeId == makeId);
        }

        public override Car Find(int? id)
                                            => Table
                                            .IgnoreQueryFilters()
                                            .Where(x => x.Id == id)
                                            .Include(m => m.MakeNavigation)
                                            .FirstOrDefault();

        public string GetNickName(int id)
        {
            var inparameter = new SqlParameter
            {
                ParameterName = "@carId",
                SqlDbType = SqlDbType.Int,
                Value = id

            };

            var outparameter = new SqlParameter
            {
                ParameterName = "@NickName",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Output
            };

            ExecuteParameterizedQuery("(\"EXEC [dbo].[GetNickName] @carId, @NickName OUTPUT\"", new[] { inparameter, outparameter });
            return outparameter.Value.ToString();
        }
        public override IEnumerable<Car> GetAll()
=> BuildQuery();
        public override IEnumerable<Car> GetAllIgnoreQueryFilters()
        => BuildQuery().IgnoreQueryFilters();
        internal IOrderedQueryable<Car> BuildQuery() => Table.Include(x => x.MakeNavigation).OrderBy(x => x.NickName);
    }
}
