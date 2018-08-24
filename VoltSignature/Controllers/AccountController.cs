using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VoltSignature.Interface;
using VoltSignature.Model.Account;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;
using VoltSignature.UI.Model;

namespace VoltSignature.UI.Controllers
{
    public class AccountController : Controller
    {
        private IStorage _storage;
        private readonly IUserService _userService;

        public AccountController(IStorage storage, IUserService userService)
        {
            _storage = storage;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet("/Account/Register/{token}")]
        public IActionResult Register(string token)
        {
            ViewBag.token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            User user = await _storage.Get<User>().Get(x => x.Email == model.Login && x.Password == model.Password, query => query.Include(x => x.UserRole), true);
            if (user == null)
                return View(model);
            await Authenticate(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
         

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user  = await _userService.RegisterUser(model, RegisterParameters);
                await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public  IActionResult GetRegisterToken()
        {
            RegistrationParameters parameters = new RegistrationParameters()
            {
                CompanyId = 1,
                Position = "Sales",
                RoleId = 1
            };

            string token = _userService.GenerateRegistrationToken(parameters);
            return Json(new { token = token });
        }

        private async Task Authenticate(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Name, customer.Email),
                new Claim(ClaimTypeConst.UserId, user.Id.ToString()),
                new Claim(ClaimTypeConst.FirstName, user.FirstName),
                new Claim(ClaimTypeConst.LastName, user.LastName),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email) ,
                new Claim(ClaimTypeConst.RoleId, user.RoleId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserRole.Name),
                new Claim(ClaimTypeConst.CompanyId, user.CompanyId.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        private RegistrationParameters RegisterParameters
        {
            get
            {
                return new RegistrationParameters
                {
                    Position = User.Claims.FirstOrDefault(x => x.Type == RegistrationParameters.PositionClaim)?.Value,
                    CompanyId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == RegistrationParameters.CompanyClaim)?.Value),
                    RoleId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == RegistrationParameters.RoleClaim)?.Value)
                };
            }
        }
    }
}