﻿using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ourTime_server.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ourTime_server.Services;


namespace ourTime_server.Services
{
    public interface IUserService
    {
        string GetMyName();
        //string CreateToken(User user);
        string Login(UserDto user);

        ActionResult<User> Register(UserDto user);
    }
}