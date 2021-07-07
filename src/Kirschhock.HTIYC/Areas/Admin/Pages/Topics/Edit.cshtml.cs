using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Infrastructure.Topics.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Pages.Topics
{
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [Required]
        [BindProperty]
        public string DisplayName { get; set; }

        [Required]
        [BindProperty]
        public string Description { get; set; }

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet()
        {
            var topic = await mediator.Send(new TopicByIdQuery(Id));
            if (topic == null)
                return RedirectToPage("Index");

            DisplayName = topic.DisplayName;
            Description = topic.Description;

            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            {
                var topic = await mediator.Send(new TopicByIdQuery(Id));
                if (topic == null)
                    return RedirectToPage("Index");

                if (ModelState.IsValid)
                {
                    topic.Description = Description;
                    topic.DisplayName = DisplayName;

                    return RedirectToPage("Index");
                }

                return Page();
            }
        }
    }

}