using Api.Data;
using Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Controller]
[Route("[controller]")]
public class RepController : ControllerBase
{

    private readonly ApiDbContext _apiDbContext;
    private readonly ILogger<RepController> _logger;

    public RepController(ApiDbContext apiDbContext, ILogger<RepController> logger)
    {
        _apiDbContext = apiDbContext;
        _logger = logger;
    }
    [Authorize]
    [HttpPost]
    [Route("RepUser")]
    public async Task<IActionResult> RepUser(RepDto dto)
    {
        var obj = await _apiDbContext.User.Where(_ => _.Id == dto.Id).FirstOrDefaultAsync();
        if (obj == null) return BadRequest("User does not exist");
        await _apiDbContext.uReport.AddAsync(new(await _apiDbContext.pReport.CountAsync() + 1, dto.Id, dto.Reason));
        await _apiDbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Reported User {dto.Id}");
        return Ok("Success");
    }
    [Authorize]
    [HttpPost]
    [Route("RepPost")]
    public async Task<IActionResult> RepPost(RepDto dto)
    {
        var obj = await _apiDbContext.Post.Where(_ => _.Id == dto.Id).FirstOrDefaultAsync();
        if (obj == null)
            return BadRequest("Post does not exist");
        await _apiDbContext.pReport.AddAsync(new(await _apiDbContext.pReport.CountAsync() + 1, dto.Id, dto.Reason));
        await _apiDbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Reported Post {dto.Id}");
        return Ok("Success");
    }

    [HttpGet, Route("Get/RepPost")]
    public async Task<IActionResult> GetRepPost() => Ok(await _apiDbContext.pReport
        .Where(_ => _.Reason != null)
        .Select(_ => new RepDto(_.Id, _.PostId, _.Reason))
        .ToListAsync());

    [HttpGet, Route("Get/RepUser")]
    public async Task<IActionResult> GetRepUser() => Ok(await _apiDbContext.uReport
        .Where(_ => _.Reason != null)
        .Select(_ => new RepDto(_.Id,_.UserId, _.Reason))
        .ToListAsync());
    [HttpPost, Route("Delete/RepPost")]
    public async Task<IActionResult> DeleteRepPost(int id)
    {
        var rep = await _apiDbContext.pReport.Where(_ => _.Id == id).FirstOrDefaultAsync();
        if (rep == null) return BadRequest("Invalid id");
        _apiDbContext.Remove(rep);
        await _apiDbContext.SaveChangesAsync();
        return Ok("Success");
    }
    [HttpPost, Route("Delete/RepUser")]
    public async Task<IActionResult> DeleteRepUser(int id)
    {
        var rep = await _apiDbContext.uReport.Where(_ => _.Id == id).FirstOrDefaultAsync();
        if (rep == null) return BadRequest("Invalid id");
        _apiDbContext.Remove(rep);
        await _apiDbContext.SaveChangesAsync();
        return Ok("Success");
    }

}
