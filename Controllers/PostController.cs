using Api.Data;
using Api.Data.Entities;
using Api.Dtos;
using Microsoft.AspNetCore.Authorization;
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
    [HttpPost]
    [Authorize]
    [Route("AddPost")]
    public async Task<IActionResult> AddPost(PostAddDto post)
    {
        int id = HttpContext.User.Claims.Where(_ => _.Type == "userid")
            .Select(_ => Convert.ToInt32(_))
            .First();
        Post _post = new(
            await _apiDbContext.Post.CountAsync() + 1,
            id,
            post.ParentPostId,
            post.Content,
            DateTime.UtcNow);
        await _apiDbContext.AddAsync(_post);
        await _apiDbContext.SaveChangesAsync();
        return Ok("Success");
    }
    [HttpGet]
    [Route("GetPosts")]
    public async Task<IActionResult> GetPosts(int parent)
    {
        var posts = await _apiDbContext.Post.Where(_ => _.ParentPostId == parent).ToListAsync();
        var Dtos = posts.Select(_ => new PostsGetDto(_.Id, _.CreatorId, _.ParentPostId, _.Contents, _.Date)).ToList();

        return Ok(Dtos);
    }
}