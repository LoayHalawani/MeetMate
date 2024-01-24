using Core.Models;

namespace API.Resources
{
    public class SaveCompanyResource
    {
        public int CompanyId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? EmailAddress { get; set; }

        public string? Logo { get; set; }

        public bool? Active { get; set; }

        public int? EmployeeId { get; set; }
    }
}
