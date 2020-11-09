using GloboTickets.Promotion.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ActQueries actQueries;

        public IndexModel(ActQueries actQueries)
        {
            this.actQueries = actQueries;
        }

        public List<Info.ActInfo> Acts { get; set; }

        public async Task OnGetAsync()
        {
            Acts = await actQueries.ListActs();
        }
    }
}
