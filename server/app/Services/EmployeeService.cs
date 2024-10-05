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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            await _unitOfWork.Employee.AddAsync(newEmployee);
            await _unitOfWork.CommitAsync();
            return newEmployee;
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _unitOfWork.Employee.Remove(employee);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _unitOfWork.Employee
                .GetAllEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.Employee
                .GetEmployeeByIdAsync(id);
        }

        public async Task UpdateEmployee(Employee employeeToBeUpdated, Employee employee)
        {
            employeeToBeUpdated.Name = employee.Name;
            employeeToBeUpdated.EmployeeId = employee.EmployeeId;

            await _unitOfWork.CommitAsync();
        }
    }
}
