using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;


namespace ourTime_server
{
    public class WebRTC : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"New Connection {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public async Task Offer(string connectionId, string sdpOffer, string username)
        {
            Console.WriteLine(sdpOffer);
            Console.WriteLine(username);

            await Clients.Client(connectionId).SendAsync("ReceiveOffer", Context.ConnectionId, sdpOffer, username);
            Console.WriteLine($"sent {username}'s SDPOffer to {connectionId}");
        }

        public async Task Answer(string connectionId, string sdpAnswer, string username)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveAnswer", Context.ConnectionId, sdpAnswer, username);
            Console.WriteLine($"sent {username}'s SDPAnswer back to {connectionId}");
        }
    }
}
