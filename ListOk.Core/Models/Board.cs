namespace ListOk.Core.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public DateTime CreatedAt { get; set; }
    }
}
