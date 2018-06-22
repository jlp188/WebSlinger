using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace WebSlinger
{
    public class IndexPagesRecord
    {
        public IndexPagesRecord()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = 'NDXY'";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    int iSeq = r.GetOrdinal("rsrcURL");
                    while (r.Read())
                    {
                        YrsrcURL = r.IsDBNull(iSeq) ? "NULL" : r.GetString(iSeq);
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = 'NDXG'";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    int iSeq = r.GetOrdinal("rsrcURL");
                    while (r.Read())
                    {
                        GrsrcURL = r.IsDBNull(iSeq) ? "NULL" : r.GetString(iSeq);
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = 'NDXAV'";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    int iSeq = r.GetOrdinal("rsrcURL");
                    while (r.Read())
                    {
                        AVrsrcURL = r.IsDBNull(iSeq) ? "NULL" : r.GetString(iSeq);
                    }
                }
            }


        }
        
        private double _mainseqno;
        public double Mainseqno
        {
            get { return _mainseqno; }
            set { _mainseqno = value; }
        }

        private string _idxDDN;
        public string IdxDDN
        {
            get { return _idxDDN; }
            set { _idxDDN = value; }
        }

        private DateTime _idxDtstamp;
        public DateTime IdxDtstamp
        {
            get { return _idxDtstamp; }
            set { _idxDtstamp = value; }
        }

        private string _idxSource;
        public string IdxSource
        {
            get { return _idxSource; }
            set { _idxSource = value; }
        }

        private double _idxCount;
        public double IdxCount
        {
            get { return _idxCount; }
            set { _idxCount = value; }
        }

        private string _YrsrcURL;
        public string YrsrcURL
        {
            get { return _YrsrcURL; }
            set { _YrsrcURL = value; }
        }

        private string _GrsrcURL;
        public string GrsrcURL
        {
            get { return _GrsrcURL; }
            set { _GrsrcURL = value; }
        }

        private string _AVrsrcURL;
        public string AVrsrcURL
        {
            get { return _AVrsrcURL; }
            set { _AVrsrcURL = value; }
        }

        public bool Insert()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr);
            string cmdStr = "INSERT INTO IndexPages (mainseqno, idxDDN, idxDtstamp, idxSource, idxCount) "
                            + "VALUES (@mainseqno, @idxDDN, @idxDtstamp, @idxSource, @idxCount)";

            SqlCommand cmd = new SqlCommand(cmdStr, conn);
            cmd.Parameters.AddWithValue("@mainseqno", Mainseqno);
            cmd.Parameters.AddWithValue("@idxDDN", IdxDDN);
            cmd.Parameters.AddWithValue("@idxDtstamp", IdxDtstamp);
            cmd.Parameters.AddWithValue("@idxSource", IdxSource);
            cmd.Parameters.AddWithValue("@idxCount", IdxCount);

            conn.Open();
            int rtn = cmd.ExecuteNonQuery();

            return rtn == 1;
        }

        public void Clear()
        {
            Mainseqno = 0;
            IdxDDN = "";
            IdxDtstamp = DateTime.MinValue;
            IdxSource = "";
            IdxCount = 0;

        }

    }
}
