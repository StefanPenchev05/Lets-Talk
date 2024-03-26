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
            await Clients.Caller.SendAsync("JoinedRoom", $"We have send you an Email. Please click on the link to Verify your account");
        }

        public async Task SendVerifiedEmail(string roomId, string token)
        {

            var data = new
            {
                verifiedEmail = true, 
                token,
                message = "You successfuly verified your email"
            };

            await Clients.Group(roomId).SendAsync("VerifiedEmail", data);
        }

        public async Task LeaveRoom(string roomId){
            await Groups.RemoveFromGroupAsync(Context.ConnectionId,roomId);
        }
    }
}