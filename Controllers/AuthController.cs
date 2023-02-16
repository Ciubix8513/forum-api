//The GPLv3 License (GPLv3)
//
//Copyright (c) 2023 Ciubix8513
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using Api.Data;
using System.Security.Claims;
using Api.Helpers;
using Api.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[ApiController, Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApiDbContext _apiDbContext;
    public AuthController(ApiDbContext authContext) => _apiDbContext = authContext;
    /* [HttpPost]
     [Route("reset")]
     public async Task<IActionResult> ResetPassword(string name)
     {
         var user = await _apiDbContext.User.Where(_ => _.Username == name || _.Email == name)
             .FirstOrDefaultAsync();
         if (user == null || user?.Email == null)
             return BadRequest("Invalid username/email");
         var newPassword = RandomString.RandomStr(15);
         await MailSender.SendEmail(user.Email, "Password reset", $"Your password has been reset to {newPassword}");
         var newUser = user;
         newUser.Password = newPassword.Hash(newUser.Username);
         _apiDbContext.Entry(user).CurrentValues.SetValues(newUser);
         await _apiDbContext.SaveChangesAsync();
         return Ok("Success");
     }*/
    [HttpPost, Route("login")]
    public async Task<IActionResult> LoginAsync(LoginDto login)
    {
        var user = await _apiDbContext.User.Where(_ => _.Username == login.Username
        && _.Password == login.Password.Hash(login.Username)).FirstOrDefaultAsync();
        if (user == null)
            return BadRequest("Invalid credentials");
        var Claims = new List<Claim>
        {
           new Claim("userid",user.Id.ToString()),
           new Claim("username",login.Username),
           new Claim("privilege",user.Privilege.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(
            Claims,
            CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
        return Ok("Success");
    }
    [Authorize, HttpPost, Route("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync();
        return Ok("Success");
    }

    [Authorize, HttpGet, Route("user-profile")]
    public async Task<IActionResult> UserProfileAsync()
    {
        int id = HttpContext.User.Claims.Where(_ => _.Type == "userid")
            .Select(_ => Convert.ToInt32(_.Value))
            .First();
        var userProfile = await _apiDbContext.User.Where(_ => _.Id == id).
        Select(_ => new UserProfileDto(
            _.Id,
            _.Username,
            _.BIO,
            _.Privilege,
            _.Password
        )).FirstOrDefaultAsync();
        return Ok(userProfile);
    }
}