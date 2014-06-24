using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExternalLogging
{
    /// <summary>
    /// Defines interface for trace listener clients.
    /// </summary>
    public interface ITraceListenerClient
    {
        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogMessage(string message);
    }
}
