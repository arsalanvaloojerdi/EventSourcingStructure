using Framework.QueryModel;
using System;
using System.Collections.Generic;

namespace EventSource.Structure.QueryModels.QueryModels.Company
{
	public class CompanyQueryModel : IQueryModel
	{
		/// <inheritdoc />
		public CompanyQueryModel(Guid id, string name, string nationalId)
		{
			Id = id;
			Name = name;
			NationalId = nationalId;
			Employees = new List<Employee>();
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		public string NationalId { get; set; }

		public virtual ICollection<Employee> Employees { get; set; }

		private CompanyQueryModel() { }
	}
}