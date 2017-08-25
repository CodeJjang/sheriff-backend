using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SherrifBackend.Models.Entities;
using SherrifBackend.Models;
using System.Web.Http.Cors;

namespace SherrifBackend.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/target")]
    public class TargetController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public void add()
        {
            string Content = Request.Content.ReadAsStringAsync().Result.ToString();
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Content);
            Target target = new Target()
            {
                Amount = int.Parse(param["amount"].ToString()),
                FoundUserId = null,
                IsPaid = false,
                RequestedUserId = "PoliceID-100",
                VehicleLicensePlate = param["plateNumber"].ToString()
            };
            SheriffModel.AddTarget(target);
        }
    }
}