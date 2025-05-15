using ListOk.Core.Models;

namespace ListOk.Core.Interfaces.Infrastucture
{
    public interface IBoardRepository
    {
        Task<IEnumerable<Board>> GetBoardsAsync(Guid userId);
        Task<Board> GetBoardByIdAsync(Guid id);
        Task AddBoardAsync(Board board);
        Task UpdateBoardAsync(Board board);
        Task DeleteBoardAsync(Guid id);
        Task<bool> BoardExistsAsync(Guid id);
    }
}
