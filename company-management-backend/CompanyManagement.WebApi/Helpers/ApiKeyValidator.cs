using CompanyManagement.Shared.Constants;

namespace CompanyManagement.WebApi.Helpers
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        public bool IsValid(string apiKey)
        {
            string envApiKey = Environment.GetEnvironmentVariable(AuthConstants.ApiKeyName) ?? string.Empty;
            if (string.Equals(apiKey, envApiKey, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }

    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}