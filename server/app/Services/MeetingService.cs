using Core.Interfaces;
using Core.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeetingService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Meeting> CreateMeeting(Meeting newMeeting)
        {
            await _unitOfWork.Meeting.AddAsync(newMeeting);
            await _unitOfWork.CommitAsync();
            return newMeeting;
        }

        public async Task DeleteMeeting(Meeting meeting)
        {
            _unitOfWork.Meeting.Remove(meeting);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetings()
        {
            return await _unitOfWork.Meeting
                .GetAllMeetingsAsync();
        }

        public async Task<Meeting> GetMeetingById(int id)
        {
            return await _unitOfWork.Meeting
                .GetMeetingByIdAsync(id);
        }

        public async Task UpdateMeeting(Meeting meetingToBeUpdated, Meeting meeting)
        {
            meetingToBeUpdated.MeetingId = meeting.MeetingId;

            await _unitOfWork.CommitAsync();
        }
    }
}
