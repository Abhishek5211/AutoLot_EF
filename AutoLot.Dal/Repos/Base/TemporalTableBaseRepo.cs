using AutoLot.Dal.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    public class TemporalTableBaseRepo<T> : BaseRepo<T>, ITemporalTableBaseRepo<T> where T : BaseEntity , new()
    {
        public TemporalTableBaseRepo(ApplicationDbContext context) : base(context)
        {

        }

        public TemporalTableBaseRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public IEnumerable<TemporalViewModel<T>> GetAllHistory()
        {
            return ExecuteQuery(Table.TemporalAll());
        }

        public IEnumerable<TemporalViewModel<T>> GetHistoryBetween(DateTime startDateTime, DateTime endDateTime)
        {
            return ExecuteQuery(Table.TemporalBetween(startDateTime,endDateTime));
        }

        public IEnumerable<TemporalViewModel<T>> GetHistoryContainedIn(DateTime startDateTime, DateTime endDateTime)
        {
            return ExecuteQuery(Table.TemporalContainedIn(startDateTime, endDateTime));
        }

        public IEnumerable<TemporalViewModel<T>> GetHistoryFromTo(DateTime startDateTime, DateTime endDateTime)
        {
            return ExecuteQuery(Table.TemporalFromTo(startDateTime, endDateTime));
        }

        internal DateTime ConvertToUtc(DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local);
        }

        internal static IEnumerable<TemporalViewModel<T>> ExecuteQuery(IQueryable<T> query)
        {
            return query.OrderBy(e => EF.Property<DateTime>(e, "ValidFrom")).Select
                (e => new TemporalViewModel<T>
                {
                    Entity = e,
                    ValidFrom = EF.Property<DateTime>(e, "ValidFrom"),
                    ValidTo = EF.Property<DateTime>(e, "ValidTo")
                }
                );
        }

        public IEnumerable<TemporalViewModel<T>> GetHistoryAsOf(DateTime dateTime)
        {
            return ExecuteQuery(Table.TemporalAsOf(dateTime));
        }
    }
}
