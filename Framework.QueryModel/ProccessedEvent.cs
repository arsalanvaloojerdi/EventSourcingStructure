using System;

namespace Framework.QueryModel
{
	public class ProccessedEvent : IQueryModel
	{
		public Guid Id { get; set; }

		public Guid EventId { get; set; }

		public string Projection { get; set; }
	}
}