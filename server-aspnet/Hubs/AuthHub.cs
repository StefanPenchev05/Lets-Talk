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

        public async Task AwaitForEmailVeification(string user, string message)
        {
            await Clients.User(user).SendAsync("AwaitForEmailVerification", message);
        }
    }
}