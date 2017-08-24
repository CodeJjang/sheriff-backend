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

namespace SherrifBackend.Controllers
{
    public class SnapshotHandlerController : ApiController
    {
        // GET api/values/5
        public string Get(int id)
        {
            var apiInstance = new DefaultApi();
            var imageBytes = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"D:\Downloads\Junk\carphotos\o5myytqmyt1z.jpg"));
            var secretKey = "sk_68915d22026ad9d2e1d979ae";
            var country = "eu";
            var recognizeVehicle = 1;
            try
            {
                InlineResponse200 httpResult = apiInstance.RecognizeBytes(imageBytes, secretKey, country, recognizeVehicle);
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
                    Location loc = new Location()
                    {
                        VehicleLicensePlate = vehicle.LicensePlate,
                        Point = new GeoJson2DCoordinates(0, 0),
                        Time = new DateTime()
                    };
                    
                }

                return "ok";
            }
            catch(Exception c)
            {
                return c.ToString();
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
