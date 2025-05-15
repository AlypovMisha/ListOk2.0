using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;
using ListOk.Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;

namespace ListOk.Infrastructure.Repositories
{
    public class CardRepository(ApplicationDbContext _context) : ICardRepository
    {
        public async Task<IEnumerable<Card>> GetCardsByColumnIdAsync(Guid columnId)
        {
            return await _context.Cards
                .Where(c => c.ColumnId == columnId)
                .ToListAsync();
        }

        public async Task<Card> GetCardByIdAsync(Guid id)
        {
            return await _context.Cards.FindAsync(id);
        }

        public async Task AddCardAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCardAsync(Card card)
        {
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCardAsync(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }
        }
    }
}
