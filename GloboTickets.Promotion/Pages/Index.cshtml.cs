using GloboTickets.Promotion.DataAccess;
using GloboTickets.Promotion.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ShowQueries showQueries;

        public IndexModel(ShowQueries showQueries)
        {
            this.showQueries = showQueries;
        }

        public List<Models.ShowModel> Shows { get; set; }

        public async Task OnGetAsync()
        {
            Shows = await showQueries.ListShows();
        }
    }
}
