using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{   
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService ??
                throw new ArgumentNullException(nameof(postService));
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }
        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {   
            var post = await _postService.GetPostByIdAsync(postId);
            
            if (post == null)
                return NotFound();

            return Ok(post);
        }
        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null)
                return NotFound();

            _postService.DeletePost(post);
            await _postService.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postToCreate)
        {
            var post = new Post()
            {
                Name = postToCreate.Name
            };

            await _postService.CreatePostAsync(post);
            await _postService.SaveChangesAsync();

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());
            
            return Created(locationUrl, post);
        }
        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid postId,
                             [FromBody] UpdatePostRequest postToUpdate)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null)
                return NotFound();

            post.Name = postToUpdate.Name;
            await _postService.SaveChangesAsync();

            return Ok(post);
        }
    }
}
