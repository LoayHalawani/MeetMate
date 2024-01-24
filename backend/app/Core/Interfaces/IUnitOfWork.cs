using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IMeetingRepository Meeting { get; }
        IRoomRepository Room { get; }
        Task<int> CommitAsync();
    }
}
