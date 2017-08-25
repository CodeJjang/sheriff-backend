﻿using System;
using System.Collections.Generic;
using OpenAlprApi.Api;
using OpenAlprApi.Client;
using OpenAlprApi.Model;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SherrifBackend.Models.Entities;
using MongoDB.Driver.GeoJsonObjectModel;
using SherrifBackend.Models;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace SherrifBackend.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/SnapshotHandler")]
    public class SnapshotHandlerController : ApiController
    {
        [HttpPost]
        [Route("receive")]
        public string receive()
        {
            string Content = Request.Content.ReadAsStringAsync().Result.ToString();
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary <string,string>>(Content);
            string lon = param["lon"];
            string lat = param["lat"];
            var apiInstance = new DefaultApi();
            var base64Image = param["base64Image"];
            //var imageBytes = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"D:\Downloads\Junk\carphotos\o5myytqmyt1z.jpg"));
            var secretKey = "sk_f162e8d21af8177056a8ab50";
            var country = "eu";
            var recognizeVehicle = 1;
            try
            {
                InlineResponse200 httpResult = apiInstance.RecognizeBytes(base64Image, secretKey, country, recognizeVehicle);
                List<string> licenses = new List<string>();
                foreach (var result in httpResult.Results)
                {
                    Vehicle vehicle = new Vehicle()
                    {
                        Color = result.Vehicle.Color.First().Name,
                        Make = result.Vehicle.Make.First().Name,
                        Type = result.Vehicle.BodyType.First().Name,
                        MakeModel = result.Vehicle.BodyType.First().Name,
                        LicensePlate = result.Plate
                    };
                    Location location = new Location()
                    {
                        LicensePlate = vehicle.LicensePlate,
                        VehicleObject = vehicle,
                        Point = new GeoJson2DCoordinates(double.Parse(lon), double.Parse(lat)),
                        Time = new DateTime(),
                        UserId = param["userId"]
                    };
                    licenses.Add(vehicle.LicensePlate);
                    SheriffModel.InsertVehicleLocation(location);
                    
                }

                if(licenses.Count > 0)
                {
                    List<Target> targets = SheriffModel.FindTargetByLicensePlate(licenses);
                    if(targets.Count > 0)
                    {
                        List<string> catched = new List<string>();
                        targets.ForEach(x => catched.Add(x.VehicleLicensePlate));
                        SheriffModel.UpdateFoundTargets(catched, param["userId"], new GeoJson2DCoordinates(double.Parse(lon), double.Parse(lat)));
                        return Newtonsoft.Json.JsonConvert.SerializeObject(targets);
                    }
                }

                return "";
            }
            catch(Exception c)
            {
                return "err";
                // TODO : well. shit happen's.
            }
        }
    }
}
