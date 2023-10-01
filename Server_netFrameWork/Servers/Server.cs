using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Server_netFrameWork.Controller;
using SocketGameProtocol;

namespace Server_netFrameWork.Servers
{
    // 1.接受应答
    public class Server
    {
        //拥有控制器
        private ControllerManager controllerManager;
        // 定义一个私有的类成员变量clientList，用于存储客户端列表
        private List<Client> clientList = new List<Client>();

        // 定义一个私有的类成员变量socket，用于接收客户端的连接
        private Socket socket;

       
        // 构造函数，接收一个端口号作为参数，用于创建Socket对象并绑定到指定的端口
        public Server(int port)
        {
            controllerManager = new ControllerManager(this);

            // 创建一个Socket对象，并指定其地址家族为InterNetwork，类型为Stream，协议类型为Tcp
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 使用socket的Bind方法绑定到指定的IP地址和端口号
            socket.Bind(new IPEndPoint(IPAddress.Any, port));

            // 使用socket的Listen方法开始监听连接请求
            socket.Listen(0);

            // 调用StartAccpt方法开始接收连接
            StartAccept();
        }

        // 定义一个方法StartAccpt，用于开始接收连接
        void StartAccept()
        {
            // 使用socket的BeginAccept方法异步接收连接请求
            socket.BeginAccept(AcceptCallback, null);
        }

        // 定义一个回调方法AcceptCallback，用于处理接收到的连接请求
        void AcceptCallback(IAsyncResult iar)
        {
            try
            {
                // 使用socket的EndAccept方法接收并返回接收到的连接
                Socket client = socket.EndAccept(iar);

                // 将接收到的连接添加到clientList中
                clientList.Add(new Client(client, this));
                StartAccept();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                  
        }

      

        public void HandleRequest(MainPack pack,Client client)
        {
            controllerManager.HandleRequest(pack, client);
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="client"></param>
        public void   RemoveClient(Client client)
        {
            clientList.Remove(client);  
        }
    }
}