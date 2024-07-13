using EmployeeApiMVC.AuthRepository;
using EmployeeApiMVC.DTO;
using EmployeeApiMVC.Repository;
using EmployeeApiMVC.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeApiMVC.Controllers
{
    public class AuthController : Controller
    {
        private IAuthRepo authRepo;
        private readonly ITokenProvider tokenProvider;
        public AuthController(IAuthRepo _authRepo, ITokenProvider _tokenProvider)
        {
            authRepo = _authRepo;
            tokenProvider = _tokenProvider;     
        }

        [HttpGet]
        public async Task<IActionResult>RegisterUser()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticData.RoleAdmin,Value=StaticData.RoleAdmin},
                new SelectListItem{Text=StaticData.RoleAccount,Value=StaticData.RoleAccount},
                new SelectListItem{Text=StaticData.RoleCustomer,Value=StaticData.RoleCustomer},
                new SelectListItem{Text=StaticData.RoleOperator,Value=StaticData.RoleOperator},
                new SelectListItem{Text=StaticData.RoleTransport,Value=StaticData.RoleTransport},
            };
            ViewBag.RoleList = roleList;    
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> RegisterUser(RegistrationRequestDto registration)
        {
            //registration.Role = "ADMIN";
            ResponseDto responseDto = await authRepo.RegisterByNameAsync(registration);
            ResponseDto responseRoleApi;
            if(responseDto != null && responseDto.IsSuccess)
            {
                if(string.IsNullOrEmpty(registration.Role))
                {
                    registration.Role=StaticData.RoleCustomer;
                }
                responseRoleApi=await authRepo.AssignRoleAsync(registration);
                if(responseRoleApi != null && responseRoleApi.IsSuccess)
                {
                    TempData["success"] = "User Register Successfully";
                    return RedirectToAction("Index", "Employees");
                }
                
            }
            else
            {
                TempData["error"] = "Error in registration";
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticData.RoleAdmin,Value=StaticData.RoleAdmin},
                new SelectListItem{Text=StaticData.RoleAccount,Value=StaticData.RoleAccount},
                new SelectListItem{Text=StaticData.RoleCustomer,Value=StaticData.RoleCustomer},
                new SelectListItem{Text=StaticData.RoleOperator,Value=StaticData.RoleOperator},
                new SelectListItem{Text=StaticData.RoleTransport,Value=StaticData.RoleTransport},
            };
            ViewBag.RoleList = roleList;
            return View(registration);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {           
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            //registration.Role = "ADMIN";
            ResponseDto responseDto = await authRepo.LoginAsync(loginRequestDto);
           
            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                SingInUser(loginResponseDto);
                tokenProvider.SetToken(loginResponseDto.Token);
             TempData["success"] = "User login Successfully";
            return RedirectToAction("Index","Employees");
               //return Redirect 

            }
            else
            {
                TempData["error"] = "Error in login";
                return View(loginRequestDto);
            }
           
            
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            tokenProvider.ClearToken();
            return RedirectToAction("Login", "Auth");
        }
        private async Task SingInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.Type =="role" ).Value));

            var principal=new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

        }
    }
}   
