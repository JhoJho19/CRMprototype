using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ClientManagementAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public static IHubContext<NotificationHub> HubContext;

        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            HubContext = hubContext;
        }

        public static async Task NotifyClients()
        {
            await HubContext.Clients.All.SendAsync("UpdateClients");
        }
    }

}

