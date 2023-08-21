using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository.IRepository
{
    public interface IUnitofwork
    {
        public ICategoryRepository categoryunit { get; }
        public IProduct productunit { get; }

        void save();
    }
}
