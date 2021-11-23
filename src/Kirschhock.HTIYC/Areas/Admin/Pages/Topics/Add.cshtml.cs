using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.Factories;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Pages.Topics
{
    public class AddModel : PageModel
    {
        private readonly IMediator mediator;
        private readonly IAggregateFactory<Topic> topicFactory;

        [Required]
        [BindProperty]
        public string DisplayName { get; set; }

        [Required]
        [BindProperty]
        public string Description { get; set; }

        public AddModel(IMediator mediator, IAggregateFactory<Topic> topicFactory)
        {
            this.mediator = mediator;
            this.topicFactory = topicFactory;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var topic = await topicFactory.CreateAsync();
                topic.DisplayName = DisplayName;
                topic.Description = Description;
                await Task.Delay(5000);

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
