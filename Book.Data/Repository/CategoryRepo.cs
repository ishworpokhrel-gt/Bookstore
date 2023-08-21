using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class CategoryRepo : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepo(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public void save()
        {
            _db.SaveChanges();
        }

        public void update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
