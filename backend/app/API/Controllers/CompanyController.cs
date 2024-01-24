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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            this._mapper = mapper;
            this._companyService = companyService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CompanyResource>>> GetAllCompanys()
        {
            var companys = await _companyService.GetAllCompanies();
            var companyResources = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyResource>>(companys);

            return Ok(companyResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyResource>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyById(id);
            var companyResource = _mapper.Map<Company, CompanyResource>(company);

            return Ok(companyResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<CompanyResource>> CreateCompany([FromBody] SaveCompanyResource saveCompanyResource)
        {
            var validator = new SaveCompanyResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCompanyResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var companyToCreate = _mapper.Map<SaveCompanyResource, Company>(saveCompanyResource);

            var newCompany = await _companyService.CreateCompany(companyToCreate);

            var company = await _companyService.GetCompanyById(newCompany.CompanyId);

            var companyResource = _mapper.Map<Company, CompanyResource>(company);

            return Ok(companyResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyResource>> UpdateCompany(int id, [FromBody] SaveCompanyResource saveCompanyResource)
        {
            var validator = new SaveCompanyResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCompanyResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var companyToBeUpdate = await _companyService.GetCompanyById(id);

            if (companyToBeUpdate == null)
                return NotFound();

            var company = _mapper.Map<SaveCompanyResource, Company>(saveCompanyResource);

            await _companyService.UpdateCompany(companyToBeUpdate, company);

            var updatedCompany = await _companyService.GetCompanyById(id);
            var updatedCompanyResource = _mapper.Map<Company, CompanyResource>(updatedCompany);

            return Ok(updatedCompanyResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (id == 0)
                return BadRequest();

            var company = await _companyService.GetCompanyById(id);

            if (company == null)
                return NotFound();

            await _companyService.DeleteCompany(company);

            return NoContent();
        }
    }
}
