using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace WebSlinger
{
    /// <summary>
    /// Allows to make a whois query
    /// </summary>
    class WhoIsRequest
    {
        private string _url, 
                       _domain, 
                       _tld, 
                       _targetServer, 
                       _defaultTargetServer = "whois.arin.net"; // Can be modified;

        /// <summary>
        /// Url to query
        /// </summary>
        public string Url { get { return _url; } }

        /// <summary>
        /// Get or set the default server to query, if no one has returned any result
        /// </summary>
        public string DefaultTargetServer 
            { get { return _defaultTargetServer; } 
            set { _defaultTargetServer = value; } }

        /// <summary>
        /// Fired when a response is received from the server
        /// </summary>
        public event EventHandler<WhoisEventArgs> ResponseReceived;

        /// <summary>
        /// Fired when connection closed
        /// </summary>
        public event EventHandler ConnectionClosed;

        /// <summary>
        /// Initialize a new instance of the class
        /// </summary>
        /// <param name="url"></param>
        public WhoIsRequest(string url)
        {
            _url = url;

            Uri u = null;

            try
            {
                u = new Uri(url);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (u != null)
            {
                Initialize(u);
            }
        }

        /// <summary>
        /// Initialize a new instance of the class
        /// </summary>
        /// <param name="url"></param>
        public WhoIsRequest(Uri url)
        {
            Initialize(url);
        }

        /// <summary>
        /// Do job
        /// </summary>
        void Initialize(Uri url)
        {
            if (url != null)
            {
                _url = url.ToString();

                _domain = url.DnsSafeHost;

                if (_domain.IndexOf(".") < _domain.Length - 4)
                {
                    _domain = _domain.Substring(_domain.IndexOf(".") + 1);
                }

                _tld = _domain.Substring(_domain.LastIndexOf("."));
                //_targetServer = Global.Global.WhoisServersConf.GetValue
                //    ("Servers", _tld, typeof(string)).ToString();

                if (_targetServer == "" || _targetServer == null)
                {
                    _targetServer = _defaultTargetServer;
                }
            }
        }

        /// <summary>
        /// Send the query
        /// </summary>
        public void GetResponse()
        {
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

            bw.RunWorkerAsync();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string response = DoRequest(_targetServer);

            StringReader sr = new StringReader(response);

            if (response.Contains("Whois Server:"))
            {
                string newTargetServer = "";

                while (newTargetServer == "")
                {
                    string line = sr.ReadLine().Trim();

                    if (line.StartsWith("Whois Server:"))
                    {
                        newTargetServer = line.Substring(14);

                        DoRequest(newTargetServer);
                    }
                }
            }

            OnConnectionClosed();
        }

        string DoRequest(string server)
        {
            WhoisEventArgs wea = new WhoisEventArgs(
                _url,
                _tld,
                server,
                "Trying to connect to " + server + " (query : " + _domain + ") ...\r\n"
            );

            OnResponseReceived(wea);

            string response = "";

            TcpClient client = null;

            try
            {
                client = new TcpClient(server, 43);
            }
            catch (Exception ex)
            {
                wea = new WhoisEventArgs(
                    _url,
                    _targetServer,
                    _targetServer,
                    "Connection to server " + server + " failed : \r\n" + 
                            ex.Message + "\r\n\r\n"
                );

                OnResponseReceived(wea);
            }

            if (client != null)
            {
                string formatedDomain = _domain + "\r\n";
                byte[] byteUrl = Encoding.ASCII.GetBytes(formatedDomain);

                Stream s = client.GetStream();

                bool error = true;

                try
                {
                    wea = new WhoisEventArgs(
                        _url,
                        _targetServer,
                        _targetServer,
                        "Connection to server " + _targetServer + 
                        " established. Executing query ...\r\n\r\n"
                    );

                    OnResponseReceived(wea);

                    s.Write(byteUrl, 0, formatedDomain.Length);

                    error = false;
                }
                catch (Exception ex)
                {
                    wea = new WhoisEventArgs(
                        _url,
                        _targetServer,
                        _targetServer,
                        "Unable to perform query : \r\n" + ex.Message + "\r\n\r\n"
                    );

                    OnResponseReceived(wea);
                }

                if (!error)
                {
                    StreamReader sr = new StreamReader(s, Encoding.ASCII);

                    response = sr.ReadToEnd() + "\r\n";

                    sr.Close();
                    sr.Dispose();
                    sr = null;

                    wea = new WhoisEventArgs(
                        _url,
                        _targetServer,
                        _targetServer,
                        response
                    );

                    OnResponseReceived(wea);
                }

                s.Close();
                s.Dispose();
                s = null;

                client.Close();
                client = null;
            }

            return response;
        }

        void OnResponseReceived(WhoisEventArgs e)
        {
            if (ResponseReceived != null)
            {
                ResponseReceived(this, e);
            }
        }

        void OnConnectionClosed()
        {
            if (ConnectionClosed != null)
            {
                ConnectionClosed(this, null);
            }
        }


    }
}
