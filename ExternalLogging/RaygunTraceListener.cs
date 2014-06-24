using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace ExternalLogging
{
    public class RaygunTraceListener : CustomTraceListener
    {
        public RaygunTraceListener()
            : this(new RaygunTraceListenerClient())
        {
        }

        public RaygunTraceListener(ITraceListenerClient client)
        {
            this.traceListenerClient = client;
        }

        /// <summary>
        /// Writes trace information, a data object and event information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data to emit.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    Write(((LogEntry)data).Message);
                }
                else if (data is string)
                {
                    Write(data as string);
                }
            }
        }

        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
            SendMessageToClient(message);
        }

        /// <summary>
        /// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            SendMessageToClient(message + Environment.NewLine);
        }

        private void SendMessageToClient(string message)
        {
            this.traceListenerClient.SendMessage(message);
        }

        private ITraceListenerClient traceListenerClient;
    }
}
