using Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegController : ControllerBase
{
    private readonly ApiDbContext _apiDbContext;

    public RegController(ApiDbContext apiDbContext) => _apiDbContext = apiDbContext;

    [HttpPost]
    [Route("AddForm")]
    public async Task<IActionResult> AddForm()
    {
        return Ok("Success");
    }
}