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
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Company> CreateCompany(Company newCompany)
        {
            await _unitOfWork.Company.AddAsync(newCompany);
            await _unitOfWork.CommitAsync();
            return newCompany;
        }

        public async Task DeleteCompany(Company company)
        {
            _unitOfWork.Company.Remove(company);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _unitOfWork.Company
                .GetAllCompaniesAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _unitOfWork.Company
                .GetCompanyByIdAsync(id);
        }

        public async Task UpdateCompany(Company companyToBeUpdated, Company room)
        {
            companyToBeUpdated.Name = room.Name;
            companyToBeUpdated.CompanyId = room.CompanyId;

            await _unitOfWork.CommitAsync();
        }
    }
}
