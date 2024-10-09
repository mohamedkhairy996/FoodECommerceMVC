using Microsoft.AspNetCore.SignalR;

namespace ModelClasses
{
    public class ChatHub : Hub
    {
        private readonly string Admin= "b696b434-b763-431f-82d2-3c5b97450dba";

        public async Task SendMessageToAdmin(string message ,string id , string name)
        {
            await Clients.User(Admin).SendAsync("sendAdminMsg", message ,id , name);
        }

        public async Task SendMessageToUser(string to, string message)
        {
            await Clients.User(to).SendAsync("sendUserMsg", message);
        }
    }
}
