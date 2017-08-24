using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SherrifBackend.Models.Entities
{
    public class Bounty
    {
        public string userId { get; set; }
        public string stolenPlateId { get; set; }
        public int amount { get; set; }
        public bool isPaid { get; set; }
    }
}