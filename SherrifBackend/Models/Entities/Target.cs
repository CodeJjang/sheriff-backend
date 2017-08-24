using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
namespace SherrifBackend.Models.Entities
{
    public class Target
    {
        public ObjectId _id { get; set; }
        public string RequestedUserId { get; set; }
        public string FoundUserId { get; set; }
        public string VehicleLicensePlate { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}