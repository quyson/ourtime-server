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
            await Clients.Client(connectionId).SendAsync("ReceiveOffer", Context.ConnectionId, sdpOffer, username);
            Console.WriteLine($"sent {username}'s SDPOffer to {connectionId}");
        }

        public async Task Answer(string connectionId, string sdpAnswer, string username)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveAnswer", Context.ConnectionId, sdpAnswer, username);
            Console.WriteLine($"sent {username}'s SDPAnswer back to {connectionId}");
        }

        public async Task SendIceCandidate(string connectionId, string username, string candidate)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveIceCandidate", candidate);
            Console.WriteLine($"{username} sent Ice Candidate {candidate} to {connectionId}!");
        }
        public async Task Decline(string peerId)
        {
            await Clients.Client(peerId).SendAsync("Declined");
            Console.WriteLine($"Declined Call");
        }
        public async Task Disconnect(string peerId)
        {
            await Clients.Client(peerId).SendAsync("Disconnected");
            Console.WriteLine($"Disconnected!");
        }
    }
}
