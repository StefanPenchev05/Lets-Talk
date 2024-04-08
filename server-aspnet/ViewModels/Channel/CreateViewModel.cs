using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModels
{
    public class CreateViewModel
    {
        public Dictionary<int, string> Users { get; set; } // Dictionary of all usernames and their roles for creating the chat
        public string ChannelName { get; set; }
        public IFormFile ChannelImg { get; set; }
        
    }
}