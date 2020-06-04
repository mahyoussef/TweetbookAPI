using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Data;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;
        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeletePost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _dataContext.Posts.Remove(post);
        }
        public async void CreatePostAsync(Post post)
        {
            if(post == null)
                throw new ArgumentNullException(nameof(post));

            await _dataContext.Posts.AddAsync(post);
        }
        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            if (postId == null)
                throw new ArgumentNullException(nameof(postId));

            var post = await _dataContext.Posts.SingleOrDefaultAsync(p => p.Id == postId);
            return(post);
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var posts = await _dataContext.Posts.ToListAsync();
            return (posts);
        }

        public void UpdatePost(Post postToUpdate)
        {
            // Due to tracking changes no logic here ...
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _dataContext.SaveChangesAsync()) > 0;
        }
    }
}
