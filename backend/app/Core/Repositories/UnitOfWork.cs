using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MeetingRoomDbContext _context;
        private CompanyRepository _companyRepository;
        private EmployeeRepository _employeeRepository;
        private MeetingRepository _meetingRepository;
        private RoomRepository _roomRepository;

        public UnitOfWork(MeetingRoomDbContext context)
        {
            this._context = context;
        }

        public ICompanyRepository Company => _companyRepository = _companyRepository ?? new CompanyRepository(_context);
        public IEmployeeRepository Employee => _employeeRepository = _employeeRepository ?? new EmployeeRepository(_context);
        public IMeetingRepository Meeting => _meetingRepository = _meetingRepository ?? new MeetingRepository(_context);
        public IRoomRepository Room => _roomRepository = _roomRepository ?? new RoomRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
