using SherrifBackend.Models;
using SherrifBackend.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SherrifBackend.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/bounty")]
    public class BountyController : ApiController 
    {
        [HttpPost]
        [Route("GetUserBounties")]
        public List<Target> GetUserBounties()
        {
            string Content = Request.Content.ReadAsStringAsync().Result.ToString();
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(Content);
            return SheriffModel.GetUserBounties(param["userId"]);
        }

        [HttpGet]
        [Route("GetUserBounties")]
        public string GetGetUserBounties(string userId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(SheriffModel.GetUserBounties(userId));
        }
    }
}