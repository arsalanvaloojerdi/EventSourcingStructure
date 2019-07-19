using System;
using System.Threading.Tasks;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.Repository
{
    public interface ICompanyRepository
    {
        Task<Company> GetAsync(Guid id);

        void Add(Company company);
    }
}