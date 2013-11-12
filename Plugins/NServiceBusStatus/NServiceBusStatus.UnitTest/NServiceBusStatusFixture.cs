using System.Collections.Generic;
using System.ServiceProcess;
using NUnit.Framework;

namespace ServiceStatus.UnitTest
{
    [TestFixture]
    public class NServiceBusStatusFixture
    {
        [Test]
        public void Should_locate_all_NSB_services()
        {
            var nsbStatus = new NServiceBusStatus();

            var results = (IDictionary<string, object>) nsbStatus.DoCheck();

            Assert.AreEqual((int)ServiceControllerStatus.Running, results["Publisher"]);
            Assert.AreEqual((int)ServiceControllerStatus.Running, results["Subscriber A"]);
            Assert.AreEqual((int)ServiceControllerStatus.Running, results["Subscriber B"]);
            Assert.AreEqual(results.Count, 3);
        }
    }
}
