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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(MeetingRoomDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await MyDbContext.Companies
                .ToListAsync();
        }

        public Task<Company> GetCompanyByIdAsync(int id)
        {
            return MyDbContext.Companies
                .SingleOrDefaultAsync(a => a.CompanyId == id);
        }

        private MeetingRoomDbContext MyDbContext
        {
            get { return Context as MeetingRoomDbContext; }
        }
    }
}
