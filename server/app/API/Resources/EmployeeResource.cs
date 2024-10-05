using Core.Models;

namespace API.Resources
{
    public class EmployeeResource
    {
        public int EmployeeId { get; set; }

        public string? Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public int? CompanyId { get; set; }
    }
}
