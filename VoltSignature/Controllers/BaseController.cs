using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using VoltSignature.Model.Account;
using VoltSignature.Model.User;

namespace VoltSignature.UI.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public abstract class BaseController : ControllerBase
    {
        private CurrentUser _user;

        protected CurrentUser CurrentUser
        {
            get
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return _user ?? (_user = new CurrentUser
                    {
                        Id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.UserId)?.Value,
                        Email = User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value,
                        FirstName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.FirstName)?.Value,
                        LastName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.LastName)?.Value,
                        CompanyId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.CompanyId)?.Value
                    });
                }
                return null;
            }
        }
    }
}