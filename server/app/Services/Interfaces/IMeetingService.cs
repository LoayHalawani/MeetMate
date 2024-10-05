using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMeetingService
    {
        Task<IEnumerable<Meeting>> GetAllMeetings();
        Task<Meeting> GetMeetingById(int id);
        Task<Meeting> CreateMeeting(Meeting newMeeting);
        Task UpdateMeeting(Meeting meetingToBeUpdated, Meeting meeting);
        Task DeleteMeeting(Meeting meeting);
    }
}
