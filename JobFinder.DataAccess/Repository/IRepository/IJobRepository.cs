using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DataAccess.Repository.IRepository
{
    public interface IJobRepository : IRepository<Job>
    {
        void Update(Job job);
    }
}
