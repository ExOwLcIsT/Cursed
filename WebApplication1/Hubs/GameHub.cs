using Cursed.Context;
using Cursed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Cursed.Hubs
{
    public class GameHub : Hub
    {
        readonly GameContext _gameContext;

        private readonly UserManager<User> _userManager;
        public GameHub(GameContext context, UserManager<User> userManager)
        {

            _gameContext = context;
            _userManager = userManager;
        }
        public async Task GetConnectionId(string s)
        {
            Console.WriteLine(Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnectionId", Context.ConnectionId);
        }
        public async Task SendMessage(string x, string y, string id)
        {
            await Clients.Client(id).SendAsync("ReceiveMessage", x, y);
        }
        public async Task SendToUser(string ui)
        {
            await Clients.Client(ui).SendAsync("ReceiveMessage", "yes");
        }
        public async Task CheckId(string myId, string checkId)
        {
            await Clients.Client(myId).SendAsync("CheckId", Clients.Client(checkId) != null);
        }
        //V20052402v#
        void CheckField(int x, int y)
        {

           
        }
    }
}