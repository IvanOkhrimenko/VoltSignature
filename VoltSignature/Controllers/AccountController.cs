using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using VoltSignature.Model.Account;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;

namespace VoltSignature.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        IStorage _storage;

        public AccountController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("Login")]
        public IActionResult Login() => View();


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            User user = await _storage.Get<User>().Get(x => x.Email == model.Login && x.Password == model.Password, null, true);
            if (user == null)
                return View(model);
            await Authenticate(user);
            return RedirectToAction("Index", "Home");
        }


        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}