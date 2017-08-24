using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace SherrifBackend.Models.Entities
{
    public class Vehicle
    {
        public ObjectId _id { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string MakeModel { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
    }
}