using ListOk.Core.Models;

namespace ListOk.Presentation.Requests
{
    public class UpdateCardRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public CardStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
