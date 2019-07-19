using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EventSource.Structure.Persistence.Sql.Context;
using EventSource.Structure.Persistence.Sql.Repositories;
using EventSource.Structure.QueryModels.BaseRepository;
using EventStore.ClientAPI;
using Framework.Config.Castle;
using Framework.Core;
using Framework.QueryModel;
using Framework.QueryModel.Synchronizer;
using System.Net;
using System.Reflection;
using Component = Castle.MicroKernel.Registration.Component;

namespace EventSource.Structure.QueryModel.Synchronizer.Config
{
	public static class Bootstrapper
	{
		public static IWindsorContainer Container { get; private set; }

		public static void WireUp()
		{
			Container = new WindsorContainer();

			Container.Register(Component.For<ICheckpointStore>().ImplementedBy<CheckpointStore>().LifestyleScoped());
			Container.Register(Classes.FromAssembly(Assembly.GetCallingAssembly()).Pick().WithServiceAllInterfaces().LifestyleScoped());
			Container.Register(Component.For<IEventStoreConnection>().UsingFactoryMethod(q => EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113))).LifestyleSingleton());
			Container.Register(Component.For<QueryContext>().LifestyleScoped());
			Container.Register(Component.For<IMediator>().ImplementedBy<WindsorMediator>().LifestyleScoped());
			Container.Register(Component.For<IServiceLocator>().ImplementedBy<WindsorServiceLocator>().LifestyleScoped());
			Container.Register(Component.For(typeof(IBaseRepository<>)).ImplementedBy(typeof(BaseRepository<>)).LifestyleScoped());

			Container.Register(Component.For<IWindsorContainer>().Instance(Container));
		}
	}
}