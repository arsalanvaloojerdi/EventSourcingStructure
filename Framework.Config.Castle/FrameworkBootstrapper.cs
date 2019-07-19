using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EventStore.ClientAPI;
using Framework.Application;
using Framework.Core;
using Framework.Persistence.Core;
using Framework.Persistence.EventStore.Repository;
using Framework.Persistence.EventStore.Snapshot;
using Framework.Persistence.EventStore.UnitOfWork;
using System.Net;

namespace Framework.Config.Castle
{
	public static class FrameworkBootstrapper
	{
		public static IWindsorContainer WireUp()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<IWindsorContainer>().Instance(container));

			RegisterCommandHandlerDecorators(container);
			RegisterBaseRepository(container);
			RegisterUnitOfWork(container);
			RegisterServiceLocator(container);
			RegisterCommandBus(container);

			return container;
		}

		#region InternalMethods

		private static void RegisterCommandHandlerDecorators(IWindsorContainer container)
		{
			container.Register(Component.For(typeof(LoggingCommandHandlerDecorator<>)).LifestyleScoped());
		}

		private static void RegisterBaseRepository(IWindsorContainer container)
		{
			container.Register(Component.For<IEventStoreConnection>()
				.UsingFactoryMethod(q => EventStoreConnection.Create(
					ConnectionSettings.Create(),
					new IPEndPoint(IPAddress.Loopback, 1113))).LifestyleSingleton());

			container.Resolve<IEventStoreConnection>().ConnectAsync().Wait();

			container.Register(Component.For<ISnapshotReader>().ImplementedBy<SnapshotReader>().LifestyleSingleton());
			container.Register(Component.For<ISnapshotter>().ImplementedBy<Snapshotter>().LifestyleSingleton());

			container.Register(Component.For(typeof(IBaseRepository<>))
				.ImplementedBy(typeof(EventStoreBaseRepository<>)).LifestyleScoped());
		}

		private static void RegisterUnitOfWork(IWindsorContainer container)
		{
			container.Register(Component.For<IUnitOfWork>()
				.ImplementedBy<EventStoreUnitOfWork>().LifestyleScoped());
		}

		private static void RegisterServiceLocator(IWindsorContainer container)
		{
			container.Register(Component.For<IServiceLocator>()
				.ImplementedBy<WindsorServiceLocator>()
				.LifestyleSingleton());
		}

		private static void RegisterCommandBus(IWindsorContainer container)
		{
			container.Register(Component.For<ICommandBus>()
				.ImplementedBy<InMemoryCommandBus>()
				.LifestyleSingleton());
		}

		#endregion
	}
}