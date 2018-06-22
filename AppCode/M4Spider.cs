using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
//using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace WebSlinger
{
    class M4Spider
    {
        public M4Spider()
        {
            _maxOutboundLinks = 99;
            _numOutboundLinks = 0;
            _avoidHttps = true;
            _insideLinks = false;
            _verifyBackLinks = true;
            _unSpidered = new List<string>();
            _spideredURLs = new List<string>();
            _obLinks = new List<string>();
            _avoidPatterns = new List<string>();
            _proxyDomain = "";
            _proxyLogin = "";
            _proxyPassword = "";
            _proxyPort = 0;
            _backLinkDN = "";
            SR = new SpiderResults();
            PrxShut = new ProxyShuttler();
            
            


            AvoidPatterns.Add("google.");
            AvoidPatterns.Add("yahoo.");
            AvoidPatterns.Add("bing.");
            AvoidPatterns.Add("altavista.");
            AvoidPatterns.Add("princeton.edu");
            AvoidPatterns.Add("amazon.com");
            AvoidPatterns.Add("baidu.com");
            AvoidPatterns.Add("planet-lab.edu");
            
            
        }

        #region Properties

        SpiderResults SR;
        ProxyShuttler PrxShut;

        private int proxCtr = 0;

        private SpiderResults _sr1;
        public SpiderResults SR1
        {
            get { return _sr1; }
            //set { _sr1 = value; }
        }

        private List<string> _unSpidered;
        public List<string> UnSpidered
        {
            get { return _unSpidered; }
            set { _unSpidered.Add(value.ToString()); }
        }

        private List<string> _spideredURLs;
        public List<string> SpideredURLs
        {
            get { return _spideredURLs; }
            set { _spideredURLs.Add(value.ToString()); }
        }

        private List<string> _obLinks;
        public List<string> ObLinks
        {
            get { return _obLinks; }
            set { _obLinks.Add(value.ToString()); }
        }

        private List<string> _avoidPatterns;
        public List<string> AvoidPatterns
        {
            get { return _avoidPatterns; }
            set { _avoidPatterns.Add(value.ToString()); }
        }

        private bool _avoidHttps;
        public bool AvoidHttps
        {
            get { return _avoidHttps; }
            set { _avoidHttps = value; }
        }

        private string _backLinkDN;
        public string BackLinkDN
        {
            get { return _backLinkDN; }
            set { _backLinkDN = value; }
        }

        private bool _insideLinks;
        public bool InsideLinks
        {
            get { return _insideLinks; }
            set { _insideLinks = value; }
        }

        private bool _verifyBackLinks;
        public bool VerifyBackLinks
        {
            get { return _verifyBackLinks; }
            set { _verifyBackLinks = value; }
        }

        private bool _chopAtQuery;
        public bool ChopAtQuery
        {
            get { return _chopAtQuery; }
            set { _chopAtQuery = value; }
        }

        private int _connectTimeout;
        public int ConnectTimeout
        {
            get { return _connectTimeout; }
            set { _connectTimeout = value; }
        }

        private string _currentURL;
        public string CurrentURL
        {
            get { return _currentURL; }
            set { _currentURL = value; }
        }

        private string _errMsg;
        public string ErrMsg
        {
            get { return _errMsg; }
            set { _errMsg = value; }
        }

        private string _lastHtml;
        public string LastHtml
        {
            get { return _lastHtml; }
        }

        private string _lastHtmlKeywords;
        public string LastHtmlKeywords
        {
            get { return _lastHtmlKeywords; }
        }

        private string _lastHtmlDescription;
        public string LastHtmlDescription
        {
            get { return _lastHtmlDescription; }
        }

        private string _lastUrl;
        public string LastUrl
        {
            get { return _lastUrl; }
        }

        private int _numFailed;
        public int NumFailed
        {
            get { return _numFailed; }
        }

        private int _numOutboundLinks;
        public int NumOutboundLinks
        {
            get { return _numOutboundLinks; }
        }

        private int _numSpidered;
        public int NumSpidered
        {
            get { return _numSpidered; }
        }

        private int _numUnspidered;
        public int NumUnspidered
        {
            get { return _numUnspidered; }
        }

        private string _proxyDomain;
        public string ProxyDomain
        {
            get { return _proxyDomain; }
            set { _proxyDomain = value; }
        }

        private string _proxyLogin;
        public string ProxyLogin
        {
            get { return _proxyLogin; }
            set { _proxyLogin = value; }
        }

        private string _proxyPassword;
        public string ProxyPassword
        {
            get { return _proxyPassword; }
            set { _proxyPassword = value; }
        }

        private int _proxyPort;
        public int ProxyPort
        {
            get { return _proxyPort; }
            set { _proxyPort = value; }
        }

        private int _readTimeout;
        public int ReadTimeout
        {
            get { return _readTimeout; }
            set { _readTimeout = value; }
        }

        private int _maxOutboundLinks;
        public int MaxOutboundLinks
        {
            get { return _maxOutboundLinks; }
            set { _maxOutboundLinks = value; }
        }

        private int _windDownCount;
        public int WindDownCount
        {
            get { return _windDownCount; }
            set { _windDownCount = value; }
        }

        #endregion



        #region Methods

        public void AddAvoidOutboundLinkPattern(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public void AddAvoidPattern(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public void AddMustMatchPattern(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public void AddUnspidered(string url)
        {
            _unSpidered.Add(url);
            _numUnspidered++;
        }

        public void ClearFailedUrls()
        {
            _numFailed = 0;
        }

        public void ClearOutboundLinks()
        {
            _numOutboundLinks = 0;
        }

        public void ClearSpideredUrls()
        {
            _numSpidered = 0;
        }

        public bool CrawlNext()
        {
            _currentURL = _unSpidered[_unSpidered.Count - 1].ToString();
            DateTime startTime = DateTime.Now;
            StreamReader strmrdr = CapturePage(_currentURL);
            DateTime stopTime = DateTime.Now;
            TimeSpan timeDif = stopTime - startTime;
            float procTime = (float)timeDif.TotalSeconds;
            bool blrtn = CycleTime("CapturePage", _currentURL, procTime);
            if (strmrdr != null)
            {
                startTime = DateTime.Now;
                ParsePage(strmrdr, _currentURL);
                stopTime = DateTime.Now;
                timeDif = stopTime - startTime;
                procTime = (float)timeDif.TotalSeconds;
                blrtn = CycleTime("Total ParsePage", BackLinkDN, procTime);
                if (null != strmrdr) strmrdr.Close();
                return true;
            }
            else
            {
                _errMsg = "Could not capture the url source content.";
                return false;
            }
            
        }

        public string GetAvoidPattern()
        {
            throw new System.NotImplementedException();
        }

        public string GetFailedUrl()
        {
            throw new System.NotImplementedException();
        }

        public string GetOutboundLink()
        {
            throw new System.NotImplementedException();
        }

        public string GetSpideredUrl()
        {
            throw new System.NotImplementedException();
        }

        public string GetUnspideredUrl()
        {
            return _unSpidered[_unSpidered.Count - 1].ToString();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public bool RecrawlLast()
        {
            StreamReader strmrdr = CapturePage(_currentURL);
            if (strmrdr != null)
            {
                ParsePage(strmrdr, _currentURL);
                if (null != strmrdr) strmrdr.Close();
                return true;
            }
            else
            {
                _errMsg = "Could not capture the url source content.";
                return false;
            }
        }

        public void SkipUnspidered()
        {
            throw new System.NotImplementedException();
        }

        public void SleepMs(int millisecs)
        {
            throw new System.NotImplementedException();
        }

        private StreamReader CapturePage(string domname)
        {
            Uri myUri;
            try
            {
                myUri = new Uri(domname);
            }
            catch (Exception URIex)
            {
                _errMsg = URIex.Message;
                _numFailed++;
                return null;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myUri);
            HttpWebResponse response;

            if ((_proxyDomain == null) || (_proxyDomain.Trim() == ""))
            {
                _proxyDomain = GetProxy();
            }
            WebProxy Proxy = new WebProxy(_proxyDomain);
            request.Proxy = Proxy;
            request.Timeout = 30000; // 30 sec timeout
            
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                StreamReader strmRdr = new StreamReader(response.GetResponseStream(),Encoding.ASCII);
                return strmRdr;
            }
            catch (WebException Wex)
            {
                if (Wex.Status == WebExceptionStatus.ReceiveFailure || Wex.Status == WebExceptionStatus.ConnectFailure
                    || Wex.Status == WebExceptionStatus.ServerProtocolViolation || Wex.Status == WebExceptionStatus.Timeout
                     || Wex.Status == WebExceptionStatus.ProtocolError || Wex.Status == WebExceptionStatus.ProxyNameResolutionFailure)
                {
                    if (_proxyDomain == null)
                    {
                        _proxyDomain = GetProxy();
                    }
                    else
                    {
                        _proxyDomain = GetProxy(_proxyDomain);
                    }

                    proxCtr++;
                    if (proxCtr != 6)
                    {
                        StreamReader strmRdr1 = CapturePage(domname);
                        return strmRdr1;
                    }
                    else
                    {
                        proxCtr = 0;
                        return null;
                    }
                }
                else
                {
                    _errMsg = Wex.Message;
                    _numFailed++;
                    return null;
                }
            }
            catch (Exception e2)
            {
                _errMsg = e2.Message;
                _numFailed++;
                return null;
            }
        }

        public string GetProxy()
        {
            //using (SqlConnection conn = new SqlConnection())
            //{
                //conn.ConnectionString = Properties.Settings.Default.ConnStr;
                //conn.Open();
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "SELECT TOP 1 proxSeqno, proxIP FROM ProxyList WHERE proxActvFlag = 'true' order by proxSeqno ASC";
                //cmd.Connection = conn;
                //string prox;

                //SqlDataReader SRdr1 = cmd.ExecuteReader();
                //SRdr1.Read();
                //prox = SRdr1["proxIP"].ToString();
                //SRdr1.Close();

                //cmd.CommandText = "UPDATE ProxyList SET proxActvFlag = 'false' WHERE proxIP = @proxIP";
                //cmd.Parameters.AddWithValue("@proxIP", prox);
                //int rtn = cmd.ExecuteNonQuery();

                string prox = PrxShut.GetProxy();

                return prox;
            //}
        }

        public string GetProxy(string oldProx)
        {
            //using (SqlConnection conn = new SqlConnection())
            //{
                //conn.ConnectionString = Properties.Settings.Default.ConnStr;
                //conn.Open();
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "UPDATE ProxyList SET proxActvFlag = 'false' WHERE proxIP = @proxIP";
                //cmd.Parameters.AddWithValue("@proxIP", oldProx);
                //cmd.Connection = conn;
                //int rtn = cmd.ExecuteNonQuery();

                //cmd.CommandText = "SELECT TOP 1 proxSeqno, proxIP FROM ProxyList WHERE proxActvFlag = 'true' order by proxSeqno ASC";
                
                //string prox;

                //SqlDataReader SRdr1 = cmd.ExecuteReader();
                //SRdr1.Read();
                //prox = SRdr1["proxIP"].ToString();
                //SRdr1.Close();

                string prox = PrxShut.GetProxy(oldProx);
                
                return prox;
            //}
        }

        private bool CycleTime(string location, string DN, float execTime)
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnStr))
            
            {
                int rtn;
                //conn.ConnectionString = Properties.Settings.Default.ConnStr;
                
                //SqlCommand cmd = new SqlCommand();
                string cmdStr = "INSERT INTO CycleTimes (domname, location, execTime) VALUES (@domname, @location, @execTime)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                
                //cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@domname", DN);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@execTime", execTime);
                try
                {
                    conn.Open();
                    rtn = cmd.ExecuteNonQuery();
                    return rtn == 1;
                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    return false;
                }

                //return false;
                
            }
        }


        private bool ParsePage(StreamReader strmRdr, string DN)
        {
            SR.Init(_backLinkDN);
            if (strmRdr != null)
            {
                Uri thisUri;
                try
                {
                    thisUri = new Uri(DN);
                }
                catch (Exception URIex)
                {
                    _errMsg = URIex.Message;
                    return false;
                }

                SR.Host = thisUri.Host;
                SR.Path = thisUri.PathAndQuery;
                SR.Port = thisUri.Port.ToString();
                SR.Protocol = thisUri.Scheme;
                SR.DDN = _backLinkDN;

                try
                {
                    // Create an instance of StreamReader to read from CapturePage().
                    // The using statement also closes the StreamReader.
                    _errMsg = ""; 
                    
                    using (strmRdr)
                    {
                        string HypLinkPattern = "href=\"(?<Link>http:.*?)\"";
                        string sHypLinkPattern = "href=\"(?<Link>https?:.*?)\"";
                        string HostPattern = @"^[a-z][a-z0-9+\-.]*://([a-z0-9\-._~%!$&'()*+,;=]+@)?(?<host>[a-z0-9\-._~%]+|\[[a-z0-9\-._~%!$&'()*+,;=:]+\])";
                        string VerifyPattern = @"^[a-z][a-z0-9+\-.]*://([a-z0-9\-._~%!$&'()*+,;=]+@)?(?<host>.*(domain))";
                        
                        string KeywordPattern = "<meta name=\"keywords\" content=\"(?<keywords>.*?)\">";
                        string DescriptionPattern = "<meta name=\"description\" content=\"(?<description>.*?)\">";
                        string val;

                        string source, DN_host = "";
                        bool lb_avoid = false;

                        string totalSrc = "";
                        MatchCollection HypLinkMatch, KeywordMatch, DescripMatch;
                        Match DNmatch, hlDNmatch, DNmatch2;

                        //totalSrc = strmRdr.ReadToEnd();

                        //while ((source = strmRdr.ReadToEnd()) != null)//strmRdr.ReadLine()) != null)
                        if ((source = strmRdr.ReadToEnd()) != null)
                        {
                            if (null != strmRdr) strmRdr.Close();
                            _errMsg = ""; 
                            KeywordMatch = Regex.Matches(source, KeywordPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            if (KeywordMatch.Count > 0)//.Success)
                            {
                                foreach (Match m1 in KeywordMatch)
                                {
                                    val = m1.Groups["keywords"].ToString();
                                    if (val != null)
                                    {
                                        SR.Keywords = val.ToString();
                                    }
                                }
                            }

                            DescripMatch = Regex.Matches(source, DescriptionPattern, RegexOptions.Singleline);
                            if (DescripMatch.Count > 0)//.Success)
                            {
                                foreach (Match m2 in DescripMatch)
                                {
                                    val = m2.Groups["description"].ToString();
                                    if (val != null)
                                    {
                                        SR.Description = val.ToString();
                                    }
                                }
                            }

                            if (!_avoidHttps) HypLinkPattern = sHypLinkPattern;



                            HypLinkMatch = Regex.Matches(source, HypLinkPattern, RegexOptions.Singleline);
                            if (HypLinkMatch.Count > 0)//.Success)
                            {
                                DateTime startTime = DateTime.Now;
                                
                                foreach (Match m in HypLinkMatch)
                                {
                                    if (_numOutboundLinks >= _maxOutboundLinks)
                                    {
                                        _sr1 = SR;
                                        _numUnspidered--;
                                        _numSpidered++;
                                        _errMsg = "";
                                        return true;
                                    }
                                    
                                    val = m.Groups["Link"].ToString();

                                    if (val != null)
                                    {
                                        foreach (string item in AvoidPatterns)
                                        {
                                            lb_avoid = false;
                                            if (val.ToLower().Contains(item.ToLower()))
                                            {
                                                lb_avoid = true;
                                                break;
                                            }
                                        }

                                        if (!lb_avoid)
                                        {
                                            hlDNmatch = Regex.Match(val, HostPattern);
                                            if (hlDNmatch.Success) DN_host = hlDNmatch.Groups["host"].ToString();

                                            if (DN_host != null && DN_host.Trim() != "")
                                            {
                                                if (!_insideLinks)
                                                {
                                                    DNmatch = Regex.Match(val, BackLinkDN);
                                                    if (!DNmatch.Success)
                                                    {
                                                        if (_verifyBackLinks) // verify the linked page has the domain linked back
                                                        {
                                                            StreamReader strmrdr2 = CapturePage(val);
                                                            if (strmrdr2 != null)
                                                            {
                                                                try
                                                                {
                                                                    using (strmrdr2)
                                                                    {
                                                                        totalSrc = strmrdr2.ReadToEnd();
                                                                        strmrdr2.Close();
                                                                        VerifyPattern = VerifyPattern.Replace("(domain)", BackLinkDN);
                                                                        DNmatch2 = Regex.Match(totalSrc, VerifyPattern, RegexOptions.IgnoreCase);// | RegexOptions.Singleline);
                                                                        if (!DNmatch2.Success)
                                                                        {
                                                                            //foreach (string item in AvoidPatterns)
                                                                            //{
                                                                            //    if (val.ToLower().Contains(item.ToLower())) 
                                                                            //    {
                                                                            //        lb_avoid = true;
                                                                            //        break;
                                                                            //    }
                                                                            //}

                                                                            //if (!lb_avoid)
                                                                            //{
                                                                            SR.HyperLinks.Add(val.ToString());
                                                                            _numOutboundLinks++;
                                                                            _errMsg = "";
                                                                            //}
                                                                            //lb_avoid = false;
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception e3)
                                                                {
                                                                    _errMsg = e3.Message;
                                                                    _numFailed++;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _numFailed++;
                                                            }
                                                        }
                                                        else //then just add the link
                                                        {
                                                            SR.HyperLinks.Add(val.ToString());
                                                            _numOutboundLinks++;
                                                            _errMsg = "";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                DateTime stopTime = DateTime.Now;
                                TimeSpan timeDif = stopTime - startTime;
                                float procTime = (float)timeDif.TotalSeconds;
                                bool blrtn = CycleTime("Backlinks in ParsePage", BackLinkDN, procTime);



                            }

                            if (_numOutboundLinks >= _maxOutboundLinks)
                            {
                                _sr1 = SR;
                                _numUnspidered--;
                                _numSpidered++;
                                _errMsg = "";
                                return true;
                            }
                        }
                    }

                    _sr1 = SR;
                    _numUnspidered--;
                    _numSpidered++;
                    _errMsg = "";
                    return true;
                }
                catch (Exception e1)
                {
                    _errMsg = e1.Message;
                    _numFailed++;
                    return false;
                }
            }
            else
            {
                _numFailed++;
                return false;
            }

        }

        #endregion

    }
}
