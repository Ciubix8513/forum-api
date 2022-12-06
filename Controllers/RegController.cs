using Api.Data;
using Api.Data.Entities;
using Api.Dtos;
using Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegController : ControllerBase
{
    private readonly ApiDbContext _apiDbContext;
    private readonly ILogger<RegController> _logger;

    public RegController(ApiDbContext apiDbContext, ILogger<RegController> logger)
    {
        _apiDbContext = apiDbContext;
        _logger = logger;
    }
    [HttpPost]
    [Route("AddForm")]
    public async Task<IActionResult> AddForm(FormAddDto form)
    {
        var userE = await _apiDbContext.User.Where(_ => (_.Username == form.Username || _.Email == form.Email))
            .FirstOrDefaultAsync();
        var formE = await _apiDbContext.Form.Where(_ => _.Username == form.Username || _.Email == form.Email)
            .FirstOrDefaultAsync();
        if (userE != null || formE != null)
            return BadRequest("User/Form already exists");
        //The user doesn't exist and email is not in use
        //Adding the data
        Form FormEntity = new(await _apiDbContext.Form.CountAsync() + 1,
                              form.Username,
                              form.Email,
                              form.Password.Hash(form.Username),
                              form.Reason,
                              DateTime.UtcNow);
        await _apiDbContext.Form.AddAsync(FormEntity);
        await _apiDbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Added new form with id = {FormEntity.Id}");
        return Ok("Success");
    }
    [HttpGet]
    [Route("GetForms")]
    public async Task<IActionResult> GetForms()
    {
        var l = await _apiDbContext.Form.Where(_ => _.Username != null).Select(_ => new FormGetDto(_.Id, _.Username, _.Reason, _.Date)).ToListAsync();
        return Ok(l);
    }
    [HttpPost]
    [Route("EmailTest")]
    public async Task<IActionResult> EmailTest()
    {
        MailSender.SendEmail("Test","Test","Ciubix8514@gmail.com");
        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("AddUser")]
    public async Task<IActionResult> AddUser(int userId)
    {
        var priv = HttpContext.User.Claims.Where(_ => _.Type == "privilege")
            .Select(_ => Convert.ToBoolean(_))
            .First();
        if(!priv)
        {
            var uId = HttpContext.User.Claims.Where(_ => _.Type == "userid")
                .Select(_ => Convert.ToInt32(_))
                .First();
            _logger.Log(LogLevel.Information, $"User with id = {uId} tried to access AddUser, GO BAN THAT MORON!");
            return BadRequest("Go fuck yourself you non mod idiot, you thought I didn't have protection against this, you're so wrong asshole lmao you bout to get banned lol");
        } 

        _logger.Log(LogLevel.Information, $"trying to add user with form id = {userId}");
        var form = await _apiDbContext.Form.Where(_ => _.Id == userId).FirstOrDefaultAsync();
        if (form == null)
            return BadRequest("Invalid id");
        User user = new(await _apiDbContext.User.CountAsync() + 1,
            form.Username,
            form.Email,
            form.Password,
            false,
            "",
            "");
        await _apiDbContext.User.AddAsync(user);
        _apiDbContext.Form.Remove(form);        
        _apiDbContext.SaveChanges();
        //Todo Setup email
        await _apiDbContext.SaveChangesAsync();
        return Ok();
    }
    [HttpPost]
    [Route("Test")]
    public async Task<IActionResult> Test()
    {
        var r = _apiDbContext.User.Count().ToString();
        _logger.Log(LogLevel.Information, r);
        return Ok(r);
    }
}