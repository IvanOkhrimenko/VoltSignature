﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using VoltSignature.Interface;
using VoltSignature.Model.User;

namespace VoltSignature.UI.Controllers
{
    [Route("api/user")] 
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{Id?}")]
        public async Task<UserModel> Get(string Id)
        {
            var user = await _userService.GetUser(Id ?? CurrentUser.Id);
            return user;
        }
        
    }
}