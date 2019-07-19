using Framework.Application.Contracts;
using Framework.Core.Exceptions.Exceptions.CommandHandler;

namespace EventSource.Structure.Commands.Company
{
	public class RegisterCompanyCommand : ICommandBase
	{
		public string Name { get; set; }

		public string NationalId { get; set; }

		/// <inheritdoc />
		public void Validate()
		{
			// TODO : Using Common Validator From Application
			if (string.IsNullOrEmpty(Name))
				throw new CommandValidationException();
		}
	}
}