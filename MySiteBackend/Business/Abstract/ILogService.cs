
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ILogService
    {
        Log Get(int id);
        List<Log> GetList();
        void Add(Log entity);
        void Update(Log entity);
        void Delete(int id);
    }
}
