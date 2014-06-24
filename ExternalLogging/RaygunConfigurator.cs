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
    /// An <see cref="ISitefinityLogCategoryConfigurator"/> 
    /// implementation for configuring Raygun logging.
    /// </summary>
    public class RaygunConfigurator : ISitefinityLogCategoryConfigurator
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunConfigurator"/> class.
        /// </summary>
        /// <param name="next">
        /// The default configurator to be used. 
        /// If the current configurator cannot handle the call it will forward it to the next one.
        /// </param>
        /// <param name="policy">The configuration policy for which the trace listener will be called.</param>
        public RaygunConfigurator(ISitefinityLogCategoryConfigurator next, ConfigurationPolicy policy)
        {
            this.next = next;
            this.policy = policy;
        }

        #endregion

        #region Public methods

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
                this.next.Configure(category);
            }
        }

        #endregion

        #region Private members

        private ISitefinityLogCategoryConfigurator next;
        private ConfigurationPolicy policy;

        #endregion
    }
}
