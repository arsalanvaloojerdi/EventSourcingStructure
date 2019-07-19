using EventSource.Structure.DomainModel.Aggregates.Companies.ValueObjects;
using Framework.DomainModel.BuildingBlocks.Entities;
using System;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.Entities
{
    public class Employee : EntityBase<Guid>
    {
        internal Employee(Guid id, FullName fullName, NationalCode nationalCode) : base(id)
        {
            this.FullName = fullName;
            this.NationalCode = nationalCode;
        }

        /// <inheritdoc />
        public Employee(FullName fullName, NationalCode nationalCode) : base(Guid.NewGuid())
        {
            this.FullName = fullName;
            this.NationalCode = nationalCode;
        }

        public FullName FullName { get; private set; }

        public NationalCode NationalCode { get; private set; }
    }
}