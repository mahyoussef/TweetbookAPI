using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Installers
{
    public class CosmosInstaller 
    {
        //public void InstallServices(IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddSingleton<ICosmosPostDbService>(InitializeCosmosClientInstanceAsync(configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
        //}
        //private static async Task<CosmosPostService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        //{
        //    string databaseName = configurationSection.GetSection("DatabaseName").Value;
        //    string containerName = configurationSection.GetSection("ContainerName").Value;
        //    string account = configurationSection.GetSection("Account").Value;
        //    string key = configurationSection.GetSection("Key").Value;
        //    Microsoft.Azure.Cosmos.Fluent.CosmosClientBuilder clientBuilder = new Microsoft.Azure.Cosmos.Fluent.CosmosClientBuilder(account, key);
        //    Microsoft.Azure.Cosmos.CosmosClient client = clientBuilder
        //                        .WithConnectionModeDirect()
        //                        .Build();
        //    CosmosPostService cosmosDbService = new CosmosPostService(client, databaseName, containerName);
        //    Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        //    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

        //    return cosmosDbService;
        
    }
}

