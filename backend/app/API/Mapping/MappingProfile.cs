using API.Resources;
using AutoMapper;
using Core.Models;
using Core.Models.Auth;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Company, CompanyResource>();
            CreateMap<Employee, EmployeeResource>();
            CreateMap<Meeting, MeetingResource>();
            CreateMap<Room, RoomResource>();

            // Resource to Domain
            CreateMap<CompanyResource, Company>();
            CreateMap<EmployeeResource, Employee>();
            CreateMap<MeetingResource, Meeting>();
            CreateMap<RoomResource, Room>();

            CreateMap<SaveCompanyResource, Company>();
            CreateMap<SaveEmployeeResource, Employee>();
            CreateMap<SaveMeetingResource, Meeting>();
            CreateMap<SaveRoomResource, Room>();

            CreateMap<UserSignUpResource, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}
