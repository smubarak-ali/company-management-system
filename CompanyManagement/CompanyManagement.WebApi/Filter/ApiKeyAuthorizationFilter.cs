using CompanyManagement.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CompanyManagement.Shared.Constants;

namespace CompanyManagement.WebApi.Filter
{
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {

        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyAuthorizationFilter(IApiKeyValidator apiKeyValidator)
        {
            _apiKeyValidator = apiKeyValidator;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string apiKey = context.HttpContext.Request.Headers[AuthConstants.ApiKeyHeaderName];

            if (!_apiKeyValidator.IsValid(apiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
