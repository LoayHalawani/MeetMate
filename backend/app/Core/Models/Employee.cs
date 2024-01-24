using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }
}
