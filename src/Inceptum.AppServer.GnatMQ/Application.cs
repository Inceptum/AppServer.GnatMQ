using System;
using Castle.Core.Logging;
using Inceptum.AppServer;
using uPLibrary.Networking.M2Mqtt;


namespace Inceptum.Applications.GnatMQ
{
    public class Application : IHostedApplication, IDisposable
    {
        private readonly ILogger m_Logger;
        private readonly MqttBroker m_MqttBroker;

        public Application(ILogger logger, MqttBroker mqttBroker)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            if (mqttBroker == null) throw new ArgumentNullException("mqttBroker");
            m_Logger = logger;
            m_MqttBroker = mqttBroker;
        }
        public void Start()
        {

            var traceLevel = uPLibrary.Networking.M2Mqtt.Utility.TraceLevel.Error;
            if (m_Logger.IsDebugEnabled)
                traceLevel |= uPLibrary.Networking.M2Mqtt.Utility.TraceLevel.Verbose | uPLibrary.Networking.M2Mqtt.Utility.TraceLevel.Frame;

            if (m_Logger.IsInfoEnabled)
                traceLevel |= uPLibrary.Networking.M2Mqtt.Utility.TraceLevel.Information;

            if (m_Logger.IsWarnEnabled)
                traceLevel |= uPLibrary.Networking.M2Mqtt.Utility.TraceLevel.Warning;

            uPLibrary.Networking.M2Mqtt.Utility.Trace.TraceLevel = traceLevel;

            uPLibrary.Networking.M2Mqtt.Utility.Trace.TraceListener = traceListener;

            m_MqttBroker.Start();
        }

        private void traceListener(string format, object[] args)
        {
            m_Logger.DebugFormat(format, args);
        }

        public void Dispose()
        {
            m_MqttBroker.Stop();
        }
    }
}
