using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoltSignature.Model.Account;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;
using VoltSignature.UI.Model;

namespace VoltSignature.UI.Controllers
{
    public class AccountController : Controller
    {
        IStorage _storage;

        public AccountController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            User user = await _storage.Get<User>().Get(x => x.Email == model.Login && x.Password == model.Password, query=> query.Include(x=>x.UserRole), true);
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
    }
}