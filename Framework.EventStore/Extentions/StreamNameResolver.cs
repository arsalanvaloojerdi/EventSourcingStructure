using Framework.DomainModel.Core;
using System;

namespace Framework.Persistence.EventStore.Extentions
{
	public static class StreamNameResolver
	{
		public static string Resolve(object id, Type type)
		{
			return $"EventSourceStructure-{type.Name}-{id}";
		}

		public static string ResolveForSnapshot(IAggregateRoot aggregate)
		{
			return $"EventSourceStructure-{aggregate.GetType().Name}-snapshot-{aggregate.GetId()}";
		}

		public static string ResolveForSnapshot(object id, Type type)
		{
			return $"EventSourceStructure-{type.Name}-snapshot-{id}";
		}
	}
}