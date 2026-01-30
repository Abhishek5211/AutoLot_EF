using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Interfaces
{
    public interface ICarRepo : ITemporalTableBaseRepo<Car>
    {
        IEnumerable<Car> GetAllBy(int makeId);
        string GetNickName(int id);
    }
}
