using System.Threading.Tasks;

namespace Framework.QueryModel.Synchronizer.Subscriber
{
	public interface ISubscriber
	{
		Task SubscribeAsync();
	}
}