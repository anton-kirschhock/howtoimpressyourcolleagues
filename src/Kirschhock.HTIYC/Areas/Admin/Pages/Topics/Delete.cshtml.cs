using System;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Infrastructure.Topics.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Pages.Topics
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty(SupportsGet = true)]
        public Guid TopicId { get; set; }

        public DeleteModel(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> OnGet()
        {
            var resp = await mediator.Send(new DeleteTopicCommand(TopicId));

            return RedirectToPage("Index");
        }
    }
}
