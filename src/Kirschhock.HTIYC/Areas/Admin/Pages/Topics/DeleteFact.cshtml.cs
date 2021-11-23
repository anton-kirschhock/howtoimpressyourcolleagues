using System;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Infrastructure.Facts.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Pages.Topics
{
    public class DeleteFactModel : PageModel
    {
        private readonly IMediator mediator;


        [BindProperty(SupportsGet = true)]
        public Guid TopicId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public DeleteFactModel(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> OnGet()
        {
            var resp = await mediator.Send(new DeleteFactCommand(TopicId, Id));

            return RedirectToPage("Index");
        }
    }
}
