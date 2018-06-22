using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace WebSlinger
{
    public class WhoIsRecord
    {
        public WhoIsRecord()
        {
        }
        
        private double _mainseqno;
        public double Mainseqno
        {
            get { return _mainseqno; }
            set { _mainseqno = value; }
        }

        private string _whoIsDDN;
        public string WhoIsDDN
        {
            get { return _whoIsDDN; }
            set { _whoIsDDN = value; }
        }

        private DateTime _createDt;
        public DateTime CreateDt
        {
            get { return _createDt; }
            set { _createDt = value; }
        }
        
        private string _nameServer;
        public string NameServer
        {
            get { return _nameServer; }
            set { _nameServer = value; }
        }

        private Boolean _COMexist;
        public Boolean COMexist
        {
            get { return _COMexist; }
            set { _COMexist = value; }
        }

        private bool _NETexist;
        public bool NETexist
        {
            get { return _NETexist; }
            set { _NETexist = value; }
        }

        private bool _ORGExist;
        public bool ORGExist
        {
            get { return _ORGExist; }
            set { _ORGExist = value; }
        }

        private bool _INFOExist;
        public bool INFOExist
        {
            get { return _INFOExist; }
            set { _INFOExist = value; }
        }

        public bool Insert()
        {
            
            
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr);
            string cmdStr = "INSERT INTO WhoIs (mainseqno, whoIsDDN, createDt, nameServer, COMexist, NETexist, ORGexist, INFOexist) "
                            + "VALUES (@mainseqno, @whoIsDDN, @createDt, @nameServer, @COMexist, @NETexist, @ORGexist, @INFOexist)";

            SqlCommand cmd = new SqlCommand(cmdStr, conn);
            cmd.Parameters.AddWithValue("@mainseqno", Mainseqno);
            cmd.Parameters.AddWithValue("@whoIsDDN", WhoIsDDN);
            if (CreateDt != DateTime.MinValue)
            {
                cmd.Parameters.AddWithValue("@createDt", CreateDt);
            }
            else
            {
                cmd.Parameters.AddWithValue("@createDt", DBNull.Value);
            }

            if (NameServer == null)
            {
                return false;
            }
            //cmd.Parameters.AddWithValue("@createDt", CreateDt);
            cmd.Parameters.AddWithValue("@nameServer", NameServer);
            cmd.Parameters.AddWithValue("@COMexist", COMexist);
            cmd.Parameters.AddWithValue("@NETexist", NETexist);
            cmd.Parameters.AddWithValue("@ORGexist", ORGExist);
            cmd.Parameters.AddWithValue("@INFOexist", INFOExist);
            
            conn.Open();
            int rtn = cmd.ExecuteNonQuery();

            return rtn == 1;
        }

        public void Clear()
        {
            Mainseqno = 0;
            WhoIsDDN = "";
            CreateDt = DateTime.MinValue;
            NameServer = "";
            COMexist = false;
            NETexist = false;
            ORGExist = false;
            INFOExist = false;
        }
    }
}
