using Cosmonaut;
using Cosmonaut.Extensions;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class CosmosPostService : ICosmosPostDbService
    {
        private Container _container;

        public CosmosPostService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddPostAsync(CosmosPostDto item)
        {
            await this._container.CreateItemAsync<CosmosPostDto>(item, new PartitionKey(item.Id));
        }

        public async Task DeletePostAsync(string id)
        {
            await this._container.DeleteItemAsync<CosmosPostDto>(id, new PartitionKey(id));
        }

        public async Task<CosmosPostDto> GetPostAsync(string id)
        {
            try
            {
                ItemResponse<CosmosPostDto> response = await this._container.ReadItemAsync<CosmosPostDto>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<IEnumerable<CosmosPostDto>> GetPostsAsync()
        {
            var query = this._container.GetItemQueryIterator<CosmosPostDto>();
            List<CosmosPostDto> results = new List<CosmosPostDto>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdatePostAsync(string id, CosmosPostDto item)
        {
            await this._container.UpsertItemAsync<CosmosPostDto>(item, new PartitionKey(id));
        }
    }
}
