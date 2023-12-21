using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;


namespace ourTime_server
{
    public class WebRTC : Hub
    {
        public class CallInformation
        {
            public string UserToCall { get; set; }
            public string SignalData { get; set; }
            public string From { get; set; }

        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"New Connection {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public async Task CallUser(CallInformation callInformation)
        {
            await Clients.Client(callInformation.UserToCall).SendAsync("IncomingCall", Context.ConnectionId, callInformation.SignalData);
            Console.WriteLine($"calling {callInformation.UserToCall}");
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
