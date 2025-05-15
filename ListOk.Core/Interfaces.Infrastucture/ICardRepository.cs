using ListOk.Core.Models;

namespace ListOk.Core.Interfaces.Infrastucture
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetCardsByColumnIdAsync(Guid columnId);
        Task<Card> GetCardByIdAsync(Guid id);
        Task AddCardAsync(Card card);
        Task UpdateCardAsync(Card card);
        Task DeleteCardAsync(Guid id);
    }
}
