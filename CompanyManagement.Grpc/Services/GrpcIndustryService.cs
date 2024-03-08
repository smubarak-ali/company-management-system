using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagement.Grpc;
using CompanyManagement.Service.Interface;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CompanyManagement.Grpc.Services
{
    public class GrpcIndustryService : IndustrySrv.IndustrySrvBase
    {
        private readonly ILogger<GrpcIndustryService> _logger;
        private readonly IIndustryService _industryService;

        public GrpcIndustryService(ILogger<GrpcIndustryService> logger, IIndustryService industryService)
        {
            _logger = logger;
            _industryService = industryService;
        }

        public override async Task<IndustryReply> GetIndustries(IndustryRequest request, ServerCallContext context)
        {
            var dtoList = await _industryService.GetIndustries();
            var responseList = dtoList.Select(x => new Industry { Id = x.Id, Name = x.Name });
            IndustryReply response = new();
            response.List.AddRange(responseList);
            return response;
        }
    }
}
