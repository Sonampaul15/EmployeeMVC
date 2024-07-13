    using EmployeeApiMVC.DTO;
using EmployeeApiMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeApiMVC.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository EmpRepo;

        public EmployeesController(IEmployeeRepository emprepo)
        {   
            EmpRepo = emprepo;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<EmployeeDto> dtos = new();
            ResponseDto? responseDto = await EmpRepo.GetAllEmployeeAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                dtos=JsonConvert.DeserializeObject<List<EmployeeDto>>(Convert.ToString(responseDto.Result));
            }
            return View(dtos);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
         return View();
        }

        [HttpPost]
        public async Task <IActionResult> CreateUser(EmployeeDto employeeDto)
        {
            ResponseDto response = await EmpRepo.CreateEmployeeByNameAsync(employeeDto);
            if (response!=null && response.IsSuccess)
            {
                return RedirectToAction("Index","Home");
            }
            return View(employeeDto);
        }
    }
}
 