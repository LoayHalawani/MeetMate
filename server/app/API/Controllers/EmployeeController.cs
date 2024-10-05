using API.Resources;
using API.Validators;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            this._mapper = mapper;
            this._employeeService = employeeService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            var employeeResources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);

            return Ok(employeeResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            var employeeResource = _mapper.Map<Employee, EmployeeResource>(employee);

            return Ok(employeeResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<EmployeeResource>> CreateEmployee([FromBody] SaveEmployeeResource saveEmployeeResource)
        {
            var validator = new SaveEmployeeResourceValidator();
            var validationResult = await validator.ValidateAsync(saveEmployeeResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var employeeToCreate = _mapper.Map<SaveEmployeeResource, Employee>(saveEmployeeResource);

            var newEmployee = await _employeeService.CreateEmployee(employeeToCreate);

            var employee = await _employeeService.GetEmployeeById(newEmployee.EmployeeId);

            var employeeResource = _mapper.Map<Employee, EmployeeResource>(employee);

            return Ok(employeeResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResource>> UpdateEmployee(int id, [FromBody] SaveEmployeeResource saveEmployeeResource)
        {
            var validator = new SaveEmployeeResourceValidator();
            var validationResult = await validator.ValidateAsync(saveEmployeeResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var employeeToBeUpdate = await _employeeService.GetEmployeeById(id);

            if (employeeToBeUpdate == null)
                return NotFound();

            var employee = _mapper.Map<SaveEmployeeResource, Employee>(saveEmployeeResource);

            await _employeeService.UpdateEmployee(employeeToBeUpdate, employee);

            var updatedEmployee = await _employeeService.GetEmployeeById(id);
            var updatedEmployeeResource = _mapper.Map<Employee, EmployeeResource>(updatedEmployee);

            return Ok(updatedEmployeeResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id == 0)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
                return NotFound();

            await _employeeService.DeleteEmployee(employee);

            return NoContent();
        }
    }
}
