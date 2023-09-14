using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    internal class GameController : BaseController
    {
        public GameController()
        {
            requestCode = RequestCode.Game;
        }

        public string StartGame(string data, Client client, Server server)
        {
            if (client.IsHouseOwner())
            {
                Room room = client.Room;
                Console.WriteLine("房间" + room.ToString() + "开始游戏");
                room.BroadcastMessage(client, ActionCode.StartGame, ((int)ReturnCode.Success).ToString());
                room.StartTimer();
                return ((int)ReturnCode.Success).ToString();
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }

        public string Move(string data, Client client, Server server)
        {
            Console.WriteLine("客户端" + client.ToString() + "移动");
            Room room = client.Room;
            if (room != null)
                room.BroadcastMessage(client, ActionCode.Move, data);
            return null;
        }

        public string Shoot(string data, Client client, Server server)
        {
            Console.WriteLine("客户端" + client.ToString() + "射击");
            Room room = client.Room;
            if (room != null)
                room.BroadcastMessage(client, ActionCode.Shoot, data);
            return null;
        }

        public string Attack(string data, Client client, Server server)
        {
            Console.WriteLine("客户端" + client.ToString() + "受到伤害");
            int damage = int.Parse(data);
            Room room = client.Room;
            if (room == null) return null;
            room.TakeDamage(damage, client);
            return null;
        }

        public string QuitBattle(string data, Client client, Server server)
        {
            Room room = client.Room;

            if (room != null)
            {
                room.BroadcastMessage(null, ActionCode.QuitBattle, "r");
                room.Close();
            }
            return null;
        }
    }
}