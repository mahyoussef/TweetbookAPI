using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(Guid postId);
        void UpdatePost(Post postToUpdate);
        Task CreatePostAsync(Post postToCreate);
        void DeletePost(Post postToDelete);
        Task<bool> SaveChangesAsync();
    }
}
