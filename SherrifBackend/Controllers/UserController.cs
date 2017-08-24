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
using SherrifBackend.Models.Entities;
using SherrifBackend.Models;

namespace SherrifBackend.Controllers
{
    [RoutePrefix("api/add")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public void add()
        {
            string Content = Request.Content.ReadAsStringAsync().Result.ToString();
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Content);
            User user = new User()
            {
                Id = param["id"].ToString(),
                Address = param["Address"].ToString(),
                Name = param["Name"].ToString(),
                Fid = param["Fid"].ToString(),
                IsSheriff = (bool)param["IsSheriff"],
                FacebookPicUrl = param["FacebookPicUrl"].ToString()
            };
            SheriffModel.AddUser(user);
        }
    }
}
