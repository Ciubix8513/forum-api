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
using Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Controller, Route("[controller]")]
public class RepController : ControllerBase
{

    private readonly ApiDbContext _apiDbContext;
    private readonly ILogger<RepController> _logger;

    public RepController(ApiDbContext apiDbContext, ILogger<RepController> logger)
    {
        _apiDbContext = apiDbContext;
        _logger = logger;
    }
    [Authorize, HttpPost, Route("RepUser")]
    public async Task<IActionResult> RepUser(int Id, string? Reason)
    {
        _logger.Log(LogLevel.Debug, $"Trying to rep user {Id}");
        var obj = await _apiDbContext.User.Where(_ => _.Id == Id).FirstOrDefaultAsync();
        if (obj == null) return BadRequest("User does not exist");
        await _apiDbContext.uReport.AddAsync(new(await _apiDbContext.pReport.CountAsync() + 1, Id, Reason));
        await _apiDbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Reported User {Id}");
        return Ok("Success");
    }
    [Authorize, HttpPost, Route("RepPost")]
    public async Task<IActionResult> RepPost(int Id,string? Reason)
    {
        var obj = await _apiDbContext.Post.Where(_ => _.Id ==  Id).FirstOrDefaultAsync();
        if (obj == null)
            return BadRequest("Post does not exist");
        await _apiDbContext.pReport.AddAsync(new(await _apiDbContext.pReport.CountAsync() + 1,  Id, Reason));
        await _apiDbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Reported Post { Id}");
        return Ok("Success");
    }

    [Authorize, HttpGet, Route("Get/RepPost")]
    public async Task<IActionResult> GetRepPost() => Ok(await _apiDbContext.pReport
        .Where(_ => _.Reason != null)
        .Select(_ => new RepDto(_.Id, _.PostId, _.Reason))
        .ToListAsync());

    [Authorize, HttpGet, Route("Get/RepUser")]
    public async Task<IActionResult> GetRepUser() => Ok(await _apiDbContext.uReport
        .Where(_ => _.Reason != null)
        .Select(_ => new RepDto(_.Id, _.UserId, _.Reason))
        .ToListAsync());
    [Authorize, HttpPost, Route("Delete/RepPost")]
    public async Task<IActionResult> DeleteRepPost(int id)
    {
        var rep = await _apiDbContext.pReport.Where(_ => _.Id == id).FirstOrDefaultAsync();
        if (rep == null) return BadRequest("Invalid id");
        _apiDbContext.Remove(rep);
        await _apiDbContext.SaveChangesAsync();
        return Ok("Success");
    }
    [Authorize, HttpPost, Route("Delete/RepUser")]
    public async Task<IActionResult> DeleteRepUser(int id)
    {
        var rep = await _apiDbContext.uReport.Where(_ => _.Id == id).FirstOrDefaultAsync();
        if (rep == null) return BadRequest("Invalid id");
        _apiDbContext.Remove(rep);
        await _apiDbContext.SaveChangesAsync();
        return Ok("Success");
    }

}
