using GloboTickets.Promotion.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess
{

    public class ContentQueries : IContentQueries
    {
        private readonly PromotionContext repository;

        public ContentQueries(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<Content> GetContent(string hash)
        {
            return await repository.Content
                .Where(c => c.Hash == hash)
                .SingleOrDefaultAsync();
        }
    }
}
