using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using millisecondInterviewTask.Interfaces;
using millisecondInterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace millisecondInterviewTask.DAL
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly string _dbName;
        private readonly string _collectionName;
        private readonly Uri _dbUri;
        private readonly string _dbKey;
        private readonly DocumentClient _client;

        public DataAccessLayer(IConfiguration configuration)
        {
            _dbName = configuration.GetValue<string>("DBConfig:DbName");
            _collectionName = configuration.GetValue<string>("DBConfig:CollectionName");
            _dbUri = new Uri(configuration.GetValue<string>("DBConfig:DbConnection:URI"));
            _dbKey = configuration.GetValue<string>("DBConfig:DbConnection:primaryKey");
            
            _client = new DocumentClient(_dbUri, _dbKey);
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = "DeploymentsDB" }).Wait();
            _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_dbName), new DocumentCollection { Id = "_collectionName" }).Wait();
        }

        public async Task CreateDocumentAsync(DeploymentRoot deploymentObject)
        {
            try
            {
                await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_dbName, _collectionName), deploymentObject);
            }
            catch (DocumentClientException ex)
            {

                throw new ApplicationException("Error creating document.", ex);
            }
            
        }

        public List<DeploymentRoot> GetAllDocumentsAsync()
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<DeploymentRoot> deploymentQuery = _client.CreateDocumentQuery<DeploymentRoot>(
                UriFactory.CreateDocumentCollectionUri(_dbName, _collectionName), queryOptions);
            return deploymentQuery.ToList();            
        }


    }
}
