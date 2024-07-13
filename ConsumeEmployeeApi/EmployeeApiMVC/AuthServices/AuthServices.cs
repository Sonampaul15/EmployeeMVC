using EmployeeApiMVC.AuthRepository;
using EmployeeApiMVC.DTO;
using EmployeeApiMVC.Repository;
using EmployeeApiMVC.Utility;
using Microsoft.Win32;

namespace EmployeeApiMVC.AuthServices
{
    public class AuthServices : IAuthRepo
    {
        private readonly IBaseRepository baseRepository;
        public AuthServices(IBaseRepository _baseRepository)
        {
            baseRepository = _baseRepository;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto RegiDto)
        {
            return await baseRepository.SendAsync(new RequestDto
            {
                apiType = StaticData.ApiType.POST,
                Data = RegiDto,
                Url = StaticData.CrudAPIUrl + "/api/Authentication/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequest)
        {
            return await baseRepository.SendAsync(new RequestDto
            {
                apiType = StaticData.ApiType.POST,
                Data = loginRequest,
                Url = StaticData.CrudAPIUrl+ "/api/Authentication/Login"
            });
        }

        public async Task<ResponseDto?> RegisterByNameAsync(RegistrationRequestDto RegiDto)
        {
            return await baseRepository.SendAsync(new RequestDto
            {
                apiType= StaticData.ApiType.POST,
                Data = RegiDto,
                Url= StaticData.CrudAPIUrl+"/api/Authentication/register"
            });
        }
    }
}
