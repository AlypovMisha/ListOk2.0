using ListOk.Application.DTOs;
using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;

namespace ListOk.Application.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<IEnumerable<BoardDto>> GetBoardsAsync(Guid userId)
        {
            var boards = await _boardRepository.GetBoardsAsync(userId);
            return boards.Select(MapToDto).OrderBy(x => x.CreatedAt);
        }

        public async Task<BoardDto> GetBoardByIdAsync(Guid id)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            return board != null ? MapToDto(board) : null;
        }

        public async Task<BoardDto> CreateBoardAsync(Guid userId, string boardTitle, string boardDescription)
        {
            var board = new Board
            {
                Id = Guid.NewGuid(),
                Title = boardTitle,
                Description = boardDescription,
                UserId = userId, // Используем переданный userId
                CreatedAt = DateTime.UtcNow
            };
            await _boardRepository.AddBoardAsync(board);
            return MapToDto(board);
        }

        public async Task<BoardDto> UpdateBoardAsync(Guid id, string boardTitle, string boardDescription)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            if (board == null) throw new KeyNotFoundException("Board not found");

            board.Title = boardTitle;
            board.Description = boardDescription;
            board.UpdatedAt = DateTime.UtcNow;
            await _boardRepository.UpdateBoardAsync(board);
            return MapToDto(board);
        }

        public async Task DeleteBoardAsync(Guid id)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            if (board == null) throw new KeyNotFoundException("Board not found");
            await _boardRepository.DeleteBoardAsync(id);
        }

        private BoardDto MapToDto(Board board)
        {
            return new BoardDto
            {
                Id = board.Id,
                Title = board.Title,
                Description = board.Description,
                CreatedAt = board.CreatedAt,
                UpdatedAt = board.UpdatedAt,
                Columns = board.Columns?.Select(c => new ColumnDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    BoardId = board.Id,
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
                }).OrderBy(x => x.CreatedAt).ToList()
            };
        }
    }
}