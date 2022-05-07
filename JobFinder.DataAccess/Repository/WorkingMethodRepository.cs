using JobFinder.DataAccess.Repository.IRepository;
using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DataAccess.Repository
{
    public class WorkingMethodRepository : Repository<WorkingMethod>, IWorkingMethodRepository
    {
        private ApplicationDbContext _db;

        public WorkingMethodRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(WorkingMethod obj)
        {
            _db.WorkingMethods.Update(obj);
        }
    }
}
