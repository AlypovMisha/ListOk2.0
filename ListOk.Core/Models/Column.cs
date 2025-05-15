namespace ListOk.Core.Models
{
    public class Column
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid BoardId { get; set; }
        public Board Board { get; set; }
        public int Order { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
