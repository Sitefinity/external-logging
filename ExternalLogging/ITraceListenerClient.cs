using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExternalLogging
{
    public interface ITraceListenerClient
    {
        void SendMessage(string message);
    }
}
