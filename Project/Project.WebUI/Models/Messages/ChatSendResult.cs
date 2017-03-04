namespace Project.WebUI.Models
{
    public class ChatSendProfile
    {
        public long FromId { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
	public class ChatSendResult
	{
       public ChatSendProfile From { get; set; }
        public string Message { get; set; }
        public long MessageId { get; set; }
       
	}
}