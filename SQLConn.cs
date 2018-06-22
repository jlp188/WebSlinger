using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace WebSlinger
{
    public class SQLConn
    {
        private SqlConnection connection;

        public SQLConn()
        {
            connection = new SqlConnection();
            connection.ConnectionString = Properties.Settings.Default.ConnStr.ToString();
        }

        public string connString
        {
            get { return connection.ConnectionString; }
            set { connection.ConnectionString = Properties.Settings.Default.ConnStr.ToString(); }
        }

        
        public void Open()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public ConnectionState State
        {
            get {return connection.State;}
            //set {connection.State = value;}
        }
        
        public void Close()
        {
            connection.Close();
        }

        public string Database
        {
            get { return connection.Database; }
            //set { connection.Database = value; }
        }

        public string DataSource
        {
            get { return connection.DataSource; }
            //set { connection.DataSource = value; }
        }

    }
}
