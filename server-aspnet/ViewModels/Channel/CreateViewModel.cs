namespace Server.ViewModels
{
    public class CreateViewModel
    {
        public Dictionary<string, string> Users { get; set; } // Dictionary of all usernames and their roles for creating the chat
        public string ChannelName { get; set; }
        public IFormFile ChannelImg { get; set; }
        
    }
}