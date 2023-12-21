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

        public async Task CallUser(string userToCall, string signalData, string from)
        {
            await Clients.Client(userToCall).SendAsync("IncomingCall", from, signalData);
            Console.WriteLine($"calling {userToCall}");
        }

        public async Task AcceptCaller(string data, string caller)
        {
            await Clients.Client(caller).SendAsync("CallAccepted", data);
            Console.WriteLine($"Accepted Call");
        }

        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("NewMessage", username, message);
            Console.WriteLine($"sent {message}");
        }
       
    }
}
