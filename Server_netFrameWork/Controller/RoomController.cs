using MySql.Data.MySqlClient.Memcached;
using Server_netFrameWork.Servers;
using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client = Server_netFrameWork.Servers.Client;

namespace Server_netFrameWork.Controller
{
    public class RoomController:BaseController
    {
        public RoomController() { 
            requestCode=RequestCode.Room;

        }
        public MainPack CreateRoom(Server server,Client client,MainPack pack)
        {
            pack.ReturnCode=server.CreateRoom(client, pack);
            return pack;
        }
        public MainPack FindRoom(Server server,Client client ,MainPack pack)
        {
            return server.FindRoom();
        }
    }
}
