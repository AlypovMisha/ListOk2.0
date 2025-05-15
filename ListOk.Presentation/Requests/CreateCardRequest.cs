namespace ListOk.Presentation.Requests
{
    public class CreateCardRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public Guid ColumnId { get; set; }
    }
}
