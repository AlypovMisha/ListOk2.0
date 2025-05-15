using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;
using ListOk.Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;

namespace ListOk.Infrastructure.Repositories
{
    public class ColumnRepository(ApplicationDbContext _context) : IColumnRepository
    {
        public async Task<IEnumerable<Column>> GetColumnsByBoardIdAsync(Guid boardId)
        {
            return await _context.Columns
                .Include(c => c.Cards)
                .Where(c => c.BoardId == boardId)
                .ToListAsync();
        }

        public async Task<Column> GetColumnByIdAsync(Guid id)
        {
            return await _context.Columns
                .Include(c => c.Cards)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddColumnAsync(Column column)
        {
            _context.Columns.Add(column);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateColumnAsync(Column column)
        {
            _context.Columns.Update(column);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteColumnAsync(Guid id)
        {
            var column = await _context.Columns.FindAsync(id);
            if (column != null)
            {
                _context.Columns.Remove(column);
                await _context.SaveChangesAsync();
            }
        }
    }
}
