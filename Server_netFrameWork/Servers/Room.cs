using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
namespace Server_netFrameWork.Servers
{
    public class Room
    {
        private RoomPack roomInfo;

        private List<Client> clientList=new List<Client>();
        public Room(Client client,RoomPack pack)
        {
            roomInfo = pack;
            clientList.Add(client);

        }

        public RoomPack GetRoomInfo
        {
            get
            {
                return roomInfo;
            }
        }

    }
}
