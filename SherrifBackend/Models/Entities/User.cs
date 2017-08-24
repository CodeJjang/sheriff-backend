using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SherrifBackend.Models.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsSheriff { get; set; }
    }
}