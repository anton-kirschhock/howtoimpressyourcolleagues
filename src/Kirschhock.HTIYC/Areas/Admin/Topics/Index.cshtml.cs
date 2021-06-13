using System.Collections.Generic;

using Kirschhock.HTIYC.Domain;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kirschhock.HTIYC.Areas.Admin.Topics
{
    public class IndexModel : PageModel
    {
        public List<Topic> Items { get; set; }

        public void OnGet()
        {
        }
    }
}
