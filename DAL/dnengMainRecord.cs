using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace WebSlinger
{
    public class dnengMainRecord
    {

        public dnengMainRecord(string DDN)
        {
            Dtstamp = DateTime.Now;
            if (!DDN.Contains("http://")) DDN = "http://" + DDN;
            Uri myUri = new Uri(DDN);
            string ls_Uri = myUri.Host.ToString();
            Domname = (DDN.IndexOf(".") > 0 ? DDN.Substring(0, (DDN.IndexOf("."))) : DDN);
            DomSuffix = (ls_Uri.IndexOf(".") > 0 ? ls_Uri.Substring(((ls_Uri.IndexOf(".") + 1))) : "");
            DomLength = Domname.Length;
            Source = "TEST";
            bool lb_rtn = this.Insert();

            LinkRaker LR = new LinkRaker();
            ArcRsrcURL = LR.GetURL("ARC");
            Arc1RsrcURL = LR.GetURL("ARC1");
            QuantRsrcURL = Arc1RsrcURL = LR.GetURL("QTC");
            DMOZRsrcURL = Arc1RsrcURL = LR.GetURL("DMZ");



            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr);
            //string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = 'ARC'";
            //SqlCommand cmd = new SqlCommand(cmdStr, conn);
            //conn.Open();
            //using (SqlDataReader r = cmd.ExecuteReader())
            //{
            //    int iSeq = r.GetOrdinal("rsrcURL");
            //    while (r.Read())
            //    {
            //        ArcRsrcURL = r.IsDBNull(iSeq) ? "NULL" : r.GetString(iSeq);
            //    }
            //}

        }
        
        private int _mainseqno;
        public int MainSeqNo
        {
            get { return _mainseqno; }
            set { _mainseqno = value; }
        }
        
        private int _source_seqno;
        public int Source_seqno
        {
            get { return _source_seqno; }
            set { _source_seqno = value; }
        }

        private string _source;
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        
        private DateTime _dtstamp;
        public DateTime Dtstamp
        {
            get { return _dtstamp; }
            set { _dtstamp = value; }
        }
        
        private string _domname;
        public string Domname
        {
            get { return _domname; }
            set { _domname = value; }
        }
        
        private string _domPrefix;
        public string DomPrefix
        {
            get { return _domPrefix; }
            set { _domPrefix = value; }
        }
        
        private string _domSuffix;
        public string DomSuffix
        {
            get { return _domSuffix; }
            set { _domSuffix = value; }
        }
        
        private int _domLength;
        public int DomLength
        {
            get { return _domLength; }
            set { _domLength = value; }
        }
        
        private string _grade;
        public string Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }
        
        private int _score;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        
        private int _pageRank;
        public int PageRank
        {
            get { return _pageRank; }
            set { _pageRank = value; }
        }
        
        private int _archiveLinks;
        public int ArchiveLinks
        {
            get { return _archiveLinks; }
            set { _archiveLinks = value; }
        }
        
        private DateTime _archiveOldest;
        public DateTime ArchiveOldest
        {
            get { return _archiveOldest; }
            set { _archiveOldest = value; }
        }
        
        private string _archiveKeyword;
        public string ArchiveKeyword
        {
            get { return _archiveKeyword; }
            set { _archiveKeyword = value; }
        }
        
        private int _QuantcastCtr;
        public int QuantcastCtr
        {
            get { return _QuantcastCtr; }
            set { _QuantcastCtr = value; }
        }
        
        private int _DMOZ_Catno;
        public int DMOZ_Catno
        {
            get { return _DMOZ_Catno; }
            set { _DMOZ_Catno = value; }
        }
        
        private int _DMOZ_NumResults;
        public int DMOZ_NumResults
        {
            get { return _DMOZ_NumResults; }
            set { _DMOZ_NumResults = value; }
        }

        private string _arcRsrcURL;
        public string ArcRsrcURL
        {
            get { return _arcRsrcURL; }
            set { _arcRsrcURL = value; }
        }

        private string _arc1RsrcURL;
        public string Arc1RsrcURL
        {
            get { return _arc1RsrcURL; }
            set { _arc1RsrcURL = value; }
        }

        private string _quantRsrcURL;
        public string QuantRsrcURL
        {
            get { return _quantRsrcURL; }
            set { _quantRsrcURL = value; }
        }

        private string _pgRnkRsrcURL;
        public string PgRnkRsrcURL
        {
            get { return _pgRnkRsrcURL; }
            set { _pgRnkRsrcURL = value; }
        }

        private string _dmozRsrcURL;
        public string DMOZRsrcURL
        {
            get { return _dmozRsrcURL; }
            set { _dmozRsrcURL = value; }
        }

        public bool Insert()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "INSERT INTO dnengMain (source_seqno, source, dtstamp, domname, domPrefix, domSuffix, domLength, grade, score, "
                + "PageRank, archiveLinks, archiveOldest, archiveKeyword, QuantcastCtr, DMOZ_Catno, DMOZ_NumResults) "
                + "VALUES (@source_seqno, @source, @dtstamp, @domname, @domPrefix, @domSuffix, @domLength, @grade, "
                + "@score, @PageRank, @archiveLinks, @archiveOldest, @archiveKeyword, @QuantcastCtr, @DMOZ_Catno, @DMOZ_NumResults)";

                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                Source_seqno = Source_seqno != null ? Source_seqno : Convert.ToInt32(DBNull.Value);
                cmd.Parameters.AddWithValue("@source_seqno", Source_seqno);

                Source = (Source != null ? Source : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@source", Source);

                Dtstamp = (Dtstamp != null ? Dtstamp : Convert.ToDateTime(DBNull.Value));
                cmd.Parameters.AddWithValue("@dtstamp", Dtstamp);

                Domname = (Domname != null ? Domname : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@domname", Domname);

                DomPrefix = (DomPrefix != null ? DomPrefix : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@domPrefix", DomPrefix);

                DomSuffix = (DomSuffix != null ? DomSuffix : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@domSuffix", DomSuffix);

                DomLength = (DomLength != null ? DomLength : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@domLength", DomLength);

                Grade = (Grade != null ? Grade : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@grade", Grade);

                Score = (Score != null ? Score : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@score", Score);

                PageRank = (PageRank != null ? PageRank : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@PageRank", PageRank);

                ArchiveLinks = (ArchiveLinks != null ? ArchiveLinks : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@archiveLinks", ArchiveLinks);

                ArchiveOldest = ((ArchiveOldest > DateTime.MinValue) ? ArchiveOldest : DateTime.MinValue);
                if (ArchiveOldest != DateTime.MinValue)
                {
                    cmd.Parameters.AddWithValue("@archiveOldest", ArchiveOldest);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@archiveOldest", DBNull.Value);
                }

                ArchiveKeyword = (ArchiveKeyword != null ? ArchiveKeyword : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@archiveKeyword", ArchiveKeyword);

                QuantcastCtr = (QuantcastCtr != null ? QuantcastCtr : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@QuantcastCtr", QuantcastCtr);

                DMOZ_Catno = (DMOZ_Catno != null ? DMOZ_Catno : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@DMOZ_Catno", DMOZ_Catno);

                DMOZ_Catno = (DMOZ_Catno != null ? DMOZ_Catno : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@DMOZ_NumResults", DMOZ_NumResults);

                conn.Open();
                int rtn = cmd.ExecuteNonQuery();

                if (rtn == 1)
                {
                    cmdStr = "SELECT mainseqno FROM dnengMain WHERE (domname = @domname) AND (dtstamp = @dtstamp)"; // AND (source_seqno = @source_seqno)
                    SqlCommand cmd1 = new SqlCommand(cmdStr, conn);
                    cmd1.Parameters.AddWithValue("@domname", Domname);
                    cmd1.Parameters.AddWithValue("@dtstamp", Dtstamp);
                    //cmd.Parameters.AddWithValue("@source_seqno", Source_seqno);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (SqlDataReader r = cmd1.ExecuteReader())
                    {
                        int iSeq = r.GetOrdinal("mainseqno");
                        while (r.Read())
                        {
                            MainSeqNo = Convert.ToInt32(r.GetValue(iSeq));
                        }
                    }
                }

                return rtn == 1;
            }

        }


        public bool Update()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "UPDATE dnengMain SET source_seqno = @source_seqno, source = @source, "
                + "dtstamp = @dtstamp, domname = @domname, domPrefix = @domPrefix, domSuffix = @domSuffix, "
                + "domLength = @domLength, grade = @grade, score = @score, "
                + "PageRank = @PageRank, archiveLinks = @archiveLinks, archiveOldest = @archiveOldest, "
                + "archiveKeyword = @archiveKeyword, QuantcastCtr = @QuantcastCtr, "
                + "DMOZ_Catno = @DMOZ_Catno, DMOZ_NumResults = @DMOZ_NumResults "
                + "WHERE mainseqno = @mainseqno";

                SqlCommand cmd = new SqlCommand(cmdStr, conn);

                cmd.Parameters.AddWithValue("@mainseqno", MainSeqNo);

                Source_seqno = Source_seqno != null ? Source_seqno : Convert.ToInt16(DBNull.Value);
                cmd.Parameters.AddWithValue("@source_seqno", Source_seqno);

                Source = (Source != null ? Source : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@source", Source);

                Dtstamp = (Dtstamp != null ? Dtstamp : Convert.ToDateTime(DBNull.Value));
                cmd.Parameters.AddWithValue("@dtstamp", Dtstamp);

                Domname = (Domname != null ? Domname : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@domname", Domname);

                DomPrefix = (DomPrefix != null ? DomPrefix : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@domPrefix", DomPrefix);

                DomSuffix = (DomSuffix != null ? DomSuffix : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@domSuffix", DomSuffix);

                DomLength = (DomLength != null ? DomLength : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@domLength", DomLength);

                Grade = (Grade != null ? Grade : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@grade", Grade);

                Score = (Score != null ? Score : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@score", Score);

                PageRank = (PageRank != null ? PageRank : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@PageRank", PageRank);

                ArchiveLinks = (ArchiveLinks != null ? ArchiveLinks : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@archiveLinks", ArchiveLinks);

                ArchiveOldest = ((ArchiveOldest > DateTime.MinValue) ? ArchiveOldest : DateTime.MinValue);
                if (ArchiveOldest != DateTime.MinValue)
                {
                    cmd.Parameters.AddWithValue("@archiveOldest", ArchiveOldest);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@archiveOldest", DBNull.Value);
                }

                ArchiveKeyword = (ArchiveKeyword != null ? ArchiveKeyword : Convert.ToString(DBNull.Value));
                cmd.Parameters.AddWithValue("@archiveKeyword", ArchiveKeyword);

                QuantcastCtr = (QuantcastCtr != null ? QuantcastCtr : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@QuantcastCtr", QuantcastCtr);

                DMOZ_Catno = (DMOZ_Catno != null ? DMOZ_Catno : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@DMOZ_Catno", DMOZ_Catno);

                DMOZ_Catno = (DMOZ_Catno != null ? DMOZ_Catno : Convert.ToInt32(DBNull.Value));
                cmd.Parameters.AddWithValue("@DMOZ_NumResults", DMOZ_NumResults);

                conn.Open();
                int rtn = cmd.ExecuteNonQuery();

                return rtn == 1;

            }

        }
    }
}
