using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.SignalRHub
{
    public class RegisterHub : Hub, IRegisterHub
    {
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Caller.SendAsync("JoinedRoom", $"You have joined room {roomId}");
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

        public async Task LeaveRoom(string roomId){
            await Groups.RemoveFromGroupAsync(Context.ConnectionId,roomId);
        }
    }
}