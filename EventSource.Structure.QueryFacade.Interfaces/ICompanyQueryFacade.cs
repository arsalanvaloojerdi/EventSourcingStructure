using EventSource.Structure.QueryModels.QueryModels.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSource.Structure.QueryFacade.Interfaces
{
    public interface ICompanyQueryFacade
    {
        Task<List<CompanyQueryModel>> GetAllAsync();
    }
}