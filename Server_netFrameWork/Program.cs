


using Server_netFrameWork.Servers;
using System;

class Program
{
  static void Main(string[] args)
   {
        try
        {
            Server server = new Server(6666);
            Console.ReadLine();
        }catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
       
   }
}
