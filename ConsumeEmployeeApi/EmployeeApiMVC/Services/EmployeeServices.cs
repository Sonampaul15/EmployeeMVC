using EmployeeApiMVC.DTO;
using EmployeeApiMVC.Repository;
using EmployeeApiMVC.Utility;

namespace EmployeeApiMVC.Services
{
    public class EmployeeServices : IEmployeeRepository
    {
        private readonly IBaseRepository BaseRepository;
        public EmployeeServices(IBaseRepository baseRepository)
        {
            BaseRepository = baseRepository;
        }
        public async Task<ResponseDto?> CreateEmployeeByNameAsync(EmployeeDto employeeDto)
        {
            return await BaseRepository.SendAsync(new RequestDto()
            {
                apiType = StaticData.ApiType.POST,
                Data = employeeDto,
                Url=StaticData.CrudAPIUrl + "/api/Employees"
            });
        }

        public Task<ResponseDto?> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetAllEmployeeAsync()
        {
            return await BaseRepository.SendAsync(new RequestDto() 
            { 
             apiType= StaticData.ApiType.GET,
             Data="",
             Url = StaticData.CrudAPIUrl + "/api/Employees"
            }); 
        }

        public Task<ResponseDto?> GetEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
