namespace TicketApp.Models
{
    public class ErrorViewModel
    {
        public class ErrorViewModel
        {
            public string? RequestId { get; set; }
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}