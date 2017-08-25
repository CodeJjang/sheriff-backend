using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using SherrifBackend.Models.Utilities;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Timers;
using System.Configuration;
using SherrifBackend.Models.Entities;

namespace SherrifBackend.Models
{
    public static class IntersectionJob
    {
        private static Timer timer;
        private static float hours = float.Parse(ConfigurationManager.AppSettings["RelevantHours"]);

        public static void StartFindIntersections()
        {
            //timer = new Timer(10 * 1000);
            //timer.Elapsed += FindTargets;
            // Hook up the Elapsed event for the timer. 
            //timer.AutoReset = true;
            //timer.Enabled = true;
        }

        private static void FindTargets(Object source, ElapsedEventArgs e)
        {
            List<string> licensePlates = SheriffModel.GetTargets();
            DateTime fromTime = DateTime.Now.AddHours(-hours);
            List<Location> hitTargets = SheriffModel.FindTargets(licensePlates, fromTime);
            if (hitTargets != null && hitTargets.Count > 0)
            {
                var userTargets = hitTargets.GroupBy(
                    p => p.UserId,
                    p => p.VehicleObject.LicensePlate,
                    (key, g) => new { UserId = key, LicensePlates = g.ToList() });

                foreach (var item in userTargets)
                {
                    SheriffModel.UpdateFoundTargets(item.LicensePlates, item.UserId,null);
                }
            }
        }
    }
}