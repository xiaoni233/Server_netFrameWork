
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
using Server_netFrameWork.Servers;
namespace Server_netFrameWork.Controller
{
    public class UserController:BaseController
    {
        public UserController()
        {
            requestCode=RequestCode.User;

        }
        public MainPack Logon(Server server, Client client, MainPack pack)
        {
            if(client.Logon( pack)) 
            {
                pack.ReturnCode = ReturnCode.Succeed;

            }
            else
            {
                pack.ReturnCode = ReturnCode.Fail;
            }
            return pack;
        }

        //public MainPack Login(Server server, Client client, MainPack pack)
        //{

        //}
    }
   
}
