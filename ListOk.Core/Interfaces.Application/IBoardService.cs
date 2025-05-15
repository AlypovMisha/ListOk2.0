using ListOk.Application.DTOs;

namespace ListOk.Core.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardDto>> GetBoardsAsync(Guid userId);
        Task<BoardDto> CreateBoardAsync(Guid userId, string boardTitle, string boardDescription);
        Task<BoardDto> GetBoardByIdAsync(Guid id);
        Task<BoardDto> UpdateBoardAsync(Guid id, string boardTitle, string boardDescription);
        Task DeleteBoardAsync(Guid id);
    }
}
