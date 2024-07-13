using EmployeeApiMVC.DTO;

namespace EmployeeApiMVC.Repository
{
    public interface IBaseRepository
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
