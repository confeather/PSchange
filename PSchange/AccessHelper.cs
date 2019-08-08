using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PSchange
{
    class AccessHelper
    {
        public static string DBName = "PSchangeDB";

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPasswd"></param>
        /// <returns></returns>
        public static bool CheckUser(string userName,string userPasswd)
        {
            bool login = false;

            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand command = new OleDbCommand("select * from UserList where userName = '" + userName + "'" + "and passwd = '" + userPasswd + "'", conn);
            OleDbDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("登陆成功！", "message", MessageBoxButton.OK);
                login = true;

                string type = dr.GetString(dr.GetOrdinal("type"));
                if (type == "manager")
                {
                    GameListWindow gameListWindow = new GameListWindow();
                    gameListWindow.Show();
                }
                else
                {
                    string nickname = dr.GetString(dr.GetOrdinal("nickName"));
                    string userID = dr.GetInt32(dr.GetOrdinal("ID")).ToString();
                    EventCenter.Broadcast<string,string>(EventType.Login, nickname,userID);
                }
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "message", MessageBoxButton.OK);
            }

            conn.Close();

            return login;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPasswd"></param>
        public static void RegistUser(string userName, string userPasswd,string nickName,string phone,string qq,string email,string address)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand command = new OleDbCommand("select * from UserList where userName = '" + userName  + "'", conn);
            OleDbDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("用户名已存在！", "message", MessageBoxButton.OK);
            }
            else
            {
                OleDbCommand command2 = new OleDbCommand("insert into UserList(userName,passwd,nickName,phone,qq,email,address) values('" 
                                                                              + userName + "','" + 
                                                                              userPasswd + "','" +  
                                                                              nickName + "','" +
                                                                              phone + "','" + 
                                                                              qq + "','" + 
                                                                              email + "','" + 
                                                                              address + "')", conn);
                command2.ExecuteNonQuery();
                MessageBox.Show("注册成功！", "message", MessageBoxButton.OK);
            }

            conn.Close();
        }
        /// <summary>
        /// 得到用户昵称
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserNickName(string userID)
        {
            string nickname = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            if (userID != "")
            {
                OleDbCommand command = new OleDbCommand("select * from UserList where ID = " + userID, conn);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    nickname = dr.GetString(dr.GetOrdinal("nickName"));
                }
            }

            conn.Close();
            conn.Dispose();
            conn = null;
            return nickname;
        }
        /// <summary>
        /// 得到用户电话
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserPhone(string userID)
        {
            string phone = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            if (userID != "")
            {
                OleDbCommand command = new OleDbCommand("select * from UserList where ID = " + userID, conn);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("phone")))
                    {
                        phone = dr.GetString(dr.GetOrdinal("phone"));
                    }

                }
            }

            

           
            conn.Close();
            conn.Dispose();
            conn = null;
            return phone;
        }
        /// <summary>
        /// 得到用户QQ
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserQQ(string userID)
        {
            string qq = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            if (userID!="")
            {
                OleDbCommand command = new OleDbCommand("select * from UserList where ID = " + userID, conn);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("qq")))
                    {
                        qq = dr.GetString(dr.GetOrdinal("qq"));
                    }

                }
            }

          
            conn.Close();
            conn.Dispose();
            conn = null;
            return qq;
        }
        /// <summary>
        /// 得到用户email
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserEmail(string userID)
        {
            string email = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            if (userID!="")
            {
                OleDbCommand command = new OleDbCommand("select * from UserList where ID = " + userID, conn);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("email")))
                    {
                        email = dr.GetString(dr.GetOrdinal("email"));
                    }

                }
            }

            conn.Close();
            conn.Dispose();
            conn = null;
            return email;
        }
        /// <summary>
        /// 得到用户地址
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserAddress(string userID)
        {
            string address = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            if (userID!="")
            {
                OleDbCommand command = new OleDbCommand("select * from UserList where ID = " + userID, conn);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("address")))
                    {
                        address = dr.GetString(dr.GetOrdinal("address"));
                    }


                }
            }

           

            conn.Close();
            conn.Dispose();
            conn = null;
            return address;
        }
        /// <summary>
        /// 得到用户密码
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserPasswd(string userID)
        {
            string passwd = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from UserList where ID = " + userID, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                passwd = dr.GetString(dr.GetOrdinal("passwd"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;
            return passwd;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userID"></param>
        public static void UpdateUser(string userID,string passwd,string nickname,string phone,string qq,string email,string address)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("update UserList set passwd = '" + passwd + "'," +
                                                                        "nickName = '" + nickname + "'," +
                                                                        "phone = '" + phone + "'," +
                                                                        "qq = '" + qq + "'," +
                                                                        "email = '" + email + "'," +
                                                                        "address = '" + address + "'" +
                                                                        "where ID = " + userID, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }

        public static List<string> GetGameList()
        {
            List<string> gameList = new List<string>();

            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            //MaxValue = Convert.ToInt32(new OleDbCommand("select Count(*) from Student", Olecon).ExecuteScalar());

            OleDbCommand command = new OleDbCommand("select * from GameList", conn);
            OleDbDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                string zhName = dr.GetString(dr.GetOrdinal("ZhName"));
                string enName = dr.GetString(dr.GetOrdinal("EnName"));
                string ID = dr.GetInt32(dr.GetOrdinal("ID")).ToString();
                gameList.Add(zhName + "," + enName + "," + ID);
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return gameList;

        }
        
        /// <summary>
        /// 得到游戏中文名称
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameZhName(int index)
        {
            string[] gameList = GetGameList()[index].Split(',');
            string zhname = gameList[0];
            return zhname;
        }
        public static string GetGameZhName(string id)
        {
            string zhName = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
           
            OleDbCommand command = new OleDbCommand("select * from GameList where ID = " + id, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                zhName = dr.GetString(dr.GetOrdinal("ZhName"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return zhName;
        }

        /// <summary>
        /// 得到游戏英文名称
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameEnName(int index)
        {
            string[] gameList = GetGameList()[index].Split(',');
            string enname = gameList[1];
            return enname;
            //string enName = "";
            //string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            //string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            //OleDbConnection conn = new OleDbConnection(conStr);
            //conn.Open();
            ////MaxValue = Convert.ToInt32(new OleDbCommand("select Count(*) from Student", Olecon).ExecuteScalar());

            //OleDbCommand command = new OleDbCommand("select * from GameList where ID = " + id, conn);
            //OleDbDataReader dr = command.ExecuteReader();
            //if (dr.Read())
            //{
            //    enName = dr.GetString(dr.GetOrdinal("EnName"));
            //}

            //dr.Close();
            //dr = null;

            //command.Cancel();
            //command.Dispose();
            //command = null;

            //conn.Close();
            //conn.Dispose();
            //conn = null;

            //return enName;
        }
        public static string GetGameEnName(string id)
        {
            string enName = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
        
            OleDbCommand command = new OleDbCommand("select * from GameList where ID = " + id, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                enName = dr.GetString(dr.GetOrdinal("EnName"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return enName;
        }
        /// <summary>
        /// 得到游戏ID
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameID(int index)
        {
            string[] gameList = GetGameList()[index].Split(',');
            string id = gameList[2];
            return id;
            //List<string> gameList = new List<string>();

            //string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            //string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            //OleDbConnection conn = new OleDbConnection(conStr);
            //conn.Open();
            ////MaxValue = Convert.ToInt32(new OleDbCommand("select Count(*) from GameList", conn).ExecuteScalar());

            //OleDbCommand command = new OleDbCommand("select * from GameList", conn);
            //OleDbDataReader dr = command.ExecuteReader();
            //if (dr.Read())
            //{
            //    string ID = dr.GetInt32(dr.GetOrdinal("ID")).ToString();
            //    gameList.Add(ID);
            //}

            //dr.Close();
            //dr = null;

            //command.Cancel();
            //command.Dispose();
            //command = null;

            //conn.Close();
            //conn.Dispose();
            //conn = null;
            //Console.Write(gameList.Count);
            //string id = gameList[index];
            //return id;

        }
        /// <summary>
        /// 得到游戏总数
        /// </summary>
        /// <returns></returns>
        public static int GetGameCount()
        {
            int count = 0;
          
            count = GetGameList().Count;
            return count;
        }
        /// <summary>
        /// 得到游戏对应封面
        /// </summary>
        /// <param name="zhname"></param>
        /// <returns></returns>
        public static BitmapImage GetGameImage(string gameID)
        {
            if (gameID == "")
            {
                BitmapImage empty = new BitmapImage();
                return empty;
            }


            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from GameList where ID = " + gameID, conn);
            OleDbDataReader dr = command.ExecuteReader();
            byte[] buff = null;
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr["pic"]))
                {
                    buff = (byte[])dr["pic"];
                }
               
            }
            BitmapImage bi = new BitmapImage();
            
            if (!Convert.IsDBNull(dr["pic"]))
            {
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(buff);
                try
                {
                    bi.EndInit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    bi = new BitmapImage();
                    MessageBox.Show("封面无法显示！", "message", MessageBoxButton.OK);

                    //throw;
                }
            }

            
            conn.Close();

            return bi;
        }
        /// <summary>
        /// 存入游戏封面
        /// </summary>
        /// <param name="zhname"></param>
        /// <param name="filepath"></param>
        public static void SaveGameImage(string gameID, string filepath)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            FileStream fs = new FileStream(filepath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] buffer = br.ReadBytes((int)fs.Length);
            OleDbCommand command = new OleDbCommand("update GameList set pic = @pic where ID = " + gameID, conn);
            command.Parameters.AddWithValue("@pic", buffer);
            command.ExecuteNonQuery();
            br.Close();
            fs.Close();
            conn.Close();
        }
        /// <summary>
        /// 更改游戏信息
        /// </summary>
        public static void UpdateGameInfo(string gameID,string zhname,string enname)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("update GameList set ZhName = '" + zhname + "',EnName = '" + enname + "' where ID = " + gameID, conn);
            command.ExecuteNonQuery();
 
            conn.Close();
        }
        /// <summary>
        /// 新增游戏信息
        /// </summary>
        /// <param name="zhname"></param>
        /// <param name="enname"></param>
        public static void AddGameInfo(string zhname, string enname)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("insert into GameList(ZhName,EnName) values('" + zhname + "','" + enname + "')", conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
        /// <summary>
        /// 删除游戏信息
        /// </summary>
        /// <param name="gameID"></param>
        public static void DeleteGameInfo(string gameID)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("DELETE FROM GameList WHERE ID = " + gameID , conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
        /// <summary>
        /// 此为搜索使用
        /// </summary>
        /// <returns></returns>
        public static List<string> GetGameListName()
        {
            List<string> gameList = new List<string>();

            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from GameList", conn);
            OleDbDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                string zhName = dr.GetString(dr.GetOrdinal("ZhName"));
                string enName = dr.GetString(dr.GetOrdinal("EnName"));
                gameList.Add(zhName + enName);
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return gameList;

        }
        /// <summary>
        /// 搜索：返回游戏索引
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetSearchGameIndex(string str)
        {
            //Console.WriteLine(str);
            int index = 0;
            int count = 0;
            List<string> gameList = GetGameListName();

            foreach (string content in gameList)
            {
                count++;
                if (content.Contains(str))
                {
                    index = count - 1;
                    break;
                }
            }

            return index;
        }
        /// <summary>
        /// 得到游戏库存
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public static string GetGameStorage(string gameID)
        {
            string storage = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            
            OleDbCommand command = new OleDbCommand("select * from GameList where ID = " + gameID, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                storage = dr.GetInt32(dr.GetOrdinal("storage")).ToString();
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return storage;
        }
        /// <summary>
        /// 新增二手信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        /// <param name="price"></param>
        /// <param name="changeGame"></param>
        /// <param name="message"></param>
        public static void AddGameStorage(string type,string gameID,string userID,string price,string changeGame,string message)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("insert into StorageInfo(type,gameID,userID,price,changeGame,message) values('" + type + "','"
                                                                                                                                    + gameID + "','"
                                                                                                                                    + userID + "','"
                                                                                                                                    + price + "','"
                                                                                                                                    + changeGame + "','"
                                                                                                                                    + message + "')", conn);
            command.ExecuteNonQuery();
            OleDbCommand command2 = new OleDbCommand("update GameList set storage = storage + 1 where ID = " + gameID, conn);
            command2.ExecuteNonQuery();

            conn.Close();
        }

        public static List<string> GetGameStorageList(string gameid)
        {
            List<string> gameList = new List<string>();

            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            //MaxValue = Convert.ToInt32(new OleDbCommand("select Count(*) from Student", Olecon).ExecuteScalar());

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where gameID = '" + gameid + "'", conn);
            OleDbDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                string type = dr.GetString(dr.GetOrdinal("type"));
                string userID = dr.GetString(dr.GetOrdinal("userID"));
                string price = dr.GetString(dr.GetOrdinal("price"));
                string changeGame = dr.GetString(dr.GetOrdinal("changeGame"));
                string message = dr.GetString(dr.GetOrdinal("message"));
                string storageID = dr.GetInt32(dr.GetOrdinal("ID")).ToString();
                gameList.Add(type + "," + userID + "," + price + "," + changeGame + "," + message + "," + storageID);
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return gameList;

        }

        public static List<string> GetGameStorageListFromUserID(string userid)
        {
            List<string> gameList = new List<string>();

            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            //MaxValue = Convert.ToInt32(new OleDbCommand("select Count(*) from Student", Olecon).ExecuteScalar());

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where userID = '" + userid + "'", conn);
            OleDbDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                string type = dr.GetString(dr.GetOrdinal("type"));
                string userID = dr.GetString(dr.GetOrdinal("userID"));
                string price = dr.GetString(dr.GetOrdinal("price"));
                string changeGame = dr.GetString(dr.GetOrdinal("changeGame"));
                string message = dr.GetString(dr.GetOrdinal("message"));
                string storageID = dr.GetInt32(dr.GetOrdinal("ID")).ToString();
                string gameID = dr.GetString(dr.GetOrdinal("gameID"));
                gameList.Add(type + "," + userID + "," + price + "," + changeGame + "," + message + "," + storageID + "," + gameID);
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return gameList;

        }

        /// <summary>
        /// 得到二手信息的类型
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public static string GetGameStorageType(string gameID,int index)
        {
            string[] gameList = GetGameStorageList(gameID)[index].Split(',');
            string type = gameList[0];
            return type;
        }
        public static string GetGameStorageType(string storageid)
        {
            string type = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where ID = " + storageid, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                type = dr.GetString(dr.GetOrdinal("type"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return type;
        }
        /// <summary>
        /// 得到二手信息发布者ID
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameStorageUserID(string gameID, int index)
        {
            string[] gameList = GetGameStorageList(gameID)[index].Split(',');
            string user = gameList[1];
            return user;
        }
        public static string GetGameStorageUserID(string storageid)
        {
            string userid = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where ID = " + storageid, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                userid = dr.GetString(dr.GetOrdinal("userID"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return userid;
        }
        /// <summary>
        /// 得到二手信息中的价格
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameStoragePrice(string gameID, int index)
        {
            string[] gameList = GetGameStorageList(gameID)[index].Split(',');
            string price = gameList[2];
            return price;
        }
        public static string GetGameStoragePrice(string storageid)
        {
            string price = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where ID = " + storageid, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                price = dr.GetString(dr.GetOrdinal("price"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return price;
        }
        /// <summary>
        /// 得到二手信息中的交换游戏
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameStorageChangeGame(string gameID, int index)
        {
            string[] gameList = GetGameStorageList(gameID)[index].Split(',');
            string changeGame = gameList[3];
            return changeGame;
        }
        public static string GetGameStorageChangeGame(string storageid)
        {
            string changeGame = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where ID = " + storageid, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                changeGame = dr.GetString(dr.GetOrdinal("changeGame"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return changeGame;
        }
        /// <summary>
        /// 得到二手信息中的留言
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameStorageMessage(string gameID, int index)
        {
            string[] gameList = GetGameStorageList(gameID)[index].Split(',');
            string message = gameList[4];
            return message;
        }
        public static string GetGameStorageMessage(string storageid)
        {
            string message = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where ID = " + storageid, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                message = dr.GetString(dr.GetOrdinal("message"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return message;
        }
        /// <summary>
        /// 得到二手信息的ID
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetGameStorageID(string gameID, int index)
        {
            string[] gameList = GetGameStorageList(gameID)[index].Split(',');
            string id = gameList[5];
            return id;
        }    
        public static string GetGameStorageIDFromUserID(string userID,int index)
        {
            string[] gameList = GetGameStorageListFromUserID(userID)[index].Split(',');
            string id = gameList[5];
            return id;
        }
        
        /// <summary>
        /// 得到二手信息中的游戏id
        /// </summary>
        /// <param name="storageid"></param>
        /// <returns></returns>
        public static string GetGameStorageGameID(string storageid)
        {
            string gameid = "";
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("select * from StorageInfo where ID = " + storageid, conn);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                gameid = dr.GetString(dr.GetOrdinal("gameID"));
            }

            dr.Close();
            dr = null;

            command.Cancel();
            command.Dispose();
            command = null;

            conn.Close();
            conn.Dispose();
            conn = null;

            return gameid;
        }
        public static string GetGameStorageGameIDFromUserID(string userID, int index)
        {
            string[] gameList = GetGameStorageListFromUserID(userID)[index].Split(',');
            string id = gameList[6];
            return id;
        }

        /// <summary>
        /// 得到库存信息中此用户发布的消息总数
        /// </summary>
        /// <returns></returns>
        public static int GetGameStorageCountFromUserID(string userid)
        {
            int count = 0;
            //string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            //string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            //OleDbConnection conn = new OleDbConnection(conStr);
            //conn.Open();
            //count = Convert.ToInt32(new OleDbCommand("select Count(*) from StorageInfo where userID = " + userid, conn).ExecuteScalar());

            //conn.Close();
            //count = GetGameList().Count;
            count = GetGameStorageListFromUserID(userid).Count;
            return count;
        }
        /// <summary>
        /// 更新“已发布”的二手消息
        /// </summary>
        /// <param name="storageID"></param>
        /// <param name="type"></param>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        /// <param name="price"></param>
        /// <param name="changeGame"></param>
        /// <param name="message"></param>
        public static void UpdateGameStorageInfo(string storageID,string type,string gameID,string userID,string price,string changeGame,string message)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("update StorageInfo set type = '" + type 
                                                                          + "',gameID = '" + gameID
                                                                          + "',userID = '" + userID
                                                                          + "',price = '" + price
                                                                          + "',changeGame = '" + changeGame
                                                                          + "',message = '" + message
                                                                          + "' where ID = " + storageID, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
        /// <summary>
        /// 删除“已发布”的某条信息
        /// </summary>
        /// <param name="storageID"></param>
        public static void DeleteGameStorageInfo(string storageID)
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DBName + ".accdb";
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + strPath;
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();

            OleDbCommand command = new OleDbCommand("DELETE FROM StorageInfo WHERE ID = " + storageID, conn);
            command.ExecuteNonQuery();
            string gameID = GetGameStorageGameID(storageID);
            OleDbCommand command2 = new OleDbCommand("update GameList set storage = storage - 1 where ID = " + gameID, conn);
            command2.ExecuteNonQuery();
            conn.Close();


        }


    }
}
