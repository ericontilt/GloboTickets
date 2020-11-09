using GloboTickets.Promotion.DataAccess.Entities;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess
{
    public interface IContentQueries
    {
        Task<Content> GetContent(string hash);
    }
}
