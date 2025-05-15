namespace ListOk.Presentation.Requests
{
    public class UpdateBoardRequest
    {

        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
