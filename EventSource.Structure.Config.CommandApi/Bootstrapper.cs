using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EventSource.Structure.CommandHandlers.Company;
using EventSource.Structure.DomainModel.Aggregates.Companies.Snapshots;
using EventSource.Structure.DomainModel.Events.Company;
using EventSource.Structure.Infrastructure.Logging.Context;
using EventSource.Structure.Infrastructure.Logging.Implements;
using EventSource.Structure.Persistence.EventStore.Repositories;
using Framework.Application;
using Framework.Application.Contracts;
using Framework.Config.Castle;
using Framework.Persistence.EventStore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace EventSource.Structure.Config.CommandApi
{
	public static class Bootstrapper
	{
		public static IWindsorContainer Container { get; private set; }

		public static void WireUp()
		{
			Container = FrameworkBootstrapper.WireUp();

			Container.Register(Classes.FromAssembly(Assembly.GetCallingAssembly()).BasedOn<ApiController>()
				.LifestyleScoped());

			InitializeTypeResolver();
			RegisterCommandHandlers();
			RegisterRepositories();
			RegisterLogger();
		}

		#region InternalMethods

		private static void InitializeTypeResolver()
		{
			TypeResolver.DomainEventTypes = new Dictionary<string, Type>
			{
				{typeof(CompanyRegistered).Name, typeof(CompanyRegistered)},
				{typeof(EmployeeHired).Name, typeof(EmployeeHired)},
				{typeof(CompanySnapshot).Name, typeof(CompanySnapshot)}
			};

			TypeResolver.SnapshotTypes = new Dictionary<string, Type>
			{
				{typeof(CompanySnapshot).Name, typeof(CompanySnapshot)}
			};
		}

		private static void RegisterCommandHandlers()
		{
			Container.Register(Classes.FromAssemblyContaining<CompanyCommandHandlers>()
				.BasedOn(typeof(ICommandHandlerAsync<>))
				.WithServiceAllInterfaces()
				.LifestyleScoped());
		}

		private static void RegisterRepositories()
		{
			Container.Register(Classes.FromAssemblyContaining<CompanyRepository>()
				.Pick()
				.WithServiceAllInterfaces()
				.LifestyleScoped());
		}

		private static void RegisterLogger()
		{
			Container.Register(Component.For<LogContext>().ImplementedBy<LogContext>().LifestyleScoped());
			Container.Register(Component.For<ICommandLogger>().ImplementedBy<CommandLogger>().LifestyleScoped());
		}

		#endregion
	}
}