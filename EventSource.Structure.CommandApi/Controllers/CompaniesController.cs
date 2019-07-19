using EventSource.Structure.Commands.Company;
using Framework.Application;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace EventSource.Structure.CommandApi.Controllers
{
	public class CompaniesController : ApiController
	{
		private readonly ICommandBus _bus;

		public CompaniesController(ICommandBus bus)
		{
			_bus = bus;
		}

		public async Task<IHttpActionResult> Post(RegisterCompanyCommand command)
		{
			await _bus.DispatchAsync(command);

			return Ok();
		}

		[HttpPost]
		[Route("api/companies/{id}/Employees")]
		public async Task<IHttpActionResult> HireEmployee(Guid id, HireEmployeeCommand command)
		{
			command.Validate();
			command.Id = id;

			await _bus.DispatchAsync(command);

			return Ok();
		}
	}
}