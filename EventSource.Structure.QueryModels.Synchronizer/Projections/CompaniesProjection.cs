using EventSource.Structure.DomainModel.Events.Company;
using EventSource.Structure.QueryModels.BaseRepository;
using EventSource.Structure.QueryModels.QueryModels.Company;
using Framework.QueryModel.Synchronizer;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EventSource.Structure.QueryModels.Synchronizer.Projections
{
	// TODO : Make Handlers Idempotent
	public class CompaniesProjection : IProjection,
		IProjector<CompanyRegistered>,
		IProjector<EmployeeHired>
	{
		private readonly IBaseRepository<CompanyQueryModel> _companyRepository;

		public CompaniesProjection(IBaseRepository<CompanyQueryModel> companyRepository)
		{
			_companyRepository = companyRepository;
		}

		/// <inheritdoc />
		public async Task ProjectAsync(CompanyRegistered @event)
		{
			if (await _companyRepository.IsExistsAsync(@event.AggregateId))
				return;

			var company = new CompanyQueryModel(@event.AggregateId, @event.Name, @event.NationalId);

			await _companyRepository.AddAsync(company);
		}

		/// <inheritdoc />
		public async Task ProjectAsync(EmployeeHired @event)
		{
			var company = await _companyRepository
				.AsQuery()
				.Include(q => q.Employees)
				.FirstOrDefaultAsync(q => q.Id == @event.AggregateId);

			if (company.Employees.Any(q => q.Id == @event.EmployeeId))
				return;

			company.Employees.Add(new Employee
			{
				Id = @event.EmployeeId,
				FirstName = @event.FirstName,
				LastName = @event.LastName,
				NationalCode = @event.NationalCode
			});

			await _companyRepository.SaveChangesAsync();
		}
	}
}