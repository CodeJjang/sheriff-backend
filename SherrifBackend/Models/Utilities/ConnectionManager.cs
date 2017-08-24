using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MongoDB.Driver.Core;
using System.Threading;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SherrifBackend.Models.Utilities
{
    public static class ConnectionManager
    {
        #region Data Members

        private static MongoClient _mongoClient;

        #endregion

        #region Public Methods
        public static MongoClient GetMongoClient(string connectionString)
        {
            if (_mongoClient == null)
            {
                _mongoClient = new MongoClient(connectionString);
            }

            return _mongoClient;
        }

        // Connect to local host default configuration
        public static MongoClient GetMongoClient()
        {
            return GetMongoClient(ConfigurationManager.AppSettings["MongoHost"]);
        }

        public static IMongoDatabase GetMongoDatabase()
        {
            MongoClient client = ConnectionManager.GetMongoClient();
            return client.GetDatabase(ConfigurationManager.AppSettings["MongoDatabase"]);
        }

        public static IMongoCollection<BsonDocument> GetMongoCollection(string dbName, string collectionName)
        {
            MongoClient client = ConnectionManager.GetMongoClient();
            IMongoDatabase mongoDB = client.GetDatabase(dbName);
            return mongoDB.GetCollection<BsonDocument>(collectionName);
        }

        #endregion
    }
}