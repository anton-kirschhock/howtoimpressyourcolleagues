using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.Topics.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Topics
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IQueryable<Topic> Items { get; set; }

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task OnGet()
        {
            Items = await mediator.Send(new TopicQuery());
        }
    }
}
