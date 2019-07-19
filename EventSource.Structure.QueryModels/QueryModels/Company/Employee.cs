using System;

namespace EventSource.Structure.QueryModels.QueryModels.Company
{
	public class Employee
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string NationalCode { get; set; }
	}
}