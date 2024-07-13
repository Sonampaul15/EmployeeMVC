using EmployeeApiMVC.Repository;
using EmployeeApiMVC.Utility;

namespace EmployeeApiMVC.Services
{
    public class TokenProviderService : ITokenProvider
    {
        private readonly IHttpContextAccessor contextAccessor;
        public TokenProviderService(IHttpContextAccessor _contextAccessor)
        {
                contextAccessor = _contextAccessor;
        }
        public void ClearToken()
        {
            contextAccessor.HttpContext?.Response.Cookies.Delete(StaticData.TokenValue);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken=contextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticData.TokenValue, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string? token)
        {
            
            contextAccessor.HttpContext?.Response.Cookies.Append(StaticData.TokenValue, token); 
        }
    }
}
