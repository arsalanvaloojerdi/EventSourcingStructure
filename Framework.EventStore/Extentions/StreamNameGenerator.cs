using Framework.DomainModel.Core;

namespace Framework.Persistence.EventStore.Extentions
{
	public static class StreamNameGenerator
	{
		public static string Generate(IAggregateRoot aggregate)
		{
			return $"EventSourceStructure-{aggregate.GetType().Name}-{aggregate.GetId()}";
		}

		public static string GenerateForSnapshot(IAggregateRoot aggregate)
		{
			return $"EventSourceStructure-{aggregate.GetType().Name}-snapshot-{aggregate.GetId()}";
		}
	}
}