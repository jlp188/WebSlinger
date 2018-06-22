using System;
using System.Collections.Generic;
using System.Text;

namespace WebSlinger
{
    public class BacklinkSummaryRecord
    {
        private double _mainseqno;

        public double Mainseqno
        {
            get { return _mainseqno; }
            set { _mainseqno = value; }
        }
        private int _blSumDtStamp;

        public int BlSumDtStamp
        {
            get { return _blSumDtStamp; }
            set { _blSumDtStamp = value; }
        }
        private string _blSumSource;

        public string BlSumSource
        {
            get { return _blSumSource; }
            set { _blSumSource = value; }
        }
        private int _blSumCount;

        public int BlSumCount
        {
            get { return _blSumCount; }
            set { _blSumCount = value; }
        }
        private double _blSumValidate;

        public double BlSumValidate
        {
            get { return _blSumValidate; }
            set { _blSumValidate = value; }
        }
    }
}
