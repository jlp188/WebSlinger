using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace WebSlinger
{
    class ProxyList
    {
        private string _proxIP;
        public string ProxyIP
        {
            get {return _proxIP;}
            set { _proxIP = value; }
        }

        private bool _proxActvFlag;
        public bool ProxyFlag
        {
            get {return _proxActvFlag;}
            set {_proxActvFlag = value;}
        }

        public bool Insert()
        {
            int rtn;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.ConnStr;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO ProxyList (proxIP, proxActvFlag) VALUES (@proxIP, @proxActvFlag)";
            cmd.Parameters.AddWithValue("@proxIP", ProxyIP);
            cmd.Parameters.AddWithValue("@proxActvFlag", ProxyFlag);
            cmd.Connection = conn;
            conn.Open();
            
            rtn = cmd.ExecuteNonQuery();
            if (rtn > 0)
            {
                ProxyIP = "";
            }
            return rtn > 0;
        }

        public bool Delete()
        {
            int rtn;
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.ConnStr;
            cmd.Connection = conn;
            conn.Open();
            
            cmd.CommandText = "DELETE FROM ProxyList";
            //cmd.Parameters.AddWithValue("@proxIP", proxIP);
            //cmd.Parameters.AddWithValue("@proxActvFlag", proxActvFlag);
            rtn = cmd.ExecuteNonQuery();
            if (rtn > 0)
            {
                ProxyIP = "";
            }

            return rtn > 0;
        }
    }
}
