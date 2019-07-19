using Framework.DomainModel.Events;
using System;

namespace EventSource.Structure.DomainModel.Events.Company
{
    public class EmployeeHired : DomainEvent
    {
        /// <inheritdoc />
        public EmployeeHired(Guid aggregateId, Guid employeeId, string firstName, string lastName, string nationalCode) : base(aggregateId)
        {
            this.EmployeeId = employeeId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.NationalCode = nationalCode;
        }

        public Guid EmployeeId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string NationalCode { get; private set; }
    }
}