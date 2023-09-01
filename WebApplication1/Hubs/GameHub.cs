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
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnectionId", Context.ConnectionId);
        }
        public async Task SendMessage(string x, string y, string id)
        {
            await Clients.Client(id).SendAsync("ReceiveStep", x, y);
        }
        public async Task CheckId(string myId, string checkId, string pfp, string nick)
        {
            if (Clients.Client(checkId) != null && myId != checkId)
            {
                await Clients.Client(myId).SendAsync("CheckId");
                await Clients.Client(checkId).SendAsync("ReceiveEnemy", pfp, nick, true, myId);
            }
        }
        public async Task GetMove(string myId, string enemyId)
        {
            Random a = new Random();
            int m = a.Next(1, 3);
            await Clients.Client(myId).SendAsync("ReceiveMove", m);
            await Clients.Client(enemyId).SendAsync("ReceiveMove", 3 - m);
        }
        public async Task SendEnemy(string myId, string checkId, string pfp, string nick)
        {
            await Clients.Client(checkId).SendAsync("ReceiveEnemy", pfp, nick, false, myId);
        }
    }
}