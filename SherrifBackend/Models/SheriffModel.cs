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

namespace SherrifBackend.Models
{
    public static class SheriffModel
    {
        private const string USERS_COLLECTION = "USERS";
        private const string BOUNTY_COLLECTION = "BOUNTIES";
        private const string LOCATION_COLLECTION = "LOCATIONS";
        private const string VEHICLE_COLLECTION = "VEHICLES";
        private const string STATISTIC_COLLECTION = "STATISTICS";

        public static void AddUser(User user)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<User> users = mongoDB.GetCollection<User>(USERS_COLLECTION);
            users.InsertOne(user);
        }

        public static void AddStolenVehicle(Vehicle vehicle)
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Vehicle> vehicles = mongoDB.GetCollection<Vehicle>(VEHICLE_COLLECTION);
            vehicles.InsertOne(vehicle);
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
            IMongoCollection<Bounty> bounties = mongoDB.GetCollection<Bounty>(BOUNTY_COLLECTION);
            var query = MongoDB.Driver.Builders<Bounty>.Filter.Eq("userId", userId);
            return bounties.Find<Bounty>(query).ToList();

        }

        public static void InsertVehicleLocation(Vehicle vehicle, double xCoordinate, double yCoordinate)
        {
            Location location = new Location();
            location.Time = DateTime.Now;
            location.VehicleObject = vehicle;
            location.Point = new GeoJson2DCoordinates(xCoordinate, yCoordinate);
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

        private static List<string> GetStolenCars()
        {
            IMongoDatabase mongoDB = ConnectionManager.GetMongoDatabase();
            IMongoCollection<Vehicle> vehicles = mongoDB.GetCollection<Vehicle>(VEHICLE_COLLECTION);
            return vehicles.Find(_ => true).Project<string>
                (Builders<Vehicle>.Projection.Include(f => f.LicensePlate)).ToList();
        }
    }
}