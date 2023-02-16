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
public class UserController : ControllerBase
{
    private readonly ApiDbContext _apiDbContext;
    private readonly ILogger<UserController> _logger;

    public UserController(ApiDbContext apiDbContext, ILogger<UserController> logger)
    {
        _apiDbContext = apiDbContext;
        _logger = logger;
    }

    [Authorize, HttpPost, Route("SetBio")]
    public async Task<IActionResult> SetBIO(BioSetDto dto)
    {

        int id = HttpContext.User.Claims.Where(_ => _.Type == "userid")
             .Select(_ => Convert.ToInt32(_.Value))
             .First();
        var isMod = HttpContext.User.Claims.Where(_ => _.Type == "privilege")
            .Select(_ => Convert.ToBoolean(_.Value))
            .First();
        var user = await _apiDbContext.User.Where(_ => _.Id == dto.Id).FirstOrDefaultAsync();
        if (user == null)
            return BadRequest("User doesn't exist");
        if (!(isMod || user.Id == id))
            return BadRequest("YOU CANNOT EDIT THIS USER YOU FOOL");
        var modUser = user;
        modUser.BIO = dto.Contents;
        _apiDbContext.Entry(user).CurrentValues.SetValues(modUser);
        await _apiDbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Edited bio of the user {user.Username}");
        return Ok("Success");
    }
    [HttpGet, Route("GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
        var usr = await _apiDbContext.User.Where(_ => _.Id == id).Select(_ => new UserDataDto(
            _.Id,
            _.Username,
            _.BIO,
            _apiDbContext.Post.Where(p => p.CreatorId == _.Id && p.Contents != null).Count(),
            _.PFP
        )).FirstOrDefaultAsync();
        return Ok(usr);
    }
}