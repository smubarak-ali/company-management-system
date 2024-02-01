using CompanyManagement.Service.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.WebApi.Controllers
{
    [Route("api/v1/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger _logger;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var list = await _companyService.GetCompanies();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("ddl")]
        public async Task<IActionResult> GetCompaniesForDropdown()
        {
            try
            {
                var list = await _companyService.GetCompaniesForDropdown();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto company)
        {
            try
            {
                var id = await _companyService.InsertCompany(company);
                return Created("", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDto company)
        {
            try
            {
                 await _companyService.UpdateCompany(id, company);
                return Ok();
            }
            catch (InvalidIdException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}