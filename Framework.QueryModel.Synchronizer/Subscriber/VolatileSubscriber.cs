using System.Threading.Tasks;

namespace Framework.QueryModel.Synchronizer.Subscriber
{
	public abstract class VolatileSubscriber<T> : ISubscriber where T : IProjection
	{
		/// <inheritdoc />
		public abstract Task SubscribeAsync();
	}
}