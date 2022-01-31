using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Pages.Topics
{
    public class AddModel : PageModel
    {
        private readonly IMediator mediator;

        [Required]
        [BindProperty]
        public string DisplayName { get; set; }

        [Required]
        [BindProperty]
        public string Description { get; set; }

        public AddModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var topic = new Topic(DisplayName);
                topic.Description = Description;
                await mediator.Publish(new AddTopicCommand(topic));

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
