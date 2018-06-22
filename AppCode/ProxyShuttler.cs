using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebSlinger
{
    class ProxyShuttler
    {
        public ProxyShuttler()
        {
            string prx = GetProxy();
        }

        string _currentProxy;
        public string CurrentProxy
        {
            get { return _currentProxy; }
            //set { _lbAlexa = value; }
        }

        /// <summary>
        /// Get a Proxy from the ProxyList table
        /// </summary>
        public string GetProxy()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.ConnStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT TOP 1 proxSeqno, proxIP FROM ProxyList WHERE proxActvFlag = 'true'";
                cmd.Connection = conn;
                string prox;

                SqlDataReader SR1 = cmd.ExecuteReader();
                SR1.Read();
                prox = SR1["proxIP"].ToString();
                SR1.Close();

                //cmd.CommandText = "UPDATE ProxyList SET proxActvFlag = 'false' WHERE proxIP = @proxIP";
                //cmd.Parameters.AddWithValue("@proxIP", prox);
                //int rtn = cmd.ExecuteNonQuery();

                _currentProxy = prox;

                return prox;
            }
        }


        /// <summary>
        /// Get a Proxy from the ProxyList table and kill oldProxy
        /// </summary>
        public string GetProxy(string oldProxy)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.ConnStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE ProxyList SET proxActvFlag = 'false' WHERE proxIP = @proxIP";
                cmd.Connection = conn; 
                cmd.Parameters.AddWithValue("@proxIP", oldProxy);
                int rtn = cmd.ExecuteNonQuery();
                
                cmd.CommandText = "SELECT TOP 1 proxSeqno, proxIP FROM ProxyList WHERE proxActvFlag = 'true'";
                cmd.Connection = conn;
                string prox;

                SqlDataReader SR1 = cmd.ExecuteReader();
                SR1.Read();
                prox = SR1["proxIP"].ToString();
                SR1.Close();

                _currentProxy = prox;

                return prox;
            }
        }
    }
}
