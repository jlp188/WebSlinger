using System;

namespace WebSlinger
{
    /// <summary>
    /// Arguments for the whois system    
    /// </summary>
    public class WhoisEventArgs : EventArgs
    {
        private string _url, _message, _tld, _targetServer;

        /// <summary>
        /// URL to query
        /// </summary>
        public string Url { get { return _url; } }

        /// <summary>
        /// Message returned by the whois server
        /// </summary>
        public string Message { get { return _message; } }

        /// <summary>
        /// TLD of the current query
        /// </summary>
        public string TLD { get { return _tld; } }

        /// <summary>
        /// Hostname of the server queried
        /// </summary>
        public string TargetServer { get { return _targetServer; } }

        /// <summary>
        /// Initialize a new instance of the class with the specified parameters
        /// </summary>
        /// <param name="url">Url to query</param>
        /// <param name="tld">Top Level Domain</param>
        /// <param name="targetServer">Server handling the query</param>
        /// <param name="message">Message linked to the event</param>
        public WhoisEventArgs
            (string url, string tld, string targetServer, string message)
        {
            _url = url;
            _tld = tld;
            _targetServer = targetServer;
            _message = message;
        }
    }

}
