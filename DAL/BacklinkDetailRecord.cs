using System;
using System.Collections.Generic;
using System.Text;

namespace WebSlinger
{
    public class BacklinkDetailRecord
    {
        private double _mainseqno;

        public double Mainseqno
        {
            get { return _mainseqno; }
            set { _mainseqno = value; }
        }
        private DateTime _blDetDtStamp;

        public DateTime BlDetDtStamp
        {
            get { return _blDetDtStamp; }
            set { _blDetDtStamp = value; }
        }
        private string _blDetDomname;

        public string BlDetDomname
        {
            get { return _blDetDomname; }
            set { _blDetDomname = value; }
        }
        private string _blDetSource;

        public string BlDetSource
        {
            get { return _blDetSource; }
            set { _blDetSource = value; }
        }
        private int _blDetPageRank;

        public int BlDetPageRank
        {
            get { return _blDetPageRank; }
            set { _blDetPageRank = value; }
        }
        private double _blDetAlexaRating;

        public double BlDetAlexaRating
        {
            get { return _blDetAlexaRating; }
            set { _blDetAlexaRating = value; }
        }
        private double _blDetAlexaReach;

        public double BlDetAlexaReach
        {
            get { return _blDetAlexaReach; }
            set { _blDetAlexaReach = value; }
        }
        private double _blDetCompete;

        public double BlDetCompete
        {
            get { return _blDetCompete; }
            set { _blDetCompete = value; }
        }
        private double _blDetQuantcast;

        public double BlDetQuantcast
        {
            get { return _blDetQuantcast; }
            set { _blDetQuantcast = value; }
        }
        private double _blDetScore;

        public double BlDetScore
        {
            get { return _blDetScore; }
            set { _blDetScore = value; }
        }
    }
}
