using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExternalLogging
{
    /// <summary>
    /// An <see cref="ITraceListenerClient"/> class for logging errors using the Mindscape.Raygun4Net API.
    /// </summary>
    public class RaygunTraceListenerClient : ITraceListenerClient
    {
        #region Public methods

        /// <inheritdoc />
        public void LogMessage(string message)
        {
            var raygunAssembly = RaygunTraceListenerClient.GetRaygunAssemblyFromAppDomain();
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
        
        #endregion

        #region Private methods

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

        #endregion

        #region Private members

        private const string raygunApiKey = "YOUR_RAYGUN_API_KEY";
        private const string raygunAssemblyName = "Mindscape.Raygun4Net";
        private static Assembly raygunAssembly;

        #endregion
    }
}
