namespace Server.ViewModels
{
    public class Search
    {
        public string message { get; set; } = null;
        public string channel { get; set; } = null;
        public string FileName { get; set; } = null;
        public string userName { get; set; } = null;
        public int pageIndex { get; set; } = 0;
    }
}