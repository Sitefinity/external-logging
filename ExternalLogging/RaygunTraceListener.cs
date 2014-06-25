using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace ExternalLogging
{
    /// <summary>
    /// A <see cref="CustomTraceListener"/> class for external error logging.
    /// </summary>
    public class RaygunTraceListener : CustomTraceListener
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunTraceListener"/> class.
        /// </summary>
        public RaygunTraceListener()
            : this(new RaygunTraceListenerClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunTraceListener"/> class.
        /// </summary>
        /// <param name="client">The <see cref="ITraceListenerClient"/> client which will be used for logging errors .</param>
        public RaygunTraceListener(ITraceListenerClient client)
        {
            this.traceListenerClient = client;
        }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    this.Write(((LogEntry)data).Message);
                }
                else if (data is string)
                {
                    this.Write(data as string);
                }
            }
        }

        /// <inheritdoc />
        public override void Write(string message)
        {
            this.traceListenerClient.LogMessage(message);
        }

        /// <inheritdoc />
        public override void WriteLine(string message)
        {
            this.traceListenerClient.LogMessage(message + Environment.NewLine);
        }

        #endregion

        #region Private members

        private ITraceListenerClient traceListenerClient;

        #endregion
    }
}
