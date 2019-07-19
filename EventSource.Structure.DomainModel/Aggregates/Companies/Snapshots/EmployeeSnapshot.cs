using System;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.Snapshots
{
    public class EmployeeSnapshot
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalCode { get; set; }
    }
}