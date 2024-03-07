using CompanyManagement.BL.Command;
using CompanyManagement.BL.Query;
using CompanyManagement.Criteria;
using CompanyManagement.Service.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Exceptions;
using CompanyManagement.WebApi.Attribute;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.WebApi.Controllers
{
    [Route("api/v1/company")]
    [ApiController]
    [Auth]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger _logger;
        private readonly ISender _sender;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger, ISender sender)
        {
            _companyService = companyService;
            _logger = logger;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery] int? companyNo, [FromQuery] string? companyName, [FromQuery] int? industryId, 
               [FromQuery] string? city, [FromQuery] string? parentCompany, [FromQuery] int? totalEmployees, [FromQuery] bool sortByCompanyNameDesc = false,
               [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                CompanySearchCriteria criteria = new()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    City = city,
                    CompanyName = companyName,
                    ParentCompany = parentCompany,
                    CompanyNo = companyNo,
                    IndustryId = industryId,
                    CompanyNameDesc = sortByCompanyNameDesc,
                    TotalEmployees = totalEmployees
                };

                var list = await _companyService.GetCompanies(criteria);
                return Ok(new { Items = list.Items, TotalPages = list.TotalPages });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetCompanyByIdQuery(id);
                var comp = await _sender.Send(query, cancellationToken);
                return Ok(comp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }


        [HttpGet("ddl")]
        public async Task<IActionResult> GetCompaniesForDropdown(CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetCompanyForDropdownQuery();
                var list = await _sender.Send(query, cancellationToken);
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
                var command = new InsertCompanyCommand(company);
                var id = await _sender.Send(command);
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
                var command = new UpdateCompanyCommand(id, company);
                 await _sender.Send(command);
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