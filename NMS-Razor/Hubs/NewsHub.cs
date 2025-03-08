using Microsoft.AspNetCore.SignalR;

namespace NMS_Razor.Hubs
{
    public class NewsHub : Hub
    {
        public async Task SendNewsUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsUpdate", message);
        }
        
    }
}
