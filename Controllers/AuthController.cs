using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using LibraryManagementService.DTOs;
using Infrastructure.Services;
using Infrastructure.Identity;      // ← use this ApplicationUser
// remove: using LibraryManagementService.Models;

namespace LibraryManagement.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;
        private readonly JwtTokenService _tokenSvc;

        public AuthController(
            UserManager<ApplicationUser> userMgr,
            SignInManager<ApplicationUser> signInMgr,
            JwtTokenService tokenSvc)
        {
            _userMgr   = userMgr;
            _signInMgr = signInMgr;
            _tokenSvc  = tokenSvc;
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterDto());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = new ApplicationUser
            {
                UserName  = dto.Username,
                Email     = dto.Email,
                FirstName = dto.FirstName,
                LastName  = dto.LastName
            };

            var res = await _userMgr.CreateAsync(user, dto.Password);
            if (!res.Succeeded)
            {
                foreach (var e in res.Errors)
                    ModelState.AddModelError("", e.Description);
                return View(dto);
            }

            // assign Admin role & auto-login
            await _userMgr.AddToRoleAsync(user, "Admin");
            await _signInMgr.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginDto());

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userMgr.FindByNameAsync(dto.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(dto);
            }

            var rl = await _signInMgr.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!rl.Succeeded)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(dto);
            }

            // issue JWT & store in cookie
            var roles = await _userMgr.GetRolesAsync(user);
            var token = _tokenSvc.GenerateToken(user, roles);

            Response.Cookies.Append("X-Access-Token", token, new CookieOptions
            {
                HttpOnly   = true,
                Secure     = true,
                SameSite   = SameSiteMode.Strict
            });

            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync();
            Response.Cookies.Delete("X-Access-Token");
            return RedirectToAction("Login");
        }
    }
}
