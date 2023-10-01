using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
using Google.Protobuf;

namespace Server_netFrameWork.Tool
{
    public class Message
    {
        //存放报文，一个byte[i]元素就是一字节大小
        private byte[] buffer = new byte[1024];
        //存取了多少数据,每次读的开始，就是上一数据段的段尾
        private int startindex;

        public byte[] Buffer
        {
            get { return buffer; }
        }
        public int StartIndex
        {
            get { return startindex; }
        }
        //剩余的空间
        public int Remsize
        {
            get { return buffer.Length - startindex; }
        }

        /// <summary>
        ///分包函数 todo涉及到tcp 粘包问题，请学了计算机网络再回来研究吧 
        /// </summary>
        /// <param name="len"></param>
        public void  ReadBuffer(int len,Action<MainPack> HandleRequest)
        {
            try
            {
                startindex += len;
                if (startindex <= 4)//只有包头
                {
                    return;
                }
                //将包头(包体长度信息)转化为整数
                int count = BitConverter.ToInt32(buffer, 0);

                while (true)
                {
                    if (startindex >= (count + 4))
                    {
                        MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);

                        HandleRequest(pack);
                        //
                        Array.Copy(buffer, count + 4, buffer, 0, startindex - count - 4);

                        startindex -= (count + 4);
                    }
                    else
                    {
                        break;
                    }
                }
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
        }

       /// <summary>
       /// 对应的打包函数
       /// </summary>
       /// <param name="pack"></param>
       /// <returns></returns>
        public static byte[] PackeData(MainPack pack)
        {
            byte[] data=pack.ToByteArray();
            byte[] head=BitConverter.GetBytes(data.Length);//包头
            return head.Concat(data).ToArray();
        }
    }
}
