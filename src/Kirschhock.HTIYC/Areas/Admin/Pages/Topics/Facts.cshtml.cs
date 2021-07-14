using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.Topics.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Pages.Topics
{
    public class FactsModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty(SupportsGet = true)]
        public Guid TopicId { get; set; }

        public IEnumerable<Fact> Facts { get; set; }

        public FactsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet()
        {
            var topic = await mediator.Send(new TopicByIdQuery(TopicId));
            if (topic == null)
                return RedirectToPage("index");

            Facts = topic.Facts;

            return Page();
        }
    }
}
