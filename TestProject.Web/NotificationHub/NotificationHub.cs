using Microsoft.AspNetCore.SignalR;
using TestProject.Domain.Entities;

namespace TestProject.Web
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(TextMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }


    
}
