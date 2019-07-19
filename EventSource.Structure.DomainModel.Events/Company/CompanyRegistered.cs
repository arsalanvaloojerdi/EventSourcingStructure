using Framework.DomainModel.Events;
using System;

namespace EventSource.Structure.DomainModel.Events.Company
{
	public class CompanyRegistered : DomainEvent
	{
		/// <inheritdoc />
		public CompanyRegistered(Guid aggregateId, string name, string nationalId) : base(aggregateId)
		{
			this.Name = name;
			this.NationalId = nationalId;
		}

		public string Name { get; private set; }

		public string NationalId { get; private set; }

		private CompanyRegistered() : base(Guid.NewGuid()) { }
	}
}