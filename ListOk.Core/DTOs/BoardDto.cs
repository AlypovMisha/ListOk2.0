using ListOk.Core.DTOs;

namespace ListOk.Application.DTOs
{
    public class BoardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ColumnDto> Columns { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
