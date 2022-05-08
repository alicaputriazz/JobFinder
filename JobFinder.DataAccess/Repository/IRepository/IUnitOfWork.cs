using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IWorkingMethodRepository WorkingMethod { get; }
        IJobRepository Job { get; }

        void Save();
    }
}
