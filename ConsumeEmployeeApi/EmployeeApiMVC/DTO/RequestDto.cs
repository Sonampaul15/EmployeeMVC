using System.Security.AccessControl;
using static EmployeeApiMVC.Utility.StaticData;

namespace EmployeeApiMVC.DTO
{
    public class RequestDto
    {
        public ApiType apiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public Object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
