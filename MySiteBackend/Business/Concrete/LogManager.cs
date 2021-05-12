
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using Business.Abstract;

namespace Business.Concrete
{
    public class LogManager : ILogService
    {
        private ILogDal _logDal;
        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public Log Get(int id)
        {
            return _logDal.Get(x => x.Id == id);
        }

        public List<Log> GetList()
        {
            return _logDal.GetList();
        }

        public void Add(Log entity)
        {
            _logDal.Add(entity);
        }

        public void Update(Log entity)
        {
            _logDal.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            _logDal.Delete(entity);
        }
    }
}