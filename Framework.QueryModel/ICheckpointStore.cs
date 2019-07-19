using System.Threading.Tasks;

namespace Framework.QueryModel
{
	public interface ICheckpointStore
	{
		Task Set(Checkpoint checkpoint);

		Task<Checkpoint> Get(string projectionName);
	}
}