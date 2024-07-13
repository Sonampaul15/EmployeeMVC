using EmployeeApiMVC.AuthRepository;
using EmployeeApiMVC.AuthServices;
using EmployeeApiMVC.Repository;
using EmployeeApiMVC.Services;
using EmployeeApiMVC.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IBaseRepository, BaseService>();
builder.Services.AddHttpClient<IEmployeeRepository, EmployeeServices>();
builder.Services.AddScoped<IBaseRepository, BaseService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeServices>();
builder.Services.AddScoped<ITokenProvider, TokenProviderService>();
builder.Services.AddScoped<IAuthRepo, AuthServices>();
StaticData.CrudAPIUrl = builder.Configuration["ServiceUrl:CrudApi"];
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan=TimeSpan.FromMinutes(5);
        options.LoginPath="/Auth/Login";
        options.AccessDeniedPath= "/Auth/Login";
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
