using JobFinder.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            WorkingMethod = new WorkingMethodRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IWorkingMethodRepository WorkingMethod { get; private set; }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
