using EmployeeApiMVC.DTO;

namespace EmployeeApiMVC.Repository
{
    public interface IEmployeeRepository
    {
        Task<ResponseDto?> GetAllEmployeeAsync();
        Task<ResponseDto?> GetEmployeeByIdAsync(int id);
        Task<ResponseDto?> CreateEmployeeByNameAsync(EmployeeDto employeeDto);

        Task<ResponseDto?> UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task<ResponseDto?> DeleteEmployeeByIdAsync(int id);
    }
}
