using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WebSlinger
{
    
    class SpiderResults
    {
        private string _DDN;
        public string DDN
        {
            get { return _DDN; }
            set { _DDN = value; }
        }

        private string _host;
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _port;
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _protocol;
        public string Protocol
        {
            get { return _protocol; }
            set { _protocol = value; }
        }

        private string _queryString;
        public string QueryString
        {
            get { return _queryString; }
            set { _queryString = value; }
        }

        private string _keywords;
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private ArrayList _hyperLinks;
        public ArrayList HyperLinks
        {
            get { return _hyperLinks; }
            //set { _hyperLinks = value; }
        }

        public SpiderResults()
        {
            _hyperLinks = new ArrayList();
            //HyperLinks = new ArrayList();
        }

        public int AddHyperLink(string link)
        {
            return _hyperLinks.Add(link);
        }

        public void Init(string DomainName)
        {
            DDN = DomainName;
        }

    }

}
