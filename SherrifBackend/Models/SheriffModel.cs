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
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Driver.Core;

namespace SherrifBackend.Models
{
    public static class SheriffModel
    {
        private const string USERS_COLLECTION = "USERS";
        private const string BOUNTY_COLLECTION = "BOUNTIES";
        private const string LOCATION_COLLECTION = "LOCATIONS";
        private const string VEHICLE_COLLECTION = "VEHICLES";
        private const string TARGET_COLLECTION = "TARGETS";
        private const string STATISTIC_COLLECTION = "STATISTICS";

        public static void AddUser(User user)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<User> users = mongoDB.GetCollection<User>(USERS_COLLECTION);
            users.InsertOne(user);
        }

        public static void AddTarget(Target target)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Target> targets = mongoDB.GetCollection<Target>(TARGET_COLLECTION);
            targets.InsertOne(target);
        }

        public static void SetUserAsSheriff(User user, bool isSheriff)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<User> users = mongoDB.GetCollection<User>(USERS_COLLECTION);
            var query = MongoDB.Driver.Builders<User>.Filter.Eq("Id", user.Id);
            var update = MongoDB.Driver.Builders<User>.Update.Set("IsSheriff", isSheriff);
            users.UpdateOne(query, update);
        }

        public static List<Target> GetUserBounties(string userId)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Target> targets = mongoDB.GetCollection<Target>(TARGET_COLLECTION);
            var query = MongoDB.Driver.Builders<Target>.Filter.Eq("FoundUserId", userId);
            return targets.Find<Target>(query).ToList();

        }

        public static void InsertVehicleLocation(Location location)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Location> locations = mongoDB.GetCollection<Location>(LOCATION_COLLECTION);
            locations.InsertOne(location);
        }

        public static Statistic GetUserStatistics(string userId)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Statistic> statistics = mongoDB.GetCollection<Statistic>(STATISTIC_COLLECTION);
            var query = MongoDB.Driver.Builders<Statistic>.Filter.Eq("userId", userId);
            return statistics.Find<Statistic>(query).FirstOrDefault();
        }

        public static List<string> GetTargets()
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Target> targets = mongoDB.GetCollection<Target>(TARGET_COLLECTION);
            List<string> returnValue = new List<string>();
            targets.Find(x => x.IsPaid == false).Project<Target>
                (Builders<Target>.Projection.Include(f => f.VehicleLicensePlate)).ToList().ForEach(x => returnValue.Add(x.VehicleLicensePlate) );
            return returnValue;
        }

        public static List<Location> FindTargets(List<string> LicensePlate, DateTime fromTime)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Location> locations = mongoDB.GetCollection<Location>(LOCATION_COLLECTION);

            string query = "{ LicensePlate: { $in:[";
            foreach (var item in LicensePlate)
            {
                query += "'" + item + "',";
            }
            query = query.Substring(0, query.Length - 1) +"]}}";
            return locations.Find<Location>(query).ToList();
        }

        public static void UpdateFoundTargets(List<string> LicensePlates, string userId)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Target> targets = mongoDB.GetCollection<Target>(TARGET_COLLECTION);
            string query = "{ VehicleLicensePlate: { $in:[";
            foreach (var item in LicensePlates)
            {
                query += "'" + item + "',";
            }
            query = query.Substring(0, query.Length - 1) + "]}}";
            var update = MongoDB.Driver.Builders<Target>.Update.Set("IsPaid", true).Set("FoundUserId", userId);
            targets.UpdateMany(query, update);
        }
    }
}