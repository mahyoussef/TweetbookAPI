using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface ICosmosPostDbService
    {
        Task<IEnumerable<CosmosPostDto>> GetPostsAsync();
        Task<CosmosPostDto> GetPostAsync(string id);
        Task AddPostAsync(CosmosPostDto post);
        Task UpdatePostAsync(string id, CosmosPostDto post);
        Task DeletePostAsync(string id);
    }
}
