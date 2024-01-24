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
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(MeetingRoomDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Meeting>> GetAllMeetingsAsync()
        {
            return await MyDbContext.Meetings
                .ToListAsync();
        }

        public Task<Meeting> GetMeetingByIdAsync(int id)
        {
            return MyDbContext.Meetings
                .SingleOrDefaultAsync(a => a.MeetingId == id);
        }

        private MeetingRoomDbContext MyDbContext
        {
            get { return Context as MeetingRoomDbContext; }
        }
    }
}
