using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;

namespace ListOk.Application.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columnRepository;

        public ColumnService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }

        public async Task<IEnumerable<ColumnDto>> GetColumnsByBoardIdAsync(Guid boardId)
        {
            var columns = await _columnRepository.GetColumnsByBoardIdAsync(boardId);
            return columns.Select(c => new ColumnDto
            {
                Id = c.Id,
                Title = c.Title,
                BoardId = c.BoardId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Cards = c.Cards?.Select(card => new CardDto
                {
                    Id = card.Id,
                    Title = card.Title,
                    Description = card.Description,
                    ColumnId = card.ColumnId,
                    CreatedAt = card.CreatedAt,
                    UpdatedAt = card.UpdatedAt,
                    DateDeadline = card.DateDeadline,
                    Status = card.Status
                }).OrderBy(x => x.CreatedAt).ToList()
            }).OrderBy(x => x.CreatedAt);
        }

        public async Task<ColumnDto> CreateColumnAsync(string columnName, Guid boardId)
        {
            var column = new Column
            {
                Id = Guid.NewGuid(),
                Title = columnName,
                BoardId = boardId,
                CreatedAt = DateTime.UtcNow
            };
            await _columnRepository.AddColumnAsync(column);

            return new ColumnDto
            {
                Id = column.Id,
                Title = column.Title,
                BoardId = column.BoardId,
                CreatedAt = column.CreatedAt,
                UpdatedAt = column.UpdatedAt
            };
        }

        public async Task<ColumnDto> UpdateColumnAsync(Guid id, string columnName)
        {
            var column = await _columnRepository.GetColumnByIdAsync(id);
            if (column == null)
                throw new KeyNotFoundException("Column not found");

            column.Title = columnName;
            column.UpdatedAt = DateTime.UtcNow;
            await _columnRepository.UpdateColumnAsync(column);

            return new ColumnDto
            {
                Id = column.Id,
                Title = column.Title,
                BoardId = column.BoardId,
                CreatedAt = column.CreatedAt,
                UpdatedAt = column.UpdatedAt
            };
        }

        public async Task DeleteColumnAsync(Guid id)
        {
            var column = await _columnRepository.GetColumnByIdAsync(id);
            if (column == null)
                throw new KeyNotFoundException("Column not found");
            await _columnRepository.DeleteColumnAsync(id);
        }
    }
}
