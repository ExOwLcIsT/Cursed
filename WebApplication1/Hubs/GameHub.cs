using Microsoft.AspNetCore.SignalR;

namespace Cursed.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string x, string y)
        {
            Console.WriteLine(Context.User.Identity.Name);
            await Clients.User("user1").SendAsync("Res", x, y);
        }
    }
}