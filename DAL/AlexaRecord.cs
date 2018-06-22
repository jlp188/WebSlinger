using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace WebSlinger
{
    
    class AlexaRecord
    {
        public AlexaRecord()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = 'ALX'";
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

        private string _alxDDN;
        public string alxDDN
        {
            get { return _alxDDN; }
            set { _alxDDN = value; }
        }

        private double _alxRank7D;
        public double AlxRank7D
        {
            get { return _alxRank7D; }
            set { _alxRank7D = value; }
        }

        private double _alxRank1M;
        public double AlxRank1M
        {
            get { return _alxRank1M; }
            set { _alxRank1M = value; }
        }

        private double _alxRank3M;
        public double AlxRank3M
        {
            get { return _alxRank3M; }
            set { _alxRank3M = value; }
        }

        private string _RankDir;
        public string RankDir
        {
            get { return _RankDir; }
            set { _RankDir = value; }
        }

        private double _RankChng;
        public double RankChng
        {
            get { return _RankChng; }
            set { _RankChng = value; }
        }

        private double _alxHighRank;
        public double AlxHighRank
        {
            get { return _alxHighRank; }
            set { _alxHighRank = value; }
        }

        private double _alxReach7D;
        public double AlxReach7D
        {
            get { return _alxReach7D; }
            set { _alxReach7D = value; }
        }

        private double _alxReach1M;
        public double AlxReach1M
        {
            get { return _alxReach1M; }
            set { _alxReach1M = value; }
        }

        private double _alxReach3M;
        public double AlxReach3M
        {
            get { return _alxReach3M; }
            set { _alxReach3M = value; }
        }
        
        private string _ReachDir;
        public string ReachDir
        {
            get { return _ReachDir; }
            set { _ReachDir = value; }
        }

        private double _ReachChng;
        public double ReachChng
        {
            get { return _ReachChng; }
            set { _ReachChng = value; }
        }

        private double _alxHighReach;
        public double AlxHighReach
        {
            get { return _alxHighReach; }
            set { _alxHighReach = value; }
        }

        private double _alxSearch7D;
        public double AlxSearch7D
        {
            get { return _alxSearch7D; }
            set { _alxSearch7D = value; }
        }

        private double _alxSearch1M;
        public double AlxSearch1M
        {
            get { return _alxSearch1M; }
            set { _alxSearch1M = value; }
        }

        private double _alxSearch3M;
        public double AlxSearch3M
        {
            get { return _alxSearch3M; }
            set { _alxSearch3M = value; }
        }

        private string _SearchDir;
        public string SearchDir
        {
            get { return _SearchDir; }
            set { _SearchDir = value; }
        }

        private double _SearchChng;
        public double SearchChng
        {
            get { return _SearchChng; }
            set { _SearchChng = value; }
        }

        private double _alxHighSearch;
        public double AlxHighSearch
        {
            get { return _alxHighSearch; }
            set { _alxHighSearch = value; }
        }

        private string _alxCountry1;
        public string AlxCountry1
        {
            get { return _alxCountry1; }
            set { _alxCountry1 = value; }
        }

        private double _alxCntry1_per;
        public double AlxCntry1_per
        {
            get { return _alxCntry1_per; }
            set { _alxCntry1_per = value; }
        }

        private string _alxCountry2;
        public string AlxCountry2
        {
            get { return _alxCountry2; }
            set { _alxCountry2 = value; }
        }

        private double _alxCntry2_per;
        public double AlxCntry2_per
        {
            get { return _alxCntry2_per; }
            set { _alxCntry2_per = value; }
        }
        
        private string _alxCountry3;
        public string AlxCountry3
        {
            get { return _alxCountry3; }
            set { _alxCountry3 = value; }
        }

        private double _alxCntry3_per;
        public double AlxCntry3_per
        {
            get { return _alxCntry3_per; }
            set { _alxCntry3_per = value; }
        }

        private double _alxScore;
        public double AlxScore
        {
            get { return _alxScore; }
            set { _alxScore = value; }
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
                string cmdStr = "INSERT INTO Alexa (mainseqno, alxDDN, alxRank7D, alxRank1M, alxRank3M, alxRankChng, alxRankDir, "
               + "alxReach7D, alxReach1M, alxReach3M, alxReachChng, alxReachDir, "
               + "alxSearch7D, alxSearch1M, alxSearch3M, alxSearchChng, alxSearchDir, "
               + "alxCountry1, alxCntry1_per, alxCountry2, alxCntry2_per, alxCountry3, alxCntry3_per, alxScore) "
               + "VALUES (@mainseqno, @alxDDN, @alxRank7D, @alxRank1M, @alxRank3M, @alxRankChng, @alxRankDir, "
               + "@alxReach7D, @alxReach1M, @alxReach3M, @alxReachChng, @alxReachDir, "
               + "@alxSearch7D, @alxSearch1M, @alxSearch3M, @alxSearchChng, @alxSearchDir, "
               + "@alxCountry1, @alxCntry1_per, @alxCountry2, @alxCntry2_per, @alxCountry3, @alxCntry3_per, @alxScore)";

                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@mainseqno", Mainseqno);
                cmd.Parameters.AddWithValue("@alxDDN", alxDDN);
                cmd.Parameters.AddWithValue("@alxRank7D", AlxRank7D);
                cmd.Parameters.AddWithValue("@alxRank1M", AlxRank1M);
                cmd.Parameters.AddWithValue("@alxRank3M", AlxRank3M);
                cmd.Parameters.AddWithValue("@alxRankChng", RankChng);
                cmd.Parameters.AddWithValue("@alxRankDir", RankDir);
                cmd.Parameters.AddWithValue("@alxReach7D", AlxReach7D);
                cmd.Parameters.AddWithValue("@alxReach1M", AlxReach1M);
                cmd.Parameters.AddWithValue("@alxReach3M", AlxReach3M);
                cmd.Parameters.AddWithValue("@alxReachChng", ReachChng);
                cmd.Parameters.AddWithValue("@alxReachDir", ReachDir);
                cmd.Parameters.AddWithValue("@alxSearch7D", AlxSearch7D);
                cmd.Parameters.AddWithValue("@alxSearch1M", AlxSearch1M);
                cmd.Parameters.AddWithValue("@alxSearch3M", AlxSearch3M);
                cmd.Parameters.AddWithValue("@alxSearchChng", SearchChng);
                cmd.Parameters.AddWithValue("@alxSearchDir", SearchDir);
                
                cmd.Parameters.AddWithValue("@alxCountry1", AlxCountry1);
                cmd.Parameters.AddWithValue("@alxCntry1_per", AlxCntry1_per);
                cmd.Parameters.AddWithValue("@alxCountry2", AlxCountry2);
                cmd.Parameters.AddWithValue("@alxCntry2_per", AlxCntry2_per);
                cmd.Parameters.AddWithValue("@alxCountry3", AlxCountry3);
                cmd.Parameters.AddWithValue("@alxCntry3_per", AlxCntry3_per);

                cmd.Parameters.AddWithValue("@alxScore", AlxScore);
                conn.Open();
                int rtn = cmd.ExecuteNonQuery();

                return rtn == 1;
            }
        }

        public void Clear()
        {
            Mainseqno = 0;
            alxDDN = "";
            AlxRank7D = 0;
            AlxRank1M = 0;
            AlxRank3M = 0;
            RankChng = 0;
            RankDir = "";
            AlxReach7D = 0;
            AlxReach1M = 0;
            AlxReach3M = 0;
            ReachChng = 0;
            ReachDir = "";
            AlxSearch7D = 0;
            AlxSearch1M = 0;
            AlxSearch3M = 0;
            SearchChng = 0;
            SearchDir = "";
            AlxScore = 0;

        }
    }


}
