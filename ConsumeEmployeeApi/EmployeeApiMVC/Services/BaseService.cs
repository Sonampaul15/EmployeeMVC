using EmployeeApiMVC.DTO;
using EmployeeApiMVC.Repository;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static EmployeeApiMVC.Utility.StaticData;

namespace EmployeeApiMVC.Services
{
    public class BaseService : IBaseRepository
    {
        private readonly IHttpClientFactory HC;
        public BaseService(IHttpClientFactory hc)
        {
            HC = hc;
        }
        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = HC.CreateClient("CrudApiClient");
                HttpRequestMessage message = new();
                message.RequestUri = new Uri(requestDto.Url);
                
                    if(requestDto.Data != null)
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                    }
                    switch (requestDto.apiType)
                    {
                        case ApiType.POST: message.Method=HttpMethod.Post; break;
                        case ApiType.PUT: message.Method=HttpMethod.Put; break;
                        case ApiType.DELETE: message.Method=HttpMethod.Delete; break;
                        default: message.Method=HttpMethod.Get; break;  
                    }
                    HttpResponseMessage apiResponse = null;
                    apiResponse = await client.SendAsync(message);
                    switch (apiResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound: return new() { IsSuccess = false, Message = "Not Found" }; break;
                        case HttpStatusCode.Unauthorized: return new() { IsSuccess = false, Message = "Unauthorized" }; break;
                        case HttpStatusCode.Forbidden: return new() { IsSuccess = false, Message = "Access Denied" }; break;
                        case HttpStatusCode.InternalServerError: return new() { IsSuccess = false, Message = "Internal Server Error" };break;

                        default:
                            var apiContent = await apiResponse.Content.ReadAsStringAsync();
                            var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                            return apiResponseDto;
                    }
                
            }
            catch (Exception ex)
            {

                var dto = new ResponseDto() 
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
                return dto;
            }
        }
    }
}
