using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DataAccess.Repository.IRepository
{
    public interface IWorkingMethodRepository : IRepository<WorkingMethod>
    {
        void Update(WorkingMethod workingMethod);
    }
}
