using Api.Data;
using System.Security.Claims;
using Api.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApiAuthContext _authContext;
    public AuthController(ApiAuthContext authContext) => _authContext = authContext;

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(LoginDto login)
    {
        var user = await _authContext.User.Where(_ => _.Username == login.Username
        && _.Password == login.Password).FirstOrDefaultAsync();
        if (user == null)
            return BadRequest("Invalid credentials");
        var Claims = new List<Claim>
        {
           new Claim("userid",user.Id.ToString()),
           new Claim("username",login.Username)
        };
        var claimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
        return Ok("Success");
    }
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync();
        return Ok("Success");
    }

    [Authorize]
    [HttpGet]
    [Route("user-profile")]
    public async Task<IActionResult> UserProfileAsync()
    {
        int id = HttpContext.User.Claims.Where(_=>_.Type == "userid").Select(_=> Convert.ToInt32(_.Value)).First();
        var userProfile = await _authContext.User.Where(_ => _.Id == id).
        Select(_ => new UserProfileDto(
            _.Id,
            _.Username,
            _.BIO,
            _.Email,
            _.Privilege,
            _.Password
        )).FirstOrDefaultAsync();
        return Ok(userProfile);
    }
}