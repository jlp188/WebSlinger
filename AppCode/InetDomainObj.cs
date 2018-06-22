using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
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
    class InetDomainObj
    {
        InetDomainObj()
        {

        }


        #region properties

        private string _inetDomain;
        public string InetDomain
        {
            get { return _inetDomain; }
            set { _inetDomain = value; }
        }


        private double _seqno1;
        public double Seqno1
        {
            get { return _seqno1; }
            set { _seqno1 = value; }
        }


        private double _seqno2;
        public double Seqno2
        {
            get { return _seqno2; }
            set { _seqno2 = value; }
        }


        private double _seqno3;
        public double Seqno3
        {
            get { return _seqno3; }
            set { _seqno3 = value; }
        }


        private string _backlinkParent;
        public string BacklinkParent
        {
            get { return _backlinkParent; }
            set { _backlinkParent = value; }
        }


        private List<string> _backlinksList;
        public List<string> BacklinksList
        {
            get { return _backlinksList; }
            set { _backlinksList = value; }
        }


        private float _competeEstTraf;
        public float CompeteEstTraf
        {
            get { return _competeEstTraf; }
            set { _competeEstTraf = value; }
        }


        private float _competePercDif;
        public float CompetePercDif
        {
            get { return _competePercDif; }
            set { _competePercDif = value; }
        }


        private float _quantcastEstTraf;
        public float QuantcastEstTraf
        {
            get { return _quantcastEstTraf; }
            set { _quantcastEstTraf = value; }
        }


        private float _alexaRank7D;
        public float AlexaRank7D
        {
            get { return _alexaRank7D; }
            set { _alexaRank7D = value; }
        }


        private float _alexaRank1M;
        public float AlexaRank1M
        {
            get { return _alexaRank1M; }
            set { _alexaRank1M = value; }
        }


        private float _alexaRank3M;
        public float AlexaRank3M
        {
            get { return _alexaRank3M; }
            set { _alexaRank3M = value; }
        }


        private float _alexaRankChng;
        public float AlexaRankChng
        {
            get { return _alexaRankChng; }
            set { _alexaRankChng = value; }
        }


        private float _alexaRankDir;
        public float AlexaRankDir
        {
            get { return _alexaRankDir; }
            set { _alexaRankDir = value; }
        }


        private float _alexaReach7D;
        public float AlexaReach7D
        {
            get { return _alexaReach7D; }
            set { _alexaReach7D = value; }
        }


        private float _alexaReach1M;
        public float AlexaReach1M
        {
            get { return _alexaReach1M; }
            set { _alexaReach1M = value; }
        }


        private float _alexaReach3M;
        public float AlexaReach3M
        {
            get { return _alexaReach3M; }
            set { _alexaReach3M = value; }
        }


        private float _alexaReachChng;
        public float AlexaReachChng
        {
            get { return _alexaReachChng; }
            set { _alexaReachChng = value; }
        }


        private float _alexaReachDir;
        public float AlexaReachDir
        {
            get { return _alexaReachDir; }
            set { _alexaReachDir = value; }
        }


        private float _alexaSearch1M;
        public float AlexaSearch1M
        {
            get { return _alexaSearch1M; }
            set { _alexaSearch1M = value; }
        }


        private float _alexaSearch7D;
        public float AlexaSearch7D
        {
            get { return _alexaSearch7D; }
            set { _alexaSearch7D = value; }
        }


        private float _alexaSearch3M;
        public float AlexaSearch3M
        {
            get { return _alexaSearch3M; }
            set { _alexaSearch3M = value; }
        }


        private float _alexaSearchChng;
        public float AlexaSearchChng
        {
            get { return _alexaSearchChng; }
            set { _alexaSearchChng = value; }
        }


        private float _alexaSearchDir;
        public float AlexaSearchDir
        {
            get { return _alexaSearchDir; }
            set { _alexaSearchDir = value; }
        }


        private string _alexaCountry1;
        public string AlexaCountry1
        {
            get { return _alexaCountry1; }
            set { _alexaCountry1 = value; }
        }


        private float _alexaCountry1Perc;
        public float AlexaCountry1Perc
        {
            get { return _alexaCountry1Perc; }
            set { _alexaCountry1Perc = value; }
        }


        private string _alexaCountry2;
        public string AlexaCountry2
        {
            get { return _alexaCountry2; }
            set { _alexaCountry2 = value; }
        }


        private float _alexaCountry2Perc;
        public float AlexaCountry2Perc
        {
            get { return _alexaCountry2Perc; }
            set { _alexaCountry2Perc = value; }
        }


        private string _alexaCountry3;
        public string AlexaCountry3
        {
            get { return _alexaCountry3; }
            set { _alexaCountry3 = value; }
        }


        private float _alexaCountry3Perc;
        public float AlexaCountry3Perc
        {
            get { return _alexaCountry3Perc; }
            set { _alexaCountry3Perc = value; }
        }


        private DateTime _whoIsCreateDt;
        public DateTime WhoIsCreateDt
        {
            get { return _whoIsCreateDt; }
            set { _whoIsCreateDt = value; }
        }


        private double _cmptNumVisits;
        public double CmptNumVisits
        {
            get { return _cmptNumVisits; }
            set { _cmptNumVisits = value; }
        }

        private DateTime _cmptLastUpdate;
        public DateTime CmptLastUpdate
        {
            get { return _cmptLastUpdate; }
            set { _cmptLastUpdate = value; }
        }

        private float _cmptMonth1;
        public float CmptMonth1
        {
            get { return _cmptMonth1; }
            set { _cmptMonth1 = value; }
        }

        private float _cmptMonth2;
        public float CmptMonth2
        {
            get { return _cmptMonth2; }
            set { _cmptMonth2 = value; }
        }

        private float _cmptMonth3;
        public float CmptMonth3
        {
            get { return _cmptMonth3; }
            set { _cmptMonth3 = value; }
        }

        private float _cmptMonth4;
        public float CmptMonth4
        {
            get { return _cmptMonth4; }
            set { _cmptMonth4 = value; }
        }

        private float _cmptMonth5;
        public float CmptMonth5
        {
            get { return _cmptMonth5; }
            set { _cmptMonth5 = value; }
        }

        private float _cmptMonth6;
        public float CmptMonth6
        {
            get { return _cmptMonth6; }
            set { _cmptMonth6 = value; }
        }

        private float _cmptMonth7;
        public float CmptMonth7
        {
            get { return _cmptMonth7; }
            set { _cmptMonth7 = value; }
        }

        private float _cmptMonth8;
        public float CmptMonth8
        {
            get { return _cmptMonth8; }
            set { _cmptMonth8 = value; }
        }

        private float _cmptMonth9;
        public float CmptMonth9
        {
            get { return _cmptMonth9; }
            set { _cmptMonth9 = value; }
        }

        private float _cmptMonth10;
        public float CmptMonth10
        {
            get { return _cmptMonth10; }
            set { _cmptMonth10 = value; }
        }

        private float _cmptMonth11;
        public float CmptMonth11
        {
            get { return _cmptMonth11; }
            set { _cmptMonth11 = value; }
        }

        private float _cmptMonth12;
        public float CmptMonth12
        {
            get { return _cmptMonth12; }
            set { _cmptMonth12 = value; }
        }


        private double _estBtppcAds;
        public double EstBtppcAds
        {
            get { return _estBtppcAds; }
            set { _estBtppcAds = value; }
        }

        private float _estBtMaxPPCamount;
        public float EstBtMaxPPCamount
        {
            get { return _estBtMaxPPCamount; }
            set { _estBtMaxPPCamount = value; }
        }

        private float _estBtOvertureNum;
        public float EstBtOvertureNum
        {
            get { return _estBtOvertureNum; }
            set { _estBtOvertureNum = value; }
        }

        private float _estBtworktracker;
        public float EstBtworktracker
        {
            get { return _estBtworktracker; }
            set { _estBtworktracker = value; }
        }

        private double _estBtnumSearches;
        public double EstBtnumSearches
        {
            get { return _estBtnumSearches; }
            set { _estBtnumSearches = value; }
        }

        private float _estBttraffic;
        public float EstBttraffic
        {
            get { return _estBttraffic; }
            set { _estBttraffic = value; }
        }


        private string _whoIsNameServer;
        public string WhoIsNameServer
        {
            get { return _whoIsNameServer; }
            set { _whoIsNameServer = value; }
        }


        private bool _comExist;
        public bool ComExist
        {
            get { return _comExist; }
            set { _comExist = value; }
        }


        private bool _netExist;
        public bool NetExist
        {
            get { return _netExist; }
            set { _netExist = value; }
        }


        private bool _orgExist;
        public bool OrgExist
        {
            get { return _orgExist; }
            set { _orgExist = value; }
        }


        private bool _infoExist;
        public bool InfoExist
        {
            get { return _infoExist; }
            set { _infoExist = value; }
        }

        #endregion


        #region Methods()

        #endregion
    }
}
