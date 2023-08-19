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
            await Clients.User(ui).SendAsync("ReceiveMessage", "yes");
        }
        public async Task GetField()
        {

        }
        //V20052402v#
        void CheckField(int x, int y)
        {

            //int countToWin = 1;
            //int x1 = x - 1, y1 = y - 1;
            ////перевірка по діагоналі (ліво верх)

            //while (x1 >= 0 && y1 >= 0 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //    { }
            //    x1--;
            //    y1--;
            //}
            //x1 = x + 1;
            //y1 = y + 1;
            //while (x1 <= 9 && y1 <= 9 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //    { }

            //    x1++;
            //    y1++;
            //}
            ////перевірка по діагоналі (право верх)
            //countToWin = 1;
            //x1 = x + 1;
            //y1 = y - 1;
            //while (x1 <= 9 && y1 >= 0 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //    { }
            //    x1++;
            //    y1--;
            //}
            //x1 = x - 1;
            //y1 = y + 1;
            //while (x1 >= 0 && y1 <= 9 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //    { }
            //    x1--;
            //    y1++;
            //}
            ////перевірка по вертикалі 
            //countToWin = 1;
            //x1 = x;
            //y1 = y - 1;
            //while (y1 >= 0 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //        {}
            //    y1--;
            //}
            //y1 = y + 1;
            //while (y1 <= 9 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //        {}
            //    y1++;
            //}
            ////перевірка по горизонталі 
            //countToWin = 1;
            //x1 = x - 1;
            //y1 = y;
            //while (x1 >= 0 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //        {}
            //    x1--;
            //}
            //x1 = x + 1;
            //while (x1 <= 9 && gameField[x1, y1] == np)
            //{
            //    countToWin++;
            //    if (countToWin == 5)
            //        {}
            //    x1++;
            //}
            //countMove++;
        }
    }
}