using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.GeoJsonObjectModel;

namespace SherrifBackend.Models.Entities
{
    public class Location
    {
        public Vehicle VehicleObject { get; set; }
        public GeoJson2DCoordinates Point { get; set; }
        public DateTime Time { get; set; }
    }
}