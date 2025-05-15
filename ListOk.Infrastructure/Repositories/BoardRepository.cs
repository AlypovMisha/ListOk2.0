using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;
using ListOk.Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;

namespace ListOk.Infrastructure.Repositories
{
    public class BoardRepository(ApplicationDbContext _context) : IBoardRepository
    {
        public async Task<IEnumerable<Board>> GetBoardsAsync(Guid userId)
        {
            return await _context.Boards.Where(x => x.UserId == userId).Include(b => b.Columns).ThenInclude(c => c.Cards).ToListAsync();
        }

        public async Task<Board> GetBoardByIdAsync(Guid id)
        {
            return await _context.Boards.Include(b => b.Columns).ThenInclude(c => c.Cards).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBoardAsync(Board board)
        {
            _context.Boards.Update(board);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoardAsync(Guid id)
        {
            var board = await _context.Boards.FirstOrDefaultAsync(b => b.Id == id);
            if (board != null)
            {
                _context.Boards.Remove(board);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BoardExistsAsync(Guid id)
        {
            return await _context.Boards.AnyAsync(e => e.Id == id);
        }
    }
}
