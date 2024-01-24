using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MeetingRoomDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await MyDbContext.Employees
                .ToListAsync();
        }

        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return MyDbContext.Employees
                .SingleOrDefaultAsync(a => a.EmployeeId == id);
        }

        private MeetingRoomDbContext MyDbContext
        {
            get { return Context as MeetingRoomDbContext; }
        }
    }
}
