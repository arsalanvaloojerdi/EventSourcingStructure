using Framework.DomainModel.Core;
using System.Collections.Generic;
using System.Linq;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.Snapshots
{
	public class CompanySnapshot : Snapshot
	{
		public CompanySnapshot(Company company) : base(company.Id, company.CurrentVersion)
		{
			this.Name = company.Name.Value;
			this.NationalId = company.NationalId.Id;
			this.Employees = company.Employees.Select(q => new EmployeeSnapshot
			{
				Id = q.Id,
				FirstName = q.FullName.FirstName,
				LastName = q.FullName.LastName,
				NationalCode = q.NationalCode.Code
			});
		}

		public string Name { get; set; }

		public string NationalId { get; set; }

		public IEnumerable<EmployeeSnapshot> Employees { get; set; }

		public CompanySnapshot() { }
	}
}