using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.Data.SqlClient;

namespace WebSlinger
{
    class ErrLogRec
    {
        public ErrLogRec()
        {
        }

        private DateTime _errDateTime;
        public DateTime ErrDateTime
        {
            get { return _errDateTime; }
            set { _errDateTime = value; }
        }

        private string _errFxClass;
        public string ErrSeqno
        {
            get { return _errFxClass; }
            set { _errFxClass = value; }
        }

        private string _errSource;
        public string ErrSource
        {
            get { return _errSource; }
            set { _errSource = value; }
        }

        private string _errMessage;
        public string ErrMessage
        {
            get { return _errMessage; }
            set { _errMessage = value; }
        }

        private string _errInnerExcptnMsg;
        public string ErrInnerExcptnMsg
        {
            get { return _errInnerExcptnMsg; }
            set { _errInnerExcptnMsg = value; }
        }


        public bool Insert(string fxClass, Exception exc)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "INSERT INTO ErrLog (errDateTime, errFxClass, errSource, errMessage, errInnerExcptnMsg) "
                + "VALUES (@errDateTime, @errFxClass, @errSource, @errMessage, @errInnerExcptnMsg)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@errDateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@errFxClass", fxClass);
                cmd.Parameters.AddWithValue("@errSource", exc.Source);
                cmd.Parameters.AddWithValue("@errMessage", exc.Message);
                if (exc.InnerException == null)
                {
                    cmd.Parameters.AddWithValue("@errInnerExcptnMsg", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@errInnerExcptnMsg", (exc.InnerException.Message == null ? "" : exc.InnerException.Message));
                }

                conn.Open();
                int rtn = cmd.ExecuteNonQuery();

                return rtn == 1;
            }

        }
        

        public bool Insert(string fxClass, WebException exc)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "INSERT INTO ErrLog (errDateTime, errFxClass, errSource, errMessage, errInnerExcptnMsg) "
                + "VALUES (@errDateTime, @errFxClass, @errSource, @errMessage, @errInnerExcptnMsg)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@errDateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@errFxClass", fxClass);
                cmd.Parameters.AddWithValue("@errSource", exc.Source);
                cmd.Parameters.AddWithValue("@errMessage", exc.Message);
                if (exc.InnerException == null)
                {
                    cmd.Parameters.AddWithValue("@errInnerExcptnMsg", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@errInnerExcptnMsg", (exc.InnerException.Message == null ? "" : exc.InnerException.Message));
                }

                conn.Open();
                int rtn = cmd.ExecuteNonQuery();

                return rtn == 1;
            }

        }
    }

    

}
