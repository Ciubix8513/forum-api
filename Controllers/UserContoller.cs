using Api.Data;
using Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Controller]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApiDbContext _apiDbContext;
    private readonly ILogger<UserController> _logger;

    public UserController(ApiDbContext apiDbContext, ILogger<UserController> logger)
    {
        _apiDbContext = apiDbContext;
        _logger = logger;
    }

    [HttpPost]
    [Route("SetBio")]
    [Authorize]
    public async Task<IActionResult> SetBIO(BioSetDto dto)
    {
        
        int id = HttpContext.User.Claims.Where(_ => _.Type == "userid")
             .Select(_ => Convert.ToInt32(_))
             .First();
        var isMod  = HttpContext.User.Claims.Where(_ => _.Type == "privilege")
            .Select(_ => Convert.ToBoolean(_))
            .First();
        var user = await _apiDbContext.User.Where(_ => _.Id == dto.Id).FirstOrDefaultAsync();
        if (user == null)
            return BadRequest("User doesn't exist");
        if(!(isMod || user.Id == id))
            return BadRequest("YOU CANNOT EDIT THIS USER YOU FOOL");
        var modUser = user;
        modUser.BIO = dto.Contents;
        _apiDbContext.Entry(user).CurrentValues.SetValues(modUser);
        await _apiDbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information,$"Edited bio of the user {user.Username}");
        return Ok("Success");
    }
    [HttpGet]
    [Route("GetUsers")]
    public async Task<IActionResult> GetUsers(UserListDto dto)
    {
        var data = await _apiDbContext.User.Where(_ => dto.Ids.Contains(_.Id)).ToListAsync();

        return Ok(data);
    }
}