using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? IncludeProperty = null);
        T Get(Expression<Func<T, bool>> entity,string? IncludeProperty = null);
        void Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);

    }
}
