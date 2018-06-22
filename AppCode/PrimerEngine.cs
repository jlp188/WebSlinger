using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebSlinger
{
    class PrimerEngine
    {
        Boolean lb_Alexa,
            lb_PageRank,
            lb_Archive,
            lb_Compete,
            lb_Quantcast,
            lb_DMOZ,
            lb_WhoIs,
            lb_Estibot,
            lb_IndexPages,
            lb_Backlinks;

        public PrimerEngine()
        {

        }

        public void Start()
        {
            Int16 rtn = LoadDDN("");
            
            
            if (lb_Alexa)
            {

            }




        }

        protected void ParseRequest(string rsrc, string DDN, Int16 mainSeqno)
        {
            string pattern = "";
            string pattern2 = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.ConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM ResourceList WHERE rsrcCode = @rsrc";

            cmd.Connection = conn;

            SqlDataReader SR1 = cmd.ExecuteReader();
            if (SR1.Read()) pattern = SR1["regExpression"].ToString();
            SR1.Close();
            //SqlCommand cmd2 = new SqlCommand();
            //cmd2.CommandText = "SELECT * FROM ResourceList WHERE rsrcCode = 'ARC1'";
            //cmd2.Connection = conn;
            //SqlDataReader SR2 = cmd2.ExecuteReader();
            //if (SR2.Read()) pattern2 = SR2["regExpression"].ToString();

            
            
            switch (rsrc)
            {
                case "ARC":
                    
                    break;
                case "ARC1":

                    break;
                default:

                    break;
            }
        }

        string RequestReceive()
        {
            
            
            string source;
            return source;
        }

        public Int16 LoadDDN(string DDN)
        {


            Int16 rtn = 0;
            return rtn;
        }

    }

    


}
