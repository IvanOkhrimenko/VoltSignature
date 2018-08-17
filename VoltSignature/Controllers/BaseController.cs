using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoltSignature.Model.User;
using VoltSignature.UI.Model;

namespace VoltSignature.UI.Controllers
{ 
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
                        Id = int.Parse( User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.UserId)?.Value),
                        Email = User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value,
                        FirstName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.FirstName)?.Value,
                        LastName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.LastName)?.Value,
                        CompanyId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConst.CompanyId)?.Value)
                    });
                }
                return null;
            }
        }
    }
}