using Core.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<T,TContext> : IEntityRepository<T>
       where T : class, IEntity, new()
         where TContext : DbContext
    {
        protected readonly TContext _context;
        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefault(filter);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? _context.Set<T>().ToList()
                : _context.Set<T>().Where(filter).ToList();

        }

        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? _context.Set<T>().ToList()
                : _context.Set<T>().Where(filter).ToList();

        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
