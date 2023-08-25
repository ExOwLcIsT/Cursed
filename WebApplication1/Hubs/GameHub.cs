using Cursed.Context;
using Cursed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Cursed.Hubs
{
    public class GameHub : Hub
    {
        public async Task GetConnectionId(string s)
        {
            Console.WriteLine(Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnectionId", Context.ConnectionId);
        }
        public async Task SendMessage(string x, string y, string id)
        {
            await Clients.Client(id).SendAsync("ReceiveStep", x, y);
        }
        public async Task CheckId(string myId, string checkId)
        {
            await Clients.Client(myId).SendAsync("CheckId", Clients.Client(checkId) != null);
        }
    }
}