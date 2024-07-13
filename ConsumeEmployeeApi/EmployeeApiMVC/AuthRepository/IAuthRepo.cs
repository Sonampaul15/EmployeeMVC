using EmployeeApiMVC.DTO;

namespace EmployeeApiMVC.AuthRepository
{
    public interface IAuthRepo
    {
        Task<ResponseDto?> RegisterByNameAsync(RegistrationRequestDto RegiDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto RegiDto);
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    }
}
