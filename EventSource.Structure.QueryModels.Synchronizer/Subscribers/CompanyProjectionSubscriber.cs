using EventSource.Structure.QueryModels.Synchronizer.Projections;
using EventStore.ClientAPI;
using Framework.Core;
using Framework.QueryModel;
using Framework.QueryModel.Synchronizer;
using Framework.QueryModel.Synchronizer.Subscriber;

namespace EventSource.Structure.QueryModels.Synchronizer.Subscribers
{
	public class CompanyProjectionSubscriber : CatchupSubscriber<CompaniesProjection>
	{
		public CompanyProjectionSubscriber(
			IEventStoreConnection connection,
			ICheckpointStore checkpointStore,
			IMediator mediator, IServiceLocator serviceLocator) : base(connection, checkpointStore, mediator, serviceLocator) { }
	}
}