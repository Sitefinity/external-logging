using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Logging;
using Telerik.Microsoft.Practices.Unity;

namespace ExternalLogging
{
    /// <summary>
    /// An installer class that executes logic on the Sitefinity application start.
    /// </summary>
    public static class Installer
    {
        #region Public methods

        /// <summary>
        /// This is the actual method that is called by ASP.NET even before application start.
        /// </summary>
        public static void PreApplicationStart()
        {
            Log.Configuring += Log_Configuring;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the Configuring event of the Log control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LogConfiguringEventArgs"/> instance containing the event data.</param>
        private static void Log_Configuring(object sender, LogConfiguringEventArgs e)
        {
            var defaultConfigurator = ObjectFactory.Resolve<ISitefinityLogCategoryConfigurator>();
            var customConfigurator = new RaygunConfigurator(defaultConfigurator, ConfigurationPolicy.ErrorLog);
            ObjectFactory.Container.RegisterInstance<ISitefinityLogCategoryConfigurator>(customConfigurator);
        }

        #endregion
    }
}
