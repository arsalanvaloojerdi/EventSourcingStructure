using EventSource.Structure.DomainModel.Aggregates.Companies;
using EventSource.Structure.DomainModel.Aggregates.Companies.Repository;
using Framework.Persistence.EventStore.Repository;
using System;
using System.Threading.Tasks;

namespace EventSource.Structure.Persistence.EventStore.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IBaseRepository<Company> _companyRepository;

        public CompanyRepository(IBaseRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /// <inheritdoc />
        public async Task<Company> GetAsync(Guid id)
        {
            return await _companyRepository.GetAsync(id);
        }

        /// <inheritdoc />
        public void Add(Company company)
        {
            _companyRepository.Add(company);
        }
    }
}