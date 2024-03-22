using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.SignalRHub
{
    public class AuthHub : Hub, IAuthHub
    {
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task SendToRoom(string roomId)
        {
            var data = new
            {
                verifiedEmail = true, 
                message = "You successfuly verified your email"
            };

            await Clients.Group(roomId).SendAsync("VerifiedEmail", data);
        }
    }
}