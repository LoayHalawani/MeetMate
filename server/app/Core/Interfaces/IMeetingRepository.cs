﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task<IEnumerable<Meeting>> GetAllMeetingsAsync();
        Task<Meeting> GetMeetingByIdAsync(int id);
    }
}
