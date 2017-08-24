using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SherrifBackend.Models.Entities
{
    public class Target
    {
        public string RequestedUserId { get; set; }
        public string FoundUserId { get; set; }
        public string StolenPlatePlate { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}