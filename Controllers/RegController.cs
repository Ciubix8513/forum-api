using Api.Data;
using Api.Data.Entities;
using Api.Dtos;
using Api.Helpers;
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
        Form FormEntity = new(await _apiDbContext.Form.CountAsync(),
                              form.Username,
                              form.Email,
                              form.Password.Hash(form.Username),
                              form.Reason,
                              DateTime.UtcNow);
         await _apiDbContext.Form.AddAsync(FormEntity);
         _apiDbContext.SaveChanges();
        _logger.Log(LogLevel.Information, $"Added new form with id = {FormEntity.Id}");
        return Ok("Success");
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