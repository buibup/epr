using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;
using System.Configuration;
using App.Data.Model;
using EnterpriseWebModel;
using System.Data.SqlClient;

namespace App.Data.Dao
{
    public class UserDAO
    {
        public string GetString(CacheDataReader reader, int colIndex)
        {
            if (reader[colIndex] != null)
            {
                String data = reader[colIndex].ToString();
                return data;
            }
            else
                return string.Empty;
        }
       
        public string DeclipPWD(string password) {           
            int xconst = 37;
            string xout = "";
            int xpiece = 0;
            string xchar="";
            Decimal xnum;
            for (xpiece = 0; xpiece < password.Length ; xpiece++) {
                xchar = password.Substring(xpiece, 1);
                if (xchar == "D")
                {
                    xchar = Convert.ToString((char)(2));
                }
                else if (xchar == "p")
                {
                    xchar = Convert.ToString((char)(3));
                }
                else if (xchar == "d")
                {
                    xchar = Convert.ToString((char)(4));
                }
                else if (xchar == "t") {
                    xchar = Convert.ToString((char)(5));
                }
                
                byte[] asciiBytes = System.Text.Encoding.ASCII.GetBytes(xchar);
                xnum = (asciiBytes[0]-(xpiece+1)+xconst) % 255;
                if (xnum > 127)
                {
                    xnum = (xnum + 128) % 255;
                }
                else if (xnum < 32)
                {
                    xnum = (xnum + 40) % 255;
                }
                if (Convert.ToString((char)(xnum)) == "^") {
                    xnum = xnum + 1;
                    
                }
                xout = xout + (char)(xnum % 255);

            }



            int xlen = xout.Length;
            for (xpiece = xlen + 1;  xpiece <= 12; xpiece++) {
                xchar = xout.Substring(xpiece - 1 - xlen, 1);
                byte[] asciiBytes = System.Text.Encoding.ASCII.GetBytes(xchar);
                xnum = ((decimal)asciiBytes[0] * (decimal)2.345)  * xconst * (xconst-7)  % 255;
                //xnum = xnum.Truncate(xnum);
                if (xnum > 127)
                {
                    xnum = (xnum + 128) % 255;
                }
                else if (xnum < 32) {
                    xnum = (xnum + 40) % 255;
                }
                if (Convert.ToString((char)(xnum)) == "^")
                {
                    xnum = xnum++;
                    
                }
                xout = xout + (char)(xnum % 255);

            }

            return xout;


           
        }
        public static object GetScalar(string sql, params object[] Parms)
        {
            SymmetricEncryption oEnc = new SymmetricEncryption(Constants1.XKEY);

            var connstr = oEnc.Decrypt(System.Configuration.ConfigurationManager.AppSettings["BconnectConnectionString"]);

            using (SqlConnection connection = new SqlConnection(connstr))
            {
                string queryString = string.Format(sql, Parms);


                // Open the connection and fill the DataSet.
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(string.Format(sql, Parms), connection))
                    {
                        return command.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public UserModel GetUser(string username, string password)
        {
            UserModel user = new UserModel();
            var b = new BconnectEncryptPassWord.BconenctAPI();
            var passwordenc = b.encryptPassword(password);

            user.SSUSR_Initials = username;
            user.SSUSR_Password = passwordenc;

            string sql = "select count(*) from Login where loginname='{0}' and statusflag='A' and password='{1}' and activefrom <=getdate() and (activeto is null or activeto >getdate())";
            var a = GetScalar(sql, username, passwordenc);
            if ((int)a > 0) {
                user.session = HttpContext.Current.Session.SessionID;
                // System.Web.HttpContext.Current.Session["User"] = user;
                //Session.Add["User"] = "";
                
            }
            return user;

        }

    }
}