using CompanyManagement.WebApi.Filter;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.WebApi.Attribute
{
    public class AuthAttribute : ServiceFilterAttribute
    {
        public AuthAttribute() : base(typeof (ApiKeyAuthorizationFilter))
        {
        }
    }
}
