using ListOk.Core.Models;

namespace ListOk.Core.Interfaces.Infrastucture
{
    public interface IColumnRepository
    {
        Task<IEnumerable<Column>> GetColumnsByBoardIdAsync(Guid boardId);
        Task<Column> GetColumnByIdAsync(Guid id);
        Task AddColumnAsync(Column column);
        Task UpdateColumnAsync(Column column);
        Task DeleteColumnAsync(Guid id);
    }
}
