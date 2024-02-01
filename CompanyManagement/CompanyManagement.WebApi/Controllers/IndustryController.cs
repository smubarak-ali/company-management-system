using CompanyManagement.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.WebApi.Controllers
{
    [Route("api/v1/industry")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;
        private readonly ILogger _logger;

        public IndustryController(IIndustryService industryService, ILogger<IndustryController> logger)
        {
            _industryService = industryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _industryService.GetIndustries();
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
