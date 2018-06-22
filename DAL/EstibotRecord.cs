using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace WebSlinger
{
    public class EstibotRecord
    {
        public EstibotRecord()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = 'EST'";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    int iSeq = r.GetOrdinal("rsrcURL");
                    while (r.Read())
                    {
                        RsrcURL = r.IsDBNull(iSeq) ? "NULL" : r.GetString(iSeq);
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

        private string _estDDN;
        public string estDDN
        {
            get { return _estDDN; }
            set { _estDDN = value; }
        }

        private int _ppcAds;
        public int PpcAds
        {
            get { return _ppcAds; }
            set { _ppcAds = value; }
        }

        private decimal _maxPPCamount;
        public decimal MaxPPCamount
        {
            get { return _maxPPCamount; }
            set { _maxPPCamount = value; }
        }

        private int _overtureNum;
        public int OvertureNum
        {
            get { return _overtureNum; }
            set { _overtureNum = value; }
        }

        private int _worktracker;
        public int Worktracker
        {
            get { return _worktracker; }
            set { _worktracker = value; }
        }
        
        private int _numSearches;
        public int NumSearches
        {
            get { return _numSearches; }
            set { _numSearches = value; }
        }
        
        private int _traffic;
        public int Traffic
        {
            get { return _traffic; }
            set { _traffic = value; }
        }

        private string _regExpr;
        public string RegExpr
        {
            get { return _regExpr; }
            set { _regExpr = value; }
        }

        private string _rsrcURL;
        public string RsrcURL
        {
            get { return _rsrcURL; }
            set { _rsrcURL = value; }
        }

        public bool Insert()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr);
            string cmdStr = "INSERT INTO Estibot "
                + "(mainseqno, estDDN, ppcAds, maxPPCamount, overatureNum, worktracker, numSearches, traffic) "
                + "VALUES (@mainseqno, @estDDN, @ppcAds, @maxPPCamount, @overatureNum, @worktracker, @numSearches, @traffic)";
           

            SqlCommand cmd = new SqlCommand(cmdStr, conn);
            cmd.Parameters.AddWithValue("@mainseqno", Mainseqno);
            cmd.Parameters.AddWithValue("@estDDN", estDDN);
            cmd.Parameters.AddWithValue("@ppcAds", PpcAds);
            cmd.Parameters.AddWithValue("@maxPPCamount", MaxPPCamount);
            cmd.Parameters.AddWithValue("@overatureNum", OvertureNum);
            cmd.Parameters.AddWithValue("@worktracker", Worktracker);
            cmd.Parameters.AddWithValue("@numSearches", NumSearches);
            cmd.Parameters.AddWithValue("@traffic", Traffic);
            conn.Open();
            int rtn = cmd.ExecuteNonQuery();

            return rtn == 1;
        }

        public void Clear()
        {
            Mainseqno = 0;
            estDDN = "";
            PpcAds = 0;
            MaxPPCamount = 0;
            OvertureNum = 0;
            Worktracker = 0;
            NumSearches = 0;
            Traffic = 0;

        }
        
    }
}
