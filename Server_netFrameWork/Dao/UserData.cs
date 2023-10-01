using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SocketGameProtocol;
namespace Server_netFrameWork.Dao
{
    public class UserData
    {
     
        public UserData() 
        {
           
        }

        private void ConnectMySql()
        {
           
        }

        /// <summary>
        /// 数据库的logon操作
        /// </summary>
        /// <param name="pack"></param>
        /// <returns></returns>
        public bool Logon(MainPack pack, MySqlConnection mysqlCon)
        {
           
            string username=pack.LoginPack.Username;
            string password=pack.LoginPack.Password;
            Console.WriteLine("插入数据" + username);
            //string sqlSelect = "SELECT * FROM sys.userdata where username='@username'";
            //MySqlCommand cmd = new MySqlCommand(sqlSelect,mysqlCon);
            //MySqlDataReader read= cmd.ExecuteReader();
            //if(read.Read())
            //{
            //    Console.WriteLine("已经被注册");
            //    return false;
            //}

            string sql = "INSERT INTO `sys`.`userdata` (`username`, `password`) VALUES ('" + username + "', '" + password + "')";
            MySqlCommand comd=new MySqlCommand(sql,mysqlCon);
            try
            {
                comd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入失败");
                Console.WriteLine(ex.Message);
                return false;
            }
           return true;
        }
    }
}
