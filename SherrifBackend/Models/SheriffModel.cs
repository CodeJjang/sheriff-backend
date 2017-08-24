using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SherrifBackend.Models.Entities;
using System.Threading.Tasks;
using MongoDB.Driver.Core;
using System.Threading;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using SherrifBackend.Models.Utilities;

namespace SherrifBackend.Models
{
    public static class SheriffModel
    {
        private const string USERS_COLLECTION = "USERS";

        public static void AddUser(User user)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<User> users = mongoDB.GetCollection<User>(USERS_COLLECTION);
            users.InsertOne(user);
        }

        public static void SetUserAsSheriff(User user, bool isSheriff)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<User> users = mongoDB.GetCollection<User>(USERS_COLLECTION);
            var query = MongoDB.Driver.Builders<User>.Filter.Eq("Id", user.Id);
            var update = MongoDB.Driver.Builders<User>.Update.Set("IsSheriff", isSheriff);
            users.UpdateOne(query, update);
        }

        public static List<Bounty> GetUserBounties(string userId)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Bounty> users = mongoDB.GetCollection<Bounty>(USERS_COLLECTION);
            var query = MongoDB.Driver.Builders<User>.Filter.Eq("Id", userId);
            return null;

        }

        public static void InsertVehicle(Vehicle vehicle)
        {

        }
    }
}