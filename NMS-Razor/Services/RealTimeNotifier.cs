using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using NMS_Razor.Hubs;

namespace NMS_Razor.Services
{
    public class RealTimeNotifier : IRealTimeNotifier
    {
        private readonly IHubContext<NewsHub> _hubContext;
        public RealTimeNotifier(IHubContext<NewsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNewsUpdate", message);
        }
    }
}
