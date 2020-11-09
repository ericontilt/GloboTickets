using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess
{
    public interface IContentCommands
    {
        Task<string> SaveContent(byte[] binary, string contentType);
    }
}
