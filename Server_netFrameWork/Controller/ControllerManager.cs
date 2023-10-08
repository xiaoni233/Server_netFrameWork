using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server_netFrameWork.Servers;
using Google.Protobuf.WellKnownTypes;
using System.Reflection;

namespace Server_netFrameWork.Controller
{
    /// <summary>
    /// 处理请求
    /// </summary>
    public class ControllerManager
    {
        private Dictionary<RequestCode,BaseController>controlDict=new Dictionary<RequestCode,BaseController>();
        public ControllerManager(Server server) { 
            this.server = server;
            UserController userController = new UserController();
            controlDict.Add(userController.GetRequestCode, userController);

            TimerController tc = new TimerController();
            controlDict.Add(tc.GetRequestCode, tc);
        }

        private Server server;

        /// <summary>
        /// 处理请求
        /// </summary>
        public void HandleRequest(MainPack pack,Client client)
        {
            try
            {
                if (controlDict.TryGetValue(pack.RequestCode, out BaseController controller))
                {
                    //通过反射调用对应controller方法
                    string metname = pack.Actioncode.ToString();
                    MethodInfo method = controller.GetType().GetMethod(metname);
                    Console.WriteLine("method" + metname);

                    if (method == null)
                    {
                        Console.WriteLine("没有找到对应的Action方法处理");
                        return;
                    }
                    Console.WriteLine("服务器控制器响应对应函数" + method.Name);
                    object[] obj = new object[] { server, client, pack };
                    //调用反射到的方法  public MainPack Login(Server server,Client client,MainPack pack)
                    
                    object ret = method.Invoke(controller, obj);
                    
                    //服务器响应返回
                    if (ret != null)
                    {
                        client.Send(ret as MainPack);
                    }

                }
                else
                {
                    Console.WriteLine("没有找到对应的Controller处理");
                }
            }catch (Exception ex)
            {
                Console.WriteLine("控制器处理请求异常");
                Console.WriteLine(ex.Message);
            }
         
        }
    }
}
