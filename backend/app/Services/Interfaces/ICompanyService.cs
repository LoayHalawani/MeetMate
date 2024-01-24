using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company> GetCompanyById(int id);
        Task<Company> CreateCompany(Company newCompany);
        Task UpdateCompany(Company companyToBeUpdated, Company company);
        Task DeleteCompany(Company company);
    }
}
