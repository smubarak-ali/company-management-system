using CompanyManagement.BL.Query;
using CompanyManagement.Service.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.WebApi.Attribute;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.WebApi.Controllers
{
    [Route("api/v1/industry")]
    [ApiController]
    [Auth]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;
        private readonly ILogger _logger;
        private readonly ISender _sender;

        public IndustryController(IIndustryService industryService, ILogger<IndustryController> logger, ISender sender)
        {
            _industryService = industryService;
            _logger = logger;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetIndustryQuery();
                var list = await _sender.Send(query, cancellationToken);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
