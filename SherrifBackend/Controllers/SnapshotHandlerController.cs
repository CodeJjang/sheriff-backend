using System;
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

namespace SherrifBackend.Controllers
{
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
            var secretKey = "sk_68915d22026ad9d2e1d979ae";
            var country = "eu";
            var recognizeVehicle = 1;
            try
            {
                InlineResponse200 httpResult = apiInstance.RecognizeBytes(base64Image, secretKey, country, recognizeVehicle);
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
                        Time = new DateTime()
                    };

                    SheriffModel.InsertVehicleLocation(location);
                    
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
