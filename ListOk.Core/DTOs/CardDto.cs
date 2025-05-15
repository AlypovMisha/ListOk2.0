using ListOk.Core.Models;

namespace ListOk.Core.DTOs
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ColumnId { get; set; }
        public CardStatus Status { get; set; }
        public DateTime? DateDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
