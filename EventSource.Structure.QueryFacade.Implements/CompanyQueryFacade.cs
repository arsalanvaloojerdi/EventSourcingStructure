using EventSource.Structure.QueryFacade.Interfaces;
using EventSource.Structure.QueryModels.BaseRepository;
using EventSource.Structure.QueryModels.QueryModels.Company;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace EventSource.Structure.QueryFacade.Implements
{
	public class CompanyQueryFacade : ICompanyQueryFacade
	{
		private readonly IBaseRepository<CompanyQueryModel> _companyRepository;

		public CompanyQueryFacade(IBaseRepository<CompanyQueryModel> companyRepository)
		{
			_companyRepository = companyRepository;
		}

		public async Task<List<CompanyQueryModel>> GetAllAsync()
		{
			var companies = await _companyRepository
				.AsQuery()
				.Include(q => q.Employees)
				.ToListAsync();

			return companies;
		}
	}
}