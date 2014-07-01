using System;
using System.Linq;
using System.Reflection;
using Mindscape.Raygun4Net;
using Telerik.Sitefinity.Services;

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
            this.raygunClient.IgnoreCookieNames(new string[] { "ASP.NET_SessionId", ".ASPXAUTH", ".ASPXANONYMOUS", "AuthSession", "FedAuth", "FedAuth1", "SF-TokenId" });
        }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public void LogMessage(string message)
        {
            Exception ex;
            if (SystemManager.CurrentHttpContext.Error != null)
            {
                ex = SystemManager.CurrentHttpContext.Error;
            }
            else
            {
                ex = new Exception(message);
            }

            this.raygunClient.SendInBackground(ex);
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
