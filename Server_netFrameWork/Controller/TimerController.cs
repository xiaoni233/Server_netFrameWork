using Server_netFrameWork.Servers;
using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_netFrameWork.Controller
{
    public class TimerController:BaseController
    {
        public TimerController() {
            requestCode = RequestCode.RequestTimer;
        }
        public MainPack ActionTimer(Server server, Client client, MainPack pack)
        {
            pack.ReturnCode = ReturnCode.Succeed;
            return pack;
        }
    }
}
