using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExternalLogging
{
    public class RaygunTraceListenerClient : ITraceListenerClient
    {
        public void SendMessage(string message)
        {
            var raygunAssembly = GetRaygunAssemblyFromAppDomain();
            if (raygunAssembly == null)
            {
                throw new Exception("Raygun assembly is not added to the app domain of the web application!");
            }

            var raygunErrorMessage = new RaygunErrorMessage() { Message = message };
            var raygunMessageDetails = new RaygunMessageDetails() { Error = raygunErrorMessage };
            var raygunMessage = new RaygunMessage() { Details = raygunMessageDetails };
            var raygunClient = new RaygunClient(RaygunTraceListenerClient.raygunApiKey);

            raygunClient.Send(raygunMessage);
        }

        private static Assembly GetRaygunAssemblyFromAppDomain()
        {
            if (raygunAssembly == null)
            {
                var assemblyName = RaygunTraceListenerClient.raygunAssemblyName;
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                raygunAssembly = (from a in assemblies
                                  where a.GetName().Name == assemblyName
                                  select a).SingleOrDefault();
            }
            return raygunAssembly;
        }

        private const string raygunApiKey = "fzGOnvs9sCZlieiyDMgphA==";
        private const string raygunAssemblyName = "Mindscape.Raygun4Net";
        private static Assembly raygunAssembly;
    }
}
