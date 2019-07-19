using EventStore.ClientAPI.SystemData;

namespace Framework.QueryModel.Synchronizer
{
	public static class DefaultUserCredentials
	{
		public static UserCredentials Value => new UserCredentials("admin", "changeit");
	}
}