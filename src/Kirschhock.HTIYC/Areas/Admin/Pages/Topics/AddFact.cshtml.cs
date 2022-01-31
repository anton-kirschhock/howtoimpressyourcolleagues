using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain.DomainEvents.Facts;
using Kirschhock.HTIYC.Infrastructure.Topics.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Pages.Topics
{
    public class AddFactModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty(SupportsGet = true)]
        public Guid TopicId { get; set; }

        [Required]
        [BindProperty]
        public string Name { get; set; }

        [Required]
        [BindProperty]
        public string Title { get; set; }

        [Required]
        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string ReadMoreLink { get; set; }

        public AddFactModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet()
        {
            var topic = await mediator.Send(new TopicByIdQuery(TopicId));
            if (topic == null)
                return RedirectToPage("index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var topic = await mediator.Send(new TopicByIdQuery(TopicId));
            if (topic == null)
                return RedirectToPage("index");
            if (ModelState.IsValid)
            {
                var fact = await topic.AddFactAsync(Name, Title);
                fact.ReadMoreLink = ReadMoreLink;
                fact.Description = Description;

                await mediator.Publish(new AddFactCommand(topic, fact));

                await Task.Delay(1000);
                return RedirectToPage("Facts", new { TopicId });
            }
            return Page();

        }
    }
}
