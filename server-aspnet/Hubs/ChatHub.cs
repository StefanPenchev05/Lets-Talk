using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.SignalRHub
{
    public class ChatHub : Hub
    {

        private readonly UserManagerDB _context;

        public ChatHub(UserManagerDB context)
        {
            _context = context;
        }

        public async Task JoinRoom(string roomId, string username)
        {
            var userRole = await _context.Users
                .Include(u => u.UserChannels)
                .ThenInclude(uc => uc.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (userRole != null)
            {
                var userChannel = userRole.UserChannels.FirstOrDefault(uc => uc.ChannelId == int.Parse(roomId));
                if (userChannel != null)
                {
                    var roleType = userChannel.Role.RoleType;
                    var channelName = userChannel.Channel.Name;
                    var avatarURL = userChannel.Channel.ImageURL;
                    var messages = await _context.Messages
                    .Include(m => m.User)
                    .OrderByDescending(m => m.Timestamp)
                    .Take(30)
                    .Select(m => new
                    {
                        m.Content,
                        m.Timestamp,
                        m.User.FirstName,
                        m.User.LastName,
                        m.User.UserName,
                        AvatarUrl = m.User.ProfilePictureURL
                    })
                    .ToListAsync();

                    await Clients.Caller.SendAsync("JoinedRoom", messages, channelName, avatarURL);
                }
            }
        }
    }
}

