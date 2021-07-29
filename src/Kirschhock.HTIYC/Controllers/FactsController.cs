using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Infrastructure.Facts.Query;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Kirschhock.HTIYC.Controllers
{
    [Route("api/facts")]
    public class FactsController : Controller
    {
        private readonly IMediator mediator;

        public FactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomFact([FromQuery] string topic = null)
        {
            var response = await mediator.Send(new RandomFactQuery(topic));

            if (response.Any())
                return Ok(response.FirstOrDefault());

            else
                return BadRequest();
        }
    }
}
