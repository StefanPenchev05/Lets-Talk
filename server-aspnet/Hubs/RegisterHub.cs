using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.SignalRHub
{
    public class RegisterHub : Hub
    {

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Caller.SendAsync("JoinedRoom", $"We have send you an Email. Please click on the link to Verify your account");
        }
    }
}