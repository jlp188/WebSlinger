using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace WebSlinger
{
    public class CompeteRecord
    {
        
        public CompeteRecord()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = 'CMP'";
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
        
        private DateTime _lastUpdate;
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set { _lastUpdate = value; }
        }
        
        private string _cmptDomname;
        public string CmptDomname
        {
            get { return _cmptDomname; }
            set { _cmptDomname = value; }
        }
        
        private int _cmptNumVisits;
        public int CmptNumVisits
        {
            get { return _cmptNumVisits; }
            set { _cmptNumVisits = value; }
        }
        
        private int _cmptMonth1;
        public int CmptMonth1
        {
            get { return _cmptMonth1; }
            set { _cmptMonth1 = value; }
        }
        
        private int _cmptMonth2;
        public int CmptMonth2
        {
            get { return _cmptMonth2; }
            set { _cmptMonth2 = value; }
        }
        
        private int _cmptMonth3;
        public int CmptMonth3
        {
            get { return _cmptMonth3; }
            set { _cmptMonth3 = value; }
        }
        
        private int _cmptMonth4;
        public int CmptMonth4
        {
            get { return _cmptMonth4; }
            set { _cmptMonth4 = value; }
        }
        
        private int _cmptMonth5;
        public int CmptMonth5
        {
            get { return _cmptMonth5; }
            set { _cmptMonth5 = value; }
        }
        
        private int _cmptMonth6;
        public int CmptMonth6
        {
            get { return _cmptMonth6; }
            set { _cmptMonth6 = value; }
        }
        
        private int _cmptMonth7;
        public int CmptMonth7
        {
            get { return _cmptMonth7; }
            set { _cmptMonth7 = value; }
        }
        
        private int _cmptMonth8;
        public int CmptMonth8
        {
            get { return _cmptMonth8; }
            set { _cmptMonth8 = value; }
        }
        
        private int _cmptMonth9;
        public int CmptMonth9
        {
            get { return _cmptMonth9; }
            set { _cmptMonth9 = value; }
        }
        
        private int _cmptMonth10;
        public int CmptMonth10
        {
            get { return _cmptMonth10; }
            set { _cmptMonth10 = value; }
        }
        
        private int _cmptMonth11;
        public int CmptMonth11
        {
            get { return _cmptMonth11; }
            set { _cmptMonth11 = value; }
        }
        
        private int _cmptMonth12;
        public int CmptMonth12
        {
            get { return _cmptMonth12; }
            set { _cmptMonth12 = value; }
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
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "INSERT INTO Compete "
               + "(mainseqno ,lastUpdate ,cmptDomname ,cmptNumVisits ,cmptMonth1 ,cmptMonth2 ,cmptMonth3 ,cmptMonth4 "
               + ",cmptMonth5 ,cmptMonth6 ,cmptMonth7 ,cmptMonth8 ,cmptMonth9 ,cmptMonth10 ,cmptMonth11 ,cmptMonth12) "
               + "VALUES (@mainseqno, @lastUpdate, @cmptDomname, @cmptNumVisits, @cmptMonth1, @cmptMonth2, @cmptMonth3, "
               + "@cmptMonth4, @cmptMonth5, @cmptMonth6, @cmptMonth7, @cmptMonth8, @cmptMonth9, @cmptMonth10, @cmptMonth11, @cmptMonth12)";

                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@mainseqno", Mainseqno);
                cmd.Parameters.AddWithValue("@lastUpdate", LastUpdate);
                cmd.Parameters.AddWithValue("@cmptDomname", CmptDomname);
                cmd.Parameters.AddWithValue("@cmptNumVisits", CmptNumVisits);
                cmd.Parameters.AddWithValue("@cmptMonth1", CmptMonth1);
                cmd.Parameters.AddWithValue("@cmptMonth2", CmptMonth2);
                cmd.Parameters.AddWithValue("@cmptMonth3", CmptMonth3);
                cmd.Parameters.AddWithValue("@cmptMonth4", CmptMonth4);
                cmd.Parameters.AddWithValue("@cmptMonth5", CmptMonth5);
                cmd.Parameters.AddWithValue("@cmptMonth6", CmptMonth6);
                cmd.Parameters.AddWithValue("@cmptMonth7", CmptMonth7);
                cmd.Parameters.AddWithValue("@cmptMonth8", CmptMonth8);
                cmd.Parameters.AddWithValue("@cmptMonth9", CmptMonth9);
                cmd.Parameters.AddWithValue("@cmptMonth10", CmptMonth10);
                cmd.Parameters.AddWithValue("@cmptMonth11", CmptMonth11);
                cmd.Parameters.AddWithValue("@cmptMonth12", CmptMonth12);
                conn.Open();
                int rtn = cmd.ExecuteNonQuery();

                return rtn == 1;
            }
        }

        public void Clear()
        {
            Mainseqno = 0;
            LastUpdate = DateTime.MinValue;
            CmptDomname = "";
            CmptNumVisits = 0;
            CmptMonth1 = 0;
        }

    }
}
