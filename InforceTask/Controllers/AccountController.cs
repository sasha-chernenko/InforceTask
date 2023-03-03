using InforceTask.Context;
using InforceTask.Models;
using InforceTask.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InforceTask.Controllers
{
   // [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly URLContext _context;
        public AccountController(URLContext context)
        {
            _context = context;
        }
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var username = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == username);
            return Ok(user);
        }
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User? user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == loginViewModel.Login &&
                    u.Password == loginViewModel.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Table", "Url");
                }
                ModelState.AddModelError(nameof(LoginViewModel.Password), "Incorrect login or password");
            }
            return View(loginViewModel);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    user = new User { Login = model.Login, Password = model.Password };
                    var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "ordinary");
                    if (userRole != null)
                        user.Role = userRole;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    await Authenticate(user);
                    return RedirectToAction("Table", "Url");
                }
                ModelState.AddModelError(nameof(URL.Long), "This field can`t be empty");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }
    }
}
