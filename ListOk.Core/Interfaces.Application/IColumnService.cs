using ListOk.Core.DTOs;

namespace ListOk.Core.Interfaces
{
    public interface IColumnService
    {
        Task<IEnumerable<ColumnDto>> GetColumnsByBoardIdAsync(Guid boardId);
        Task<ColumnDto> CreateColumnAsync(string columnName, Guid boardId);
        Task<ColumnDto> UpdateColumnAsync(Guid id, string columnName);
        Task DeleteColumnAsync(Guid id);
    }
}
