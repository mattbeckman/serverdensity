using System.Collections.Generic;
using System.ServiceProcess;
using BoxedIce.ServerDensity.Agent.PluginSupport;
using Microsoft.Win32;

namespace ServiceStatus
{
    public class NServiceBusStatus : ICheck
    {
        public string Key
        {
            get { return "NServiceBusStatus"; }
        }

        public object DoCheck()
        {
            var serviceNames = GetServiceNames();

            return CheckServiceStatuses(serviceNames);
        }

        private IDictionary<string, object> CheckServiceStatuses(IList<string> serviceNames)
        {
            var nsbServiceStates = new Dictionary<string, object>();

            foreach (var serviceName in serviceNames)
            {
                var service = new ServiceController(serviceName);
                
                nsbServiceStates.Add(service.DisplayName, (int) service.Status);
            }

            return nsbServiceStates;
        }

        private IList<string> GetServiceNames()
        {
            var serviceNames = new List<string>();

            using (var servicesKey = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services"))
            {
                foreach (var serviceKey in servicesKey.GetSubKeyNames())
                {
                    using (var registryKey = servicesKey.OpenSubKey(serviceKey))
                    {
                        if (registryKey != null)
                        {
                            var imagePath = registryKey.GetValue("ImagePath");

                            if (imagePath != null && imagePath.ToString().Contains("NServiceBus.Host.exe"))
                                serviceNames.Add(serviceKey);
                        }
                    }
                }
            }

            return serviceNames;
        }
    }
}