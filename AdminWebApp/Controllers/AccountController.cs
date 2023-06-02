using AdminWebApp.Entities;
using AdminWebApp.Models;
using AdminWebApp.Repositories;
using AdminWebApp.Services.AdminWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdminWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVm register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            _authService.CreateUser(register.Name, register.Email, register.Password);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (!_authService.IsUserValid(login.Email, login.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(login);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, login.Email)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult BlockUser(int userId)
        {
            _authService.BlockUser(userId);
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public IActionResult UnblockUser(int userId)
        {
            _authService.UnblockUser(userId);
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            _authService.DeleteUser(userId);
            return RedirectToAction("Index", "Users");
        }
    }
}
