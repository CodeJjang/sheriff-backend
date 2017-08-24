using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace SherrifBackend.Models.Entities
{
    public class Location 
    {
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public Vehicle VehicleObject { get; set; }
        public GeoJson2DCoordinates Point { get; set; }
        public DateTime Time { get; set; }
        public string LicensePlate { get; set; }

    }
}