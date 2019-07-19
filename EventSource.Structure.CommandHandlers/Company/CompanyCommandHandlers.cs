using EventSource.Structure.Commands.Company;
using EventSource.Structure.DomainModel.Aggregates.Companies.Entities;
using EventSource.Structure.DomainModel.Aggregates.Companies.Repository;
using EventSource.Structure.DomainModel.Aggregates.Companies.ValueObjects;
using Framework.Application;
using Framework.Core.Exceptions.Exceptions.CommandHandler;
using Framework.Persistence.Core;
using System.Threading.Tasks;

namespace EventSource.Structure.CommandHandlers.Company
{
	using DomainModel.Aggregates.Companies;

	public class CompanyCommandHandlers
		: ICommandHandlerAsync<RegisterCompanyCommand>,
		  ICommandHandlerAsync<HireEmployeeCommand>
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CompanyCommandHandlers(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
		{
			_companyRepository = companyRepository;
			_unitOfWork = unitOfWork;
		}

		/// <inheritdoc />
		public async Task HandleAsync(RegisterCompanyCommand command)
		{
			var company = new Company(new CompanyName(command.Name), new NationalId(command.NationalId));

			_companyRepository.Add(company);

			await _unitOfWork.CommitAsync();
		}

		/// <inheritdoc />
		public async Task HandleAsync(HireEmployeeCommand command)
		{
			var company = await _companyRepository.GetAsync(command.Id);
			if (company == null)
				throw new CommandHandlerNotFoundException("Company Not Found");

			company.HireEmployee(new Employee(new FullName(command.FirstName, command.LastName), new NationalCode(command.NationalCode)));

			await _unitOfWork.CommitAsync();
		}
	}
}