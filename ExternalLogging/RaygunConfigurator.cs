using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Logging;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace ExternalLogging
{
    /// <summary>
    /// The built-in <see cref="ISitefinityLogCategoryConfigurator"/> implementation
    /// configuring Raygun logging.
    /// </summary>
    public class RaygunConfigurator : ISitefinityLogCategoryConfigurator
    {
        public RaygunConfigurator(ISitefinityLogCategoryConfigurator next, ConfigurationPolicy policy)
        {
            this.next = next;
            this.policy = policy;
        }

        /// <inheritdoc />
        public void Configure(SitefinityLogCategory category)
        {
            if (category.Name == this.policy.ToString())
            {
                category.Configuration
                    .WithOptions
                    .SendTo
                    .Custom<RaygunTraceListener>("Custom");
            }
            else
            {
                // Forward to the default configurator.
                this.next.Configure(category);
            }
        }

        private ISitefinityLogCategoryConfigurator next;
        private ConfigurationPolicy policy;
    }
}
