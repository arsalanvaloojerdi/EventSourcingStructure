using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EventSource.Structure.Persistence.Sql.Context;
using EventSource.Structure.Persistence.Sql.Repositories;
using EventSource.Structure.QueryFacade.Implements;
using EventSource.Structure.QueryFacade.Interfaces;
using EventSource.Structure.QueryModels.BaseRepository;
using System.Reflection;
using System.Web.Http;
using Component = Castle.MicroKernel.Registration.Component;

namespace EventSource.Structure.Config.QueryApi
{
    public static class Bootstrapper
    {
        public static IWindsorContainer Container { get; private set; }

        public static void WireUp()
        {
            Container = new WindsorContainer();

            Container.Register(Classes.FromAssembly(Assembly.GetCallingAssembly()).BasedOn<ApiController>()
                .LifestyleScoped());

            RegisterQueryFacade();
            RegisterRepositories();
        }

        private static void RegisterQueryFacade()
        {
	        Container.Register(Component.For<ICompanyQueryFacade>()
		        .ImplementedBy<CompanyQueryFacade>()
		        .LifestyleScoped());
        }

        private static void RegisterRepositories()
        {
	        Container.Register(Component.For<QueryContext>().LifestyleScoped());
	        Container.Register(
		        Component.For(typeof(IBaseRepository<>))
			        .ImplementedBy(typeof(BaseRepository<>))
			        .LifestyleScoped());
        }
    }
}