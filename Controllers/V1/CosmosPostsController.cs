using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{   
    [Route("api/cosmos")]
    public class CosmosPostsController : Controller
    {
        private readonly ICosmosPostDbService _cosmosDbService;
        public CosmosPostsController(ICosmosPostDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _cosmosDbService.GetPostsAsync();
            return Ok(posts);
        }
        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] string postId)
        {
            var post = await _cosmosDbService.GetPostAsync(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }
        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new CosmosPostDto {Id = Guid.NewGuid().ToString() , Name = postRequest.Name };

            await _cosmosDbService.AddPostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = Guid.Parse(post.Id) };
            return Created(locationUrl, response);
        }
    }
}
