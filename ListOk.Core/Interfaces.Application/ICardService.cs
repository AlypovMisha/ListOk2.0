using ListOk.Core.DTOs;
using ListOk.Core.Models;

namespace ListOk.Core.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDto>> GetCardsByColumnIdAsync(Guid columnId);
        Task<CardDto> GetCardByIdAsync(Guid id);
        Task<CardDto> CreateCardAsync(string title, string description, Guid columnId);
        Task<CardDto> UpdateCardAsync(Guid id, string title, string description, CardStatus status, DateTime? deadlineDate);
        Task MoveCardAsync(Guid id, Guid destinationColumnId);
        Task DeleteCardAsync(Guid id);
        Task<CardDto> UpdateCardStatusAsync(Guid id, CardStatus newStatus);
    }
}
