using EventSource.Structure.QueryFacade.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace EventSource.Structure.QueryApi.Controllers
{
    public class CompaniesController : ApiController
    {
        private readonly ICompanyQueryFacade _companyQueryFacade;

        public CompaniesController(ICompanyQueryFacade companyQueryFacade)
        {
            _companyQueryFacade = companyQueryFacade;
        }

        public async Task<IHttpActionResult> GetAllAsync()
        {
            var companies = await _companyQueryFacade.GetAllAsync();

            return Ok(companies);
        }
    }
}