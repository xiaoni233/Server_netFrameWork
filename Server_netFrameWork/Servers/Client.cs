using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Server_netFrameWork.Tool;
using Server_netFrameWork.Dao;
using SocketGameProtocol;
using MySql.Data.MySqlClient;

namespace Server_netFrameWork.Servers
{
    public class Client
    {
        private Socket socket;
        private Message message;
        private UserData userData;
        private Server server;


        private MySqlConnection mysqlCon;
        private string constr = "database=sys;data source=localhost;user=root;password=123456;pooling=false;charset=utf8mb4;port=3306";
        public UserData GetUserData
        {
            get { return userData; }
        }
        public Client(Socket socket, Server server)
        {
            try
            {
                mysqlCon = new MySqlConnection(constr);
                mysqlCon.Open();
                Console.WriteLine("数据库连接成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库连接失败");
                Console.WriteLine(ex.Message);
            }
            this.socket = socket;
            userData = new UserData();
            message = new Message();
            this.server = server;
            StartReceive();
           
        }
        void StartReceive()
        {
            try
            {
                socket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallBack, null);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        //接受，分析数据逻辑
        void ReceiveCallBack(IAsyncResult iar)
        {
            try
            {
                if(socket==null||socket.Connected==false) { return; }
                int len = socket.EndReceive(iar);
                if (len == 0)
                {
                    return;
                }
                message.ReadBuffer(len, HandleRequest);
                StartReceive();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReceiveCallBack异常");
                Console.WriteLine(ex.Message);
            }
           
        }
        void HandleRequest(MainPack pack)
        {
            server.HandleRequest(pack, this);
            Console.WriteLine("服务器响应对应函数:" + pack.Actioncode);
        }

        public void Send(MainPack pack)
        {
            socket.Send(Message.PackeData(pack));
        }
        /// <summary>
        /// 每个客户端调用自己的用户信息
        /// </summary>
        public bool Logon(MainPack pack)
        {
            return GetUserData.Logon(pack,mysqlCon);
        }

        private void Close()
        {
            server.RemoveClient(this);
            socket.Close();
            mysqlCon.Close();
        }
    }
}
