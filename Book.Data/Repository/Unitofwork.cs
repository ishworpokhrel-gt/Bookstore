using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class Unitofwork : IUnitofwork

    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository categoryunit { get; private set; }
        public IProduct productunit { get; private set; }
        public Unitofwork(ApplicationDbContext db)
        {
            _db=db;
            categoryunit=new CategoryRepo(_db);
            productunit = new ProductRepository(_db);
        }
        

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
