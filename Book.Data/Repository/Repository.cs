using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db=db;
            this.dbSet=_db.Set<T>();
            _db.products.Include(u=>u.Category);
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            IQueryable<T> Query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var incprop in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    Query= Query.Include(incprop);
                }
            }
           return Query.ToList();
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            dbSet.RemoveRange(items);
        }

        public T Get(Expression<Func<T, bool>> entity, string? includeproperties = null)
        {
             IQueryable < T > Query = dbSet;
            Query = Query.Where(entity);
            if (!string.IsNullOrEmpty(includeproperties))
            {
                foreach (var incprop in includeproperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                   Query=Query.Include(incprop);
                }
            }
            return Query.FirstOrDefault();
        }
    }
}
