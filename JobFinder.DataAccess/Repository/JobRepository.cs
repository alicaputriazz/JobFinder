using JobFinder.DataAccess.Repository.IRepository;
using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DataAccess.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private ApplicationDbContext _db;

        public JobRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Job job)
        {
            _db.Jobs.Update(job);
        }
    }
}
