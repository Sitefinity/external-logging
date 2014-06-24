using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Logging;
using Telerik.Microsoft.Practices.Unity;

namespace ExternalLogging
{
    public static class Installer
    {
        public static void PreApplicationStart()
        {
            Log.Configuring += Log_Configuring;
        }

        private static void Log_Configuring(object sender, LogConfiguringEventArgs e)
        {
            var defaultConfigurator = ObjectFactory.Resolve<ISitefinityLogCategoryConfigurator>();
            var customConfigurator = new RaygunConfigurator(defaultConfigurator, ConfigurationPolicy.ErrorLog);
            ObjectFactory.Container.RegisterInstance<ISitefinityLogCategoryConfigurator>(customConfigurator);
        }
    }
}
