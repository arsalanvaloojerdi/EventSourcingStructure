using Framework.DomainModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Persistence.EventStore
{
	public static class TypeResolver
	{
		public static Dictionary<string, Type> DomainEventTypes =
			AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => typeof(DomainEvent).IsAssignableFrom(p)).ToDictionary(q => q.Name, q => q);

		public static Dictionary<string, Type> SnapshotTypes =
			AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => typeof(DomainModel.Core.Snapshot).IsAssignableFrom(p)).ToDictionary(q => q.Name, q => q);

		public static Type ResolveDomainEventType(string name)
		{
			return DomainEventTypes[name];
		}

		public static Type ResolveSnapshotType(string name)
		{
			return SnapshotTypes[name];
		}
	}
}