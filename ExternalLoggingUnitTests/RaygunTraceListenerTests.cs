using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExternalLogging;

namespace ExternalLoggingUnitTests
{
    [TestClass]
    public class RaygunTraceListenerTests
    {
        [TestMethod]
        public void Write_SendsMessageToTraceListenerClient()
        {
            var message = "test message";
            var fakeTraceListerClient = new FakeTraceListenerClient();
            var raygunTraceListener = new RaygunTraceListener(fakeTraceListerClient);

            raygunTraceListener.Write(message);
            var actualMessage = fakeTraceListerClient.Message;

            Assert.AreEqual(message, actualMessage);
        }

        [TestMethod]
        public void WriteLine_SendsMessageToTraceListenerClient()
        {
            var message = "test message";
            var fakeTraceListerClient = new FakeTraceListenerClient();
            var raygunTraceListener = new RaygunTraceListener(fakeTraceListerClient);

            raygunTraceListener.WriteLine(message);
            var actualMessage = fakeTraceListerClient.Message;

            Assert.AreEqual(message + Environment.NewLine, actualMessage);
        }

        [TestMethod]
        public void TraceData_SendsMessageToTraceListenerClient()
        {
            var message = "test message";
            var fakeTraceListerClient = new FakeTraceListenerClient();
            var raygunTraceListener = new RaygunTraceListener(fakeTraceListerClient);

            raygunTraceListener.TraceData(null, null, System.Diagnostics.TraceEventType.Error, 0, message);

            var actualMessage = fakeTraceListerClient.Message;

            Assert.AreEqual(message, actualMessage);
        }

        public class FakeTraceListenerClient : ITraceListenerClient
        {
            public string Message { get; private set; }

            public void LogMessage(string message)
            {
                this.Message = message;
            }
        }
    }
}
