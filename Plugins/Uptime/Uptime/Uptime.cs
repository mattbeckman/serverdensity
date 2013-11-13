using System;
using System.Collections.Generic;
using System.Diagnostics;
using BoxedIce.ServerDensity.Agent.PluginSupport;

namespace Uptime
{
    public class Uptime : ICheck
    {
        public string Key
        {
            get { return "Uptime"; }
        }

        public object DoCheck()
        {
            var result = new Dictionary<string, double>();

            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                uptime.NextValue(); // initial value always zero
                var uptimeValue = uptime.NextValue();

                result.Add("Total Seconds", TimeSpan.FromSeconds(uptimeValue).TotalSeconds);
                result.Add("Total Minutes", TimeSpan.FromSeconds(uptimeValue).TotalMinutes);
                result.Add("Total Hours", TimeSpan.FromSeconds(uptimeValue).TotalHours);
                result.Add("Total Days", TimeSpan.FromSeconds(uptimeValue).TotalDays);
            }

            return result;
        }
    }
}
