using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;


namespace Server_netFrameWork.Controller
{
    /// <summary>
    /// 控制器是对请求做出函数响应的东西
    /// </summary>
    public abstract class BaseController
    {
        protected RequestCode requestCode=RequestCode.RequestNone;
        public RequestCode GetRequestCode{  get { return requestCode; } }
            
    }
}
