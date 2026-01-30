using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Interfaces
{
    public interface ITemporalTableBaseRepo<T> : IBaseRepo<T> where T : BaseEntity, new()
    {
        IEnumerable<TemporalViewModel<T>> GetAllHistory();
        IEnumerable<TemporalViewModel<T>> GetHistoryAsOf(DateTime dateTime);
        IEnumerable<TemporalViewModel<T>> GetHistoryBetween(DateTime startDateTime, DateTime endDateTime);
        IEnumerable<TemporalViewModel<T>> GetHistoryContainedIn(DateTime startDateTime, DateTime endDateTime);
        IEnumerable<TemporalViewModel<T>> GetHistoryFromTo(DateTime startDateTime, DateTime endDateTime);     
    }
}
