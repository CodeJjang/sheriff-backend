using System;
using System.Collections.Generic;
using OpenAlprApi.Api;
using OpenAlprApi.Client;
using OpenAlprApi.Model;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SherrifBackend.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            var apiInstance = new DefaultApi();
            var imageBytes = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"D:\Downloads\Junk\carphotos\20170606_143120.jpg"));
            var secretKey = "sk_68915d22026ad9d2e1d979ae";
            var country = "eu";
            var recognizeVehicle = 1;
            try
            {
                InlineResponse200 result = apiInstance.RecognizeBytes(imageBytes, secretKey, country, recognizeVehicle);
                return result.ToJson();
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
