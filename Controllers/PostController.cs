using Api.Data;
using Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]

public class PostController : ControllerBase
{
    private readonly ApiDbContext _apiDbContext;
    private readonly ILogger<PostController> _logger;

    public PostController(ApiDbContext apiDbContext, ILogger<PostController> logger)
    {
        _apiDbContext = apiDbContext;
        _logger = logger;
    }

    [HttpGet]
    [Route("GetPosts")]
    public async Task<IActionResult> GetPosts(int parent)
    {
        var posts = await _apiDbContext.Post.Where(_ => _.ParentPostId == parent).ToListAsync();
        var Dtos = posts.Select(_ => new PostsGetDto(_.Id,_.CreatorId,_.ParentPostId,_.Contents,_.Date)).ToList();        

        return Ok(Dtos);
    }


}