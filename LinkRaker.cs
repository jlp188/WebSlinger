using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.IO;
using System.Net.Sockets;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebSlinger
{
    class LinkRaker
    {
        public LinkRaker()
        {
            lb_Proxy = true;
        }


        #region LinkRaker_Variables

        public string proxAddr;
        int proxCtr;
        

        public dnengMainRecord dnRec = null;
        public AlexaRecord AlxRec;
        public CompeteRecord CmptRec;
        EstibotRecord EstRec;
        IndexPagesRecord IndxRec;
        WhoIsRecord WhoIsRec;

        bool _lbAlexa;
        public bool lb_Alexa
        {
            get { return _lbAlexa; }
            set { _lbAlexa = value; }
        }

        bool _lbPageRank;
        public bool lb_PageRank
        {
            get { return _lbPageRank; }
            set { _lbPageRank = value; }
        }

        bool _lbArchive;
        public bool lb_Archive
        {
            get { return _lbArchive; }
            set { _lbArchive = value; }
        }

        bool _lbCompete;
        public bool lb_Compete
        {
            get { return _lbCompete; }
            set { _lbCompete = value; }
        }

        bool _lbQuant;
        public bool lb_Quant
        {
            get { return _lbQuant; }
            set { _lbQuant = value; }
        }

        bool _lbDMOZ;
        public bool lb_DMOZ
        {
            get { return _lbDMOZ; }
            set { _lbDMOZ = value; }
        }

        bool _lbWhoIs;
        public bool lb_WhoIs
        {
            get { return _lbWhoIs; }
            set { _lbWhoIs = value; }
        }

        bool _lbEstibot;
        public bool lb_Estibot
        {
            get { return _lbEstibot; }
            set { _lbEstibot = value; }
        }

        bool _lbIndexPages;
        public bool lb_IndexPages
        {
            get { return _lbIndexPages; }
            set { _lbIndexPages = value; }
        }

        bool _lbBackLinks;
        public bool lb_BackLinks
        {
            get { return _lbBackLinks; }
            set { _lbBackLinks = value; }
        }


        bool _lbProxy;
        public bool lb_Proxy
        {
            get { return _lbProxy; }
            set { _lbProxy = value; }
        }

        #endregion


        /// <summary>
        /// Get a Dropped Domain Name from the DropList table
        /// </summary>
        public string GetDDN()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.ConnStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT TOP 1 dropSeqno, drplDomain FROM DropList Where drplFlag = 'false' and drplSuffix = 'com'";
                cmd.Connection = conn;
                string DDN;
                Int16 seqno;
                SqlDataReader SR1;
                try
                {
                    SR1 = cmd.ExecuteReader();
                    SR1.Read();
                    DDN = SR1["drplDomain"].ToString();
                    seqno = Convert.ToInt16(SR1["dropSeqno"]);
                    SR1.Close();
                }
                catch (Exception e1)
                {
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.GetDDN", e1);
                    return null;
                }


                cmd.CommandText = "UPDATE DropList SET drplFlag = 'true' WHERE dropSeqno = @drplSeqno";
                cmd.Parameters.AddWithValue("@drplSeqno", seqno);
                int rtn = cmd.ExecuteNonQuery();
                if (dnRec == null) dnRec = new dnengMainRecord(DDN);
                return DDN;
            }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
                cmd.CommandText = "SELECT TOP 1 proxSeqno, proxIP FROM ProxyList WHERE proxActvFlag = 'true' ORDER BY proxSeqno ASC";
                cmd.Connection = conn;
                string prox;

                SqlDataReader SR1 = cmd.ExecuteReader();
                SR1.Read();
                prox = SR1["proxIP"].ToString();
                SR1.Close();

                //cmd.CommandText = "UPDATE ProxyList SET proxActvFlag = 'false' WHERE proxIP = @proxIP";
                //cmd.Parameters.AddWithValue("@proxIP", prox);
                //int rtn = cmd.ExecuteNonQuery();

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
                cmd.Parameters.AddWithValue("@proxIP", oldProxy);
                cmd.Connection = conn;
                int rtn = cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT TOP 1 proxSeqno, proxIP FROM ProxyList WHERE proxActvFlag = 'true' ORDER BY proxSeqno ASC";
                
                string prox;

                SqlDataReader SR1 = cmd.ExecuteReader();
                SR1.Read();
                prox = SR1["proxIP"].ToString();
                SR1.Close();
                
                return prox;
            }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public StreamReader CapturePage(string domname)
        {
            ErrLogRec ErrLog = new ErrLogRec();
            bool lb_rtn;
            
            Uri myUri = new Uri(domname);
            WebRequest request = WebRequest.Create(myUri);
            WebResponse response;
            if (lb_Proxy)
            {
                WebProxy Proxy = new WebProxy(proxAddr);
                request.Proxy = Proxy;
            }

            request.Timeout = 30000; // 30 sec timeout
            try
            {
                response = request.GetResponse();
            }
            catch (WebException Wex)
            {
                if (Wex.Message == "The remote server returned an error: (404) Not Found.")
                {
                    dnRec.ArchiveKeyword = "";
                    dnRec.ArchiveLinks = 0;
                    lb_rtn = ErrLog.Insert("LinkRaker.CapturePage", Wex);
                    return null;
                }
                else
                {
                    if (Wex.Status == WebExceptionStatus.ReceiveFailure && !lb_Proxy 
                        || Wex.Status == WebExceptionStatus.ServerProtocolViolation && !lb_Proxy
                        || Wex.Status == WebExceptionStatus.ConnectFailure && !lb_Proxy
                        || Wex.Status == WebExceptionStatus.ProtocolError && !lb_Proxy
                        || Wex.Status == WebExceptionStatus.Timeout && !lb_Proxy)
                    {
                        if (proxAddr == null)
                        {
                            proxAddr = GetProxy();
                        }
                        else
                        {
                            proxAddr = GetProxy(proxAddr);
                        }

                        proxCtr++;
                        if (proxCtr != 6)
                        {
                            CapturePage(domname);
                        }
                        else
                        {
                            proxCtr = 0;
                            lb_rtn = ErrLog.Insert("LinkRaker.CapturePage", Wex);
                            return null;
                        }

                    }
                    else
                    {
                        lb_rtn = ErrLog.Insert("LinkRaker.CapturePage", Wex);
                        return null;
                    }
                }
                lb_rtn = ErrLog.Insert("LinkRaker.CapturePage", Wex);
                return null;
            }
            catch (Exception e2)
            {
                if (e2.Message == "Unable to connect to the remote server")
                {
                    if (proxAddr == null)
                    {
                        proxAddr = GetProxy();
                    }
                    else
                    {
                        proxAddr = GetProxy(proxAddr);
                    }

                    proxCtr++;
                    if (proxCtr != 6)
                    {
                        CapturePage(domname);
                    }
                    else
                    {
                        proxCtr = 0;
                        lb_rtn = ErrLog.Insert("LinkRaker.CapturePage", e2);
                        return null;
                    }
                }
                lb_rtn = ErrLog.Insert("LinkRaker.CapturePage", e2);
                return null;
            }
            proxCtr = 0;
            try
            {
                StreamReader strmRdr = new StreamReader(response.GetResponseStream());
                return strmRdr;
            }
            catch (Exception e1)
            {
                lb_rtn = ErrLog.Insert("LinkRaker.CapturePage", e1);
                return null;
            }

        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public string GetPattern(string rsrcCode)
        {
            string pattern = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.ConnStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM ResourceList WHERE rsrcCode = @rsrc";
                cmd.Parameters.AddWithValue("@rsrc", rsrcCode);

                cmd.Connection = conn;

                SqlDataReader SR1 = cmd.ExecuteReader();
                if (SR1.Read()) pattern = SR1["regExpression"].ToString();
                SR1.Close();

                return pattern;
            }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public string GetURL(string rsrcCode)
        {
            string url = "";
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            {
                string cmdStr = "SELECT rsrcURL FROM ResourceList WHERE rsrcCode = @rsrc";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@rsrc", rsrcCode);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    int iSeq = r.GetOrdinal("rsrcURL");
                    while (r.Read())
                    {
                        url = r.IsDBNull(iSeq) ? "NULL" : r.GetString(iSeq);
                    }
                }

                return url;
            }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void RunPageRank(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            int rank = GooglePR.MyPR(DDN);
            dnRec.PageRank = rank;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void RunArchive(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN);

            string url = dnRec.ArcRsrcURL.Replace("(domain)", DDN);
            
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("ARC");
                        string pattern2 = GetPattern("ARC1");
                        string val;
                        DateTime firstDate, lastDate = Convert.ToDateTime("12/31/2012");
                        string source;
                        string totalSrc = "";
                        Match match2;
                        Match match;
                        bool lb_matched = false;

                        while ((source = strmRdr.ReadLine()) != null)
                        {
                            match = Regex.Match(source, pattern, RegexOptions.Singleline);
                            if (match.Success)
                            {
                                val = match.Groups["Date"].ToString();
                                firstDate = Convert.ToDateTime(val);
                                firstDate = (firstDate <= lastDate ? firstDate : lastDate);
                                lastDate = firstDate;
                                dnRec.ArchiveOldest = lastDate;
                            }

                            totalSrc = totalSrc + source;

                            match2 = Regex.Match(totalSrc, pattern2, RegexOptions.Singleline);
                            if (match2.Success && !lb_matched)
                            {
                                val = match2.Groups["Pages"].ToString();
                                dnRec.ArchiveLinks = Convert.ToInt32(val);
                                lb_matched = true;
                            }
                        }
                    }
                }
                catch (Exception e1)
                {
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.RunArchive", e1);
                }
            }

        }

        public void RunQuantcast(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN);

            string url = GetURL("QTC");
            url = url.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("QTC");

                        string val;
                        string totalSrc;

                        totalSrc = strmRdr.ReadToEnd();
                        
                        Match match1;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            int i = 0;
                            string GrpName;
                            foreach (Group grp in match1.Groups)
                            {
                                i++;
                                GrpName = "QuantReach";
                                if (grp.Success)
                                {
                                    //val = grp.Value.ToString();
                                    val = match1.Groups[GrpName].Value.ToString();
                                    dnRec.QuantcastCtr = (val == "N/A") ? 0 : Convert.ToInt32(val);
                                    
                                }
                            }
                        }
                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.RunQuant", e1);

                }
            }
        }

        public void RunDMOZ(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            string url = GetURL("DMZ");
            url = url.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("DMZ");

                        string val;
                        string totalSrc;

                        totalSrc = strmRdr.ReadToEnd();
                        
                        Match match1;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            int i = 0;
                            string GrpName;
                            foreach (Group grp in match1.Groups)
                            {
                                i++;
                                
                                if (grp.Success)
                                {
                                    GrpName = "Grp1";
                                    val = match1.Groups[GrpName].Value.ToString();
                                    dnRec.DMOZ_Catno = Convert.ToInt32(val);

                                    GrpName = "Grp2";
                                    val = match1.Groups[GrpName].Value.ToString();
                                    dnRec.DMOZ_NumResults = Convert.ToInt32(val);
                                }
                            }
                        }
                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.RunDMOZ", e1);

                }
            }
        }


        public void RunAlexa(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN);

            AlxRec = new AlexaRecord();
            AlxRec.Mainseqno = dnRec.MainSeqNo;
            AlxRec.alxDDN = DDN;
            string url = AlxRec.RsrcURL.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("ALX");

                        pattern = "<p>Alexa traffic rank for.*?<th>7 day avg</th><td>(?<Rank7D>.*?)"
                            + "</td>.*?<th>1 month avg</th><td>(?<Rank1M>.*?)</td>.*?<th>3 month avg</th><td>"
                            + "(?<Rank3M>.*?)</td>.*?3 month change</th><td>(?<RankChng>.*?)(?:<img src='/images/arrows/"
                            + "(?<RankDir>.*?).gif|</td>)";

                        string Rank7D, Rank1M, Rank3M, RankChng, RankDir;
                        string Reach7D, Reach1M, Reach3M, ReachChng, ReachDir;
                        string Search7D, Search1M, Search3M, SearchChng, SearchDir;
                        string Country1, Country1percent, Country2, Country2percent;
                        string Country3, Country3percent, totalSrc;


                        totalSrc = strmRdr.ReadToEnd();

                        Match match1, match2, match3, match4;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            Rank7D = match1.Groups["Rank7D"].Value.ToString();
                            AlxRec.AlxRank7D = String.IsNullOrEmpty(Rank7D) ? 0 : (Rank7D.Trim() == "-" ? 0 : Convert.ToDouble(Rank7D));


                            Rank1M = match1.Groups["Rank1M"].Value.ToString();
                            AlxRec.AlxRank1M = String.IsNullOrEmpty(Rank1M) ? 0 : (Rank1M.Trim() == "-" ? 0 : Convert.ToDouble(Rank1M));

                            Rank3M = match1.Groups["Rank3M"].Value.ToString();
                            AlxRec.AlxRank3M = String.IsNullOrEmpty(Rank3M) ? 0 : (Rank3M.Trim() == "-" ? 0 : Convert.ToDouble(Rank3M));

                            RankChng = match1.Groups["RankChng"].Value.ToString();
                            AlxRec.RankChng = String.IsNullOrEmpty(RankChng) ? 0 : (RankChng.Trim() == "-" ? 0 : Convert.ToDouble(RankChng));

                            RankDir = match1.Groups["RankDir"].Value.ToString();
                            AlxRec.RankDir = String.IsNullOrEmpty(RankDir) ? "" : RankDir;

                        }
                        else
                        {
                            AlxRec.AlxRank7D = 0; AlxRec.AlxRank1M = 0; AlxRec.AlxRank3M = 0; AlxRec.RankChng = 0; AlxRec.RankDir = "";
                        }

                        pattern = "Percent of global Internet users who visit.*?<th>7 day avg</th><td>(?<Reach7D>.*?)(?:%|</td>).*?<th>1 month avg</th><td>"
                            + "(?<Reach1M>.*?)(?:%|</td>).*?<th>3 month avg</th><td>(?<Reach3M>.*?)(?:%|</td>).*?3 month change</th><td>(?<ReachChng>.*?)"
                            + "(?:%|</td>).*?(?:(?:<img src='/images/arrows/(?<ReachDir>.*?).gif|</table>))";

                        match2 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match2.Success)
                        {
                            Reach7D = match2.Groups["Reach7D"].Value.ToString();
                            AlxRec.AlxReach7D = String.IsNullOrEmpty(Reach7D) ? 0 : (Reach7D.Trim() == "-" ? 0 : Convert.ToDouble(Reach7D));

                            Reach1M = match2.Groups["Reach1M"].Value.ToString();
                            AlxRec.AlxReach1M = String.IsNullOrEmpty(Reach1M) ? 0 : (Reach1M.Trim() == "-" ? 0 : Convert.ToDouble(Reach1M));

                            Reach3M = match2.Groups["Reach3M"].Value.ToString();
                            AlxRec.AlxReach3M = String.IsNullOrEmpty(Reach3M) ? 0 : (Reach3M.Trim() == "-" ? 0 : Convert.ToDouble(Reach3M));

                            ReachChng = match2.Groups["ReachChng"].Value.ToString();
                            AlxRec.ReachChng = String.IsNullOrEmpty(ReachChng) ? 0 : (ReachChng.Trim() == "-" ? 0 : Convert.ToDouble(ReachChng));

                            ReachDir = match2.Groups["ReachDir"].Value.ToString();
                            AlxRec.ReachDir = String.IsNullOrEmpty(ReachDir) ? "" : ReachDir;
                        }
                        else
                        {
                            AlxRec.AlxReach7D = 0; AlxRec.AlxReach1M = 0; AlxRec.AlxReach3M = 0; AlxRec.ReachChng = 0; AlxRec.ReachDir = "";
                        }

                        pattern = "came from a search engine.*?<th>7 day avg</th><td>(?<Search7D>.*?)(?:%|</td>).*?<th>1 month avg</th><td>(?<Search1M>.*?)"
                            + "(?:%|</td>).*?<th>3 month avg</th><td>(?<Search3M>.*?)(?:%|</td>).*?3 month change</th><td>(?<SearchChng>.*?)(?:%|</td>).*?"
                            + "(?:(?:<img src='/images/arrows/(?<SearchDir>.*?).gif|</table>))";

                        match3 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match3.Success)
                        {
                            Search7D = match3.Groups["Search7D"].Value.ToString();
                            AlxRec.AlxSearch7D = String.IsNullOrEmpty(Search7D) ? 0 : (Search7D.Trim() == "-" ? 0 : Convert.ToDouble(Search7D));

                            Search1M = match3.Groups["Search1M"].Value.ToString();
                            AlxRec.AlxSearch1M = String.IsNullOrEmpty(Search1M) ? 0 : (Search1M.Trim() == "-" ? 0 : Convert.ToDouble(Search1M));

                            Search3M = match3.Groups["Search3M"].Value.ToString();
                            AlxRec.AlxSearch3M = String.IsNullOrEmpty(Search3M) ? 0 : (Search3M.Trim() == "-" ? 0 : Convert.ToDouble(Search3M));

                            SearchChng = match3.Groups["SearchChng"].Value.ToString();
                            AlxRec.SearchChng = SearchChng.Contains("-") ? 0 : Convert.ToDouble(SearchChng);

                            SearchDir = match3.Groups["SearchDir"].Value.ToString();
                            AlxRec.SearchDir = String.IsNullOrEmpty(SearchDir) ? "" : SearchDir;
                        }
                        else
                        {
                            AlxRec.AlxSearch7D = 0; AlxRec.AlxSearch1M = 0; AlxRec.AlxSearch3M = 0; AlxRec.SearchChng = 0; AlxRec.SearchDir = "";
                        }

                        pattern = "come from these countries:.*?<span class=\"geo_number descbold\">(?<Country1percent>.*?)(?:%|</span>).*?<a href=\"/topsites/countries/.*?>"
                            + "(?<Country1>.*?)\\s*?</a>.*?<span class=\"geo_number descbold\">(?<Country2percent>.*?)(?:%|</span>).*?<a href=\"/topsites/countries/.*?>"
                            + "(?<Country2>.*?)\\s*?</a>.*?<span class=\"geo_number descbold\">(?<Country3percent>.*?)(?:%|</span>).*?<a href=\"/topsites/countries/.*?>(?<Country3>.*?)\\s*?</a>";

                        match4 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match4.Success)
                        {

                            Country1 = match4.Groups["Country1"].Value.ToString();
                            AlxRec.AlxCountry1 = String.IsNullOrEmpty(Country1) ? "" : Country1;

                            Country1percent = match4.Groups["Country1percent"].Value.ToString();
                            AlxRec.AlxCntry1_per = String.IsNullOrEmpty(Country1percent) ? 0 : (Convert.ToDouble(Country1percent));

                            Country2 = match4.Groups["Country2"].Value.ToString();
                            AlxRec.AlxCountry2 = String.IsNullOrEmpty(Country2) ? "" : Country2;

                            Country2percent = match4.Groups["Country2percent"].Value.ToString();
                            AlxRec.AlxCntry2_per = String.IsNullOrEmpty(Country2percent) ? 0 : (Convert.ToDouble(Country2percent));

                            Country3 = match4.Groups["Country3"].Value.ToString();
                            AlxRec.AlxCountry3 = String.IsNullOrEmpty(Country3) ? "" : Country3;

                            Country3percent = match4.Groups["Country3percent"].Value.ToString();
                            AlxRec.AlxCntry3_per = String.IsNullOrEmpty(Country3percent) ? 0 : (Convert.ToDouble(Country3percent));

                        }
                        else
                        {
                            AlxRec.AlxCountry1 = ""; AlxRec.AlxCntry1_per = 0; AlxRec.AlxCountry2 = ""; AlxRec.AlxCntry2_per = 0; AlxRec.AlxCountry3 = ""; AlxRec.AlxCntry3_per = 0;
                        }

                        AlxRec.AlxScore = 0;

                        AlxRec.AlxHighRank = Math.Max(Math.Max(AlxRec.AlxRank1M, AlxRec.AlxRank7D), AlxRec.AlxRank3M);
                        AlxRec.AlxHighReach = Math.Max(Math.Max(AlxRec.AlxReach1M, AlxRec.AlxReach7D), AlxRec.AlxReach3M);
                        AlxRec.AlxHighSearch = Math.Max(Math.Max(AlxRec.AlxSearch1M, AlxRec.AlxSearch7D), AlxRec.AlxSearch3M);

                        bool lb_rtn = AlxRec.Insert();

                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.RunAlexa", e1);
                }
            }
            else
            {
                AlxRec.AlxRank7D = 0; AlxRec.AlxRank1M = 0; AlxRec.AlxRank3M = 0; AlxRec.RankChng = 0; AlxRec.RankDir = "";
                AlxRec.AlxReach7D = 0; AlxRec.AlxReach1M = 0; AlxRec.AlxReach3M = 0; AlxRec.ReachChng = 0; AlxRec.ReachDir = "";
                AlxRec.AlxSearch7D = 0; AlxRec.AlxSearch1M = 0; AlxRec.AlxSearch3M = 0; AlxRec.SearchChng = 0; AlxRec.SearchDir = "";
                AlxRec.AlxCountry1 = ""; AlxRec.AlxCntry1_per = 0; AlxRec.AlxCountry2 = ""; AlxRec.AlxCntry2_per = 0; AlxRec.AlxCountry3 = ""; AlxRec.AlxCntry3_per = 0;
            }
            
            //AlxRec.Clear();
            
        }

        public void RunCompete(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            CmptRec = new CompeteRecord();
            CmptRec.Mainseqno = dnRec.MainSeqNo;
            string url = CmptRec.RsrcURL.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("CMP");

                        string mnth, yr, rank, cnt;
                        string totalSrc;

                        totalSrc = strmRdr.ReadToEnd();

                        Match match1;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            CmptRec.CmptDomname = DDN;
                            mnth = match1.Groups["Month"].Value.ToString();

                            yr = match1.Groups["Year"].Value.ToString();
                            DateTime dt = Convert.ToDateTime(mnth + "/" + "01/" + yr);
                            CmptRec.LastUpdate = dt;

                            rank = match1.Groups["Rank"].Value.ToString();
                            CmptRec.CmptMonth1 = Convert.ToInt32(rank);

                            cnt = match1.Groups["Count"].Value.ToString();
                            CmptRec.CmptNumVisits = Convert.ToInt32(cnt);

                            bool lb_rtn = CmptRec.Insert();

                        }

                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.RunCompete", e1);
                }
            }
            
            //CmptRec.Clear();
            
        }
                
        public void RunWhoIs(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            WhoIsRec = new WhoIsRecord();
            WhoIsRec.Mainseqno = dnRec.MainSeqNo;
            WhoIsRec.WhoIsDDN = DDN;
            string DN;
            string[] ar = {"com","net","org","info"};
            foreach (string a in ar)
            {
                DN = DDN.Substring(0, (DDN.IndexOf(".")));
                DN = DN + "." + a;

                string pattern = GetPattern("WHO");
                //if (a != "com") pattern = "No match for";
                string val;
                string ls_rtn = WhoisLookup(DN, "whois.internic.net");
                if (ls_rtn != null)
                {
                    Match match1;
                    match1 = Regex.Match(ls_rtn, pattern, RegexOptions.Singleline);
                    if (match1.Success)
                    {
                        if (a == "com")
                        {
                            int i = 0;
                            string GrpName;
                            foreach (Group grp in match1.Groups)
                            {
                                i++;

                                if (grp.Success)
                                {
                                    GrpName = "NameServer";
                                    val = match1.Groups[GrpName].Value.ToString();
                                    WhoIsRec.NameServer = val;
                                    WhoIsRec.COMexist = true;

                                    GrpName = "CreateDate";
                                    val = match1.Groups[GrpName].Value.ToString();
                                    WhoIsRec.CreateDt = Convert.ToDateTime(val);
                                }
                            }
                        }
                        else
                        {
                            switch (a)
                            {
                                case "net":
                                    WhoIsRec.NETexist = true;
                                    break;
                                case "org":
                                    WhoIsRec.ORGExist = true;
                                    break;
                                case "info":
                                    WhoIsRec.INFOExist = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (a)
                        {
                            case "com":
                                WhoIsRec.COMexist = false;
                                break;
                            case "net":
                                WhoIsRec.NETexist = false;
                                break;
                            case "org":
                                WhoIsRec.ORGExist = false;
                                break;
                            case "info":
                                WhoIsRec.INFOExist = false;
                                break;
                            default:
                                break;
                        }

                    }

                }
            }

            bool lb_rtn = WhoIsRec.Insert();
        }

        public string WhoisLookup(string siteUrl,string whoisUrl)
        {
            string response = string.Empty;
            string whoisData = string.Empty;

            try
            {
                //initiaze new TcpClient with url of the lookup
                //and default port of 43
                TcpClient tcp = new TcpClient(whoisUrl, 43);

                //get the response
                NetworkStream nStream = tcp.GetStream();

                //Buffered stream for writing onto the NetworkStream
                BufferedStream bStream = new BufferedStream(nStream);

                //now we need a StreamWriter for writing on to the stream
                StreamWriter writer = new StreamWriter(bStream);

                //send the name of the domain we wish to look up
                writer.WriteLine(siteUrl);
                writer.Flush();

                try
                {
                    //now we need a reader for reading the response stream
                    StreamReader reader = new StreamReader(bStream);

                    while ((response = reader.ReadLine()) != null)
                    {
                        //build the response
                        whoisData += response;
                        whoisData += "\r\n";
                    }

                    return whoisData;
                }
                catch (Exception ex)
                {
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.WhoIsLookup", ex); 
                    return "Error reading stream: " + ex.ToString();
                }

            }
            catch (Exception ex)
            {
                ErrLogRec ErrLog = new ErrLogRec();
                bool lb_rtn = ErrLog.Insert("LinkRaker.WhoIsLookup", ex); 
                return "Error connecting to client: " + ex.ToString();
            }

        }

        public void RunEstibot(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            EstRec = new EstibotRecord();
            EstRec.Mainseqno = dnRec.MainSeqNo;
            EstRec.estDDN = DDN;
            string url = EstRec.RsrcURL.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("EST");

                        string Traffic, PPCads, MaxPPC, Overture, WorkTracker, TotalSearches;
                        string totalSrc;

                        totalSrc = strmRdr.ReadToEnd();

                        Match match1;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            Traffic = match1.Groups["Traffic"].Value.ToString();
                            EstRec.Traffic = (Traffic == null || Traffic == "") ? 0 : Convert.ToInt32(Traffic);

                            PPCads = match1.Groups["PPCads"].Value.ToString();
                            EstRec.PpcAds = (PPCads == null || PPCads == "") ? 0 : Convert.ToInt32(PPCads);

                            MaxPPC = match1.Groups["MaxPPC"].Value.ToString();
                            EstRec.MaxPPCamount = (MaxPPC == null || MaxPPC == "") ? 0 : Convert.ToDecimal(MaxPPC);

                            Overture = match1.Groups["Overture"].Value.ToString();
                            EstRec.OvertureNum = (Overture == null || Overture == "") ? 0 : Convert.ToInt32(Overture);

                            WorkTracker = match1.Groups["WordTracker"].Value.ToString();
                            EstRec.Worktracker = (WorkTracker == null || WorkTracker == "") ? 0 : Convert.ToInt32(WorkTracker);

                            TotalSearches = match1.Groups["TotalSearches"].Value.ToString();
                            EstRec.NumSearches = (TotalSearches == null || TotalSearches == "") ? 0 : Convert.ToInt32(TotalSearches);

                            bool lb_rtn = EstRec.Insert();
                            
                        }

                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.RunEstibot", e1);
                    
                }
            }
            
            //EstRec.Clear();
            
        }

        public void RunIndexPages(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            IndxRec = new IndexPagesRecord();
            
            NdxYahoo(DDN);
            NdxGoogle(DDN);
            NdxAV(DDN);
            IndxRec.Clear();
        }

        public void NdxGoogle(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            IndxRec.Mainseqno = dnRec.MainSeqNo;
            IndxRec.IdxDDN = DDN;
            string url = IndxRec.GrsrcURL.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("NDXG");

                        string Pages;
                        string totalSrc;

                        totalSrc = strmRdr.ReadToEnd();

                        Match match1;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            Pages = match1.Groups["Pages"].Value.ToString();
                            IndxRec.IdxCount = Pages.Contains("-") ? 0 : Convert.ToDouble(Pages);
                            IndxRec.IdxSource = "GL";
                            IndxRec.IdxDtstamp = DateTime.Now;

                            bool lb_rtn = IndxRec.Insert();
                        }

                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.NdxGoogle", e1);

                }
            }

            IndxRec.Clear();
        }

        public void NdxYahoo(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            IndxRec.Mainseqno = dnRec.MainSeqNo;
            IndxRec.IdxDDN = DDN;
            string url = IndxRec.YrsrcURL.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("NDXY");

                        string Pages;
                        string totalSrc;

                        totalSrc = strmRdr.ReadToEnd();

                        Match match1;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            Pages = match1.Groups["Pages"].Value.ToString();
                            IndxRec.IdxCount = Pages.Contains("-") ? 0 : Convert.ToDouble(Pages);
                            IndxRec.IdxSource = "YH";
                            IndxRec.IdxDtstamp = DateTime.Now;
                            
                            bool lb_rtn = IndxRec.Insert();
                        }

                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.NdxYahoo", e1);

                }
            }

            IndxRec.Clear();
        }

        public void NdxAV(string DDN)
        {
            if (dnRec == null) dnRec = new dnengMainRecord(DDN); 
            
            IndxRec.Mainseqno = dnRec.MainSeqNo;
            IndxRec.IdxDDN = DDN;
            string url = IndxRec.AVrsrcURL.Replace("(domain)", DDN);
            StreamReader strmRdr = CapturePage(url);

            if (strmRdr != null)
            {
                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    using (strmRdr)
                    {
                        string pattern = GetPattern("NDXAV");

                        string Pages;
                        string totalSrc;

                        totalSrc = strmRdr.ReadToEnd();

                        Match match1;
                        match1 = Regex.Match(totalSrc, pattern, RegexOptions.Singleline);
                        if (match1.Success)
                        {
                            Pages = match1.Groups["Pages"].Value.ToString();
                            IndxRec.IdxCount = Pages.Contains("-") ? 0 : Convert.ToDouble(Pages);
                            IndxRec.IdxSource = "AV";
                            IndxRec.IdxDtstamp = DateTime.Now;

                            bool lb_rtn = IndxRec.Insert();
                        }

                    }
                }
                catch (Exception e1)
                {
                    //// Let the user know what went wrong.
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("LinkRaker.NdxAV", e1);

                }
            }

            IndxRec.Clear();
        }

        public void RunBackLinks(string DDN)
        {
            StreamReader strmRdr = CapturePage(DDN);
        }


        

        public void RunResources()
        {
            string DDName;
            bool lb_rtn = false;

            if (lb_Proxy) proxAddr = GetProxy();

            DDName = GetDDN();

            while (DDName != null)
            {
                if (_lbPageRank) RunPageRank(DDName);
                if (_lbArchive) RunArchive(DDName);
                if (_lbQuant) RunQuantcast(DDName);
                if (_lbDMOZ) RunDMOZ(DDName);
                if (_lbPageRank || _lbArchive || _lbQuant || _lbDMOZ) lb_rtn = dnRec.Update();

                if (_lbAlexa) RunAlexa(DDName);
                
                if (_lbCompete) RunCompete(DDName);
                if (_lbWhoIs) RunWhoIs(DDName);
                if (_lbEstibot) RunEstibot(DDName);
                if (_lbIndexPages) RunIndexPages(DDName);
                if (_lbBackLinks) RunBackLinks(DDName);


                DDName = GetDDN();
            }



        }


    }
}
