using SherrifBackend.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Target oz = new Target()
            {
                Amount = 200,
                FoundUserId = null,
                IsPaid = false,
                VehicleLicensePlate = "2666767",
                RequestedUserId = "123"
            };

            SherrifBackend.Models.SheriffModel.AddTarget(oz);
        }
    }
}
