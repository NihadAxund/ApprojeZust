using Microsoft.AspNetCore.SignalR;

namespace approje.Hubs
{
    public class ChatHub: Hub
    {
        private static Dictionary<string, string> UsersAndId = new Dictionary<string, string>();
        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }

        public async Task Join(string User_Name)
        {
            UsersAndId.Add(User_Name, Context.ConnectionId);
        }

        public async Task SendProgramData()
        {

        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
