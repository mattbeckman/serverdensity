using System.Collections.Generic;
using NUnit.Framework;

namespace Uptime.UnitTest
{
    [TestFixture]
    public class UptimeFixture
    {
        [Test]
        public void Should_retrieve_uptime_from_local_system()
        {
            var uptime = new Uptime();

            var result = (IDictionary<string, double>) uptime.DoCheck();

            Assert.That(result["Total Seconds"] > 0);
            Assert.That(result["Total Minutes"] > 0);
            Assert.That(result["Total Hours"] > 0);
            Assert.That(result["Total Days"] > 0);
        }
    }
}
