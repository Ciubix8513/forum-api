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
        var parent = await _apiDbContext.Post.Where(_ => _.Id == post.ParentPostId).FirstOrDefaultAsync();
        if (parent == null)
            return BadRequest("Parent post doesn't exist");
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
    [HttpPost]
    [Authorize]
    [Route("EditPost")]
    public async Task<IActionResult> EditPost(PostEditDto post)
    {
        int id = HttpContext.User.Claims.Where(_ => _.Type == "userid")
             .Select(_ => Convert.ToInt32(_))
             .First();
        var oldPost = await _apiDbContext.Post.Where(_ => _.Id == post.Id).FirstOrDefaultAsync();
        if (oldPost == null)
            return BadRequest("Post doesn't exist");
        Post _post = new(post.Id,
                         id,
                         oldPost.ParentPostId,
                         post.Contents,
                         oldPost.Date);
        _apiDbContext.Entry(oldPost).CurrentValues.SetValues(_post);
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