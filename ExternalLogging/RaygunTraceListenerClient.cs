using System;
using System.Linq;
using System.Reflection;
using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.Messages;
using System.Web;

namespace ExternalLogging
{
    /// <summary>
    /// An <see cref="ITraceListenerClient"/> class for logging errors using the Mindscape.Raygun4Net API.
    /// </summary>
    public class RaygunTraceListenerClient : ITraceListenerClient
    {
        #region Constructors

        public RaygunTraceListenerClient()
        {
            var raygunAssembly = RaygunTraceListenerClient.GetRaygunAssemblyFromAppDomain();
            if (raygunAssembly == null)
            {
                throw new Exception("Raygun assembly is not added to the app domain of the web application!");
            }

            this.raygunClient = new RaygunClient();
        }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public void LogMessage(string message)
        {
            var raygunMessage = RaygunMessageBuilder.New
                .SetHttpDetails(HttpContext.Current)
                .SetEnvironmentDetails()
                .SetMachineName(Environment.MachineName)
                .SetClientDetails()
                .SetVersion(this.raygunClient.ApplicationVersion)
                .SetUser(this.raygunClient.User)
                .Build();

            raygunMessage.Details.Error = new RaygunErrorMessage() { Message = message };

            this.raygunClient.Send(raygunMessage);
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

        private const string raygunAssemblyName = "Mindscape.Raygun4Net";
        private static Assembly raygunAssembly;
        private RaygunClient raygunClient;

        #endregion
    }
}
