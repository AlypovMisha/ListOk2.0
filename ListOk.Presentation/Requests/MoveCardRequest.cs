namespace ListOk.Presentation.Requests
{
    public class MoveCardRequest
    {
        public Guid SourceColumnId { get; set; }
        public Guid DestinationColumnId { get; set; }
    }
}
