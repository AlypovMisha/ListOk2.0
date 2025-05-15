namespace ListOk.Core.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ColumnId { get; set; }
        public Column Column { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DateDeadline { get; set; }
        public CardStatus Status { get; set; } = CardStatus.todo;
        public DateTime? DateCompletion { get; set; }
    }
}
