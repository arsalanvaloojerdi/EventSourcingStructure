using Framework.Application.Contracts;
using System;

namespace EventSource.Structure.Commands.Company
{
	public class HireEmployeeCommand : ICommandBase
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string NationalCode { get; set; }

		/// <inheritdoc />
		public void Validate() { }
	}
}