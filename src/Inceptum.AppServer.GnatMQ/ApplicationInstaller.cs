using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Inceptum.AppServer.Configuration;
using uPLibrary.Networking.M2Mqtt;

namespace Inceptum.Applications.GnatMQ
{
    public class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<ConfigurationFacility>(f => { });
            container.Register(Component.For<MqttBroker>());
        }
    }
}