using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebSlinger
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        LinkRaker LR;// = new LinkRaker();
        public dnengMainRecord dnRec;
        double traf3seqno;
        double AV_links;

        private void Form2_Load(object sender, EventArgs e)
        {
            LR = new LinkRaker();
        }


        private void btnProxIP_Click(object sender, System.EventArgs e)
        {
            //StreamReader myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            ProxyList PrxList = new ProxyList();

            PrxList.Delete();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader stRdr = new StreamReader(openFileDialog1.FileName))
                    {
                        String line;
                        //int rtn;
                        bool lb_rtn;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = stRdr.ReadLine()) != null)
                        {
                            listBox1.Items.Add(line.ToString());
                            //Console.WriteLine(line);

                            PrxList.ProxyFlag = true;
                            PrxList.ProxyIP = line.ToString();

                            lb_rtn = PrxList.Insert();
                        }
                    }
                }
                catch (Exception e1)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e1.Message);
                }

            }
        }

        private void btnDropList_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.ConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO DropList (drplDomain, drplDate, drplSuffix, drplFlag) "
            + "VALUES (@drplDomain, @drplDate, @drplSuffix, @drplFlag)";
            cmd.Parameters.Add("@drplDomain", SqlDbType.VarChar);
            cmd.Parameters.Add("@drplDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@drplSuffix", SqlDbType.VarChar);
            cmd.Parameters.Add("@drplFlag", SqlDbType.Bit);
            
            cmd.Connection = conn;
            //cmd.Parameters.Add("@proxIP = ");

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        String line;
                        string dropDate;
                        string dropDomain;
                        string dropSuffix;
                        int rtn;
                        //bool lb_rtn;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            listBox1.Items.Add(line.ToString());
                            dropDate = line.Substring(0, 10);
                            dropDomain = line.Substring(15);
                            int pos = dropDomain.IndexOf(".", 0);
                            dropSuffix = dropDomain.Substring((dropDomain.IndexOf(".", 0))+1);

                            //Console.WriteLine(line);
                            cmd.Parameters["@drplDomain"].Value = dropDomain;
                            cmd.Parameters["@drplDate"].Value = Convert.ToDateTime(dropDate);
                            cmd.Parameters["@drplSuffix"].Value = dropSuffix;
                            cmd.Parameters["@drplFlag"].Value = false;

                            rtn = cmd.ExecuteNonQuery();


                        }
                    }
                }
                catch (Exception e1)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e1.Message);
                }

            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            //LinkRaker LR = new LinkRaker();
            LR.lb_Proxy = checkBox1.Checked;

            foreach(object itemChecked in checkedListBoxResources.CheckedItems)
            {
                if(checkedListBoxResources.GetItemChecked(checkedListBoxResources.Items.IndexOf(itemChecked)))
                {
                    switch (itemChecked.ToString())
                    {
                        case "Alexa":
                            LR.lb_Alexa = true;
                            break;
                        case "Compete":
                            LR.lb_Compete = true;
                            break;
                        case "Quantcast":
                            LR.lb_Quant = true;
                            break;
                        case "Archive":
                            LR.lb_Archive = true;
                            break;
                        case "PageRank":
                            LR.lb_PageRank = true;
                            break;
                        case "WhoIs":
                            LR.lb_WhoIs = true;
                            break;
                        case "Estibot":
                            LR.lb_Estibot = true;
                            break;
                        case "DMOZ":
                            LR.lb_DMOZ = true;
                            break;
                        case "IndexPages":
                            LR.lb_IndexPages = true;
                            break;
                        case "BackLinks":
                            LR.lb_BackLinks = true;
                            break;
                        default:

                            break;
                    }
                }

            }

            //LR.RunResources();

            string DDName;
            bool lb_rtn = false;

            if (LR.lb_Proxy) LR.proxAddr = LR.GetProxy();

            DDName = LR.GetDDN();

            while (DDName != null)
            {
                if (LR.lb_PageRank) LR.RunPageRank(DDName);
                if (LR.lb_Archive) LR.RunArchive(DDName);
                if (LR.lb_Quant) LR.RunQuantcast(DDName);
                if (LR.lb_DMOZ) LR.RunDMOZ(DDName);
                if (LR.lb_PageRank || LR.lb_Archive || LR.lb_Quant || LR.lb_DMOZ) lb_rtn = LR.dnRec.Update();

                if (LR.lb_Alexa) LR.RunAlexa(DDName);
                
                if (LR.lb_Compete) LR.RunCompete(DDName);
                if (LR.lb_WhoIs) LR.RunWhoIs(DDName);
                if (LR.lb_Estibot) LR.RunEstibot(DDName);
                if (LR.lb_IndexPages) LR.RunIndexPages(DDName);
                if (LR.lb_BackLinks) LR.RunBackLinks(DDName);


                DDName = LR.GetDDN();
            }



        }


        

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                LR.lb_Proxy = true;
            }
            else
            {
                LR.lb_Proxy = false;
            }
        }

        private void btnImportBL4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.ConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO TrafficRoutine3 (traf3_seqno, traf3_domname, traf3_Alexa_tr, traf3_Altavista_lp, traf3_Google_lp, traf3_MSN_lp, traf3_Yahoo_lp, traf3_processed) "
                + "VALUES (@traf3_seqno, @traf3_domname, @traf3_Alexa_tr, @traf3_Altavista_lp, @traf3_Google_lp, @traf3_MSN_lp, @traf3_Yahoo_lp, @traf3_processed)";
            cmd.Parameters.Add("@traf3_seqno", SqlDbType.Int);
            cmd.Parameters.Add("@traf3_domname", SqlDbType.VarChar);
            cmd.Parameters.Add("@traf3_Alexa_tr", SqlDbType.Int);
            cmd.Parameters.Add("@traf3_Altavista_lp", SqlDbType.Float);
            cmd.Parameters.Add("@traf3_Google_lp", SqlDbType.Int);
            cmd.Parameters.Add("@traf3_MSN_lp", SqlDbType.Int);
            cmd.Parameters.Add("@traf3_Yahoo_lp", SqlDbType.Int);
            cmd.Parameters.Add("@traf3_processed", SqlDbType.Bit);

            cmd.Connection = conn;
            //cmd.Parameters.Add("@proxIP = ");

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        String line;
                        string[] parses;
                        string sep;
                        //string seqno;
                        //string DomName;
                        //string Alexa_tr;
                        //string Altavista_lp;
                        //string Google_lp;
                        //string MSN_lp;
                        //string Yahoo_lp;

                        sep = ",";
                        int rtn;
                        //bool lb_rtn;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            listBox1.Items.Add(line.ToString());
                            parses = line.Split(sep.ToCharArray(),StringSplitOptions.None);
                            //dropDomain = line.Substring(15);
                            //int pos = dropDomain.IndexOf(".", 0);
                            //dropSuffix = dropDomain.Substring((dropDomain.IndexOf(".", 0)) + 1);

                            //Console.WriteLine(line);
                            cmd.Parameters["@traf3_seqno"].Value = Convert.ToInt32(parses[0]);
                            cmd.Parameters["@traf3_domname"].Value = parses[1].ToString();
                            cmd.Parameters["@traf3_Alexa_tr"].Value = Convert.ToInt32(parses[2]);
                            cmd.Parameters["@traf3_Altavista_lp"].Value = Convert.ToDouble(parses[3]);
                            cmd.Parameters["@traf3_Google_lp"].Value = Convert.ToInt32(parses[4]);
                            cmd.Parameters["@traf3_MSN_lp"].Value = Convert.ToInt32(parses[5]);
                            cmd.Parameters["@traf3_Yahoo_lp"].Value = Convert.ToInt32(parses[6]);
                            cmd.Parameters["@traf3_processed"].Value = false;

                            rtn = cmd.ExecuteNonQuery();


                        }
                    }
                }
                catch (Exception e1)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e1.Message);
                }

            }
        }


        private string GetDN()
        {
            //dnengMainRecord dnRec;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.ConnStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT TOP 1 traf3_seqno, traf3_domname, traf3_Altavista_lp FROM TrafficRoutine3 where traf3_processed = 'false'";//traf3_seqno = 1000";// //"SELECT TOP 1 dropSeqno, drplDomain FROM DropList Where drplFlag = 'false' and drplSuffix = 'com'";
                cmd.Connection = conn;
                string DDN;
                
                SqlDataReader SDR1;
                try
                {
                    SDR1 = cmd.ExecuteReader();
                    SDR1.Read();
                    DDN = SDR1["traf3_domname"].ToString();
                    AV_links = Convert.ToDouble(SDR1["traf3_Altavista_lp"]);
                    traf3seqno = Convert.ToDouble(SDR1["traf3_seqno"]);
                    SDR1.Close();
                }
                catch (Exception e1)
                {
                    ErrLogRec ErrLog = new ErrLogRec();
                    bool lb_rtn = ErrLog.Insert("Form2.GetDDN", e1);
                    return null;
                }


                cmd.CommandText = "UPDATE TrafficRoutine3 SET traf3_processed = 'true' WHERE traf3_domname = @domname";
                cmd.Parameters.AddWithValue("@domname", DDN);
                int rtn = cmd.ExecuteNonQuery();

                dnRec = new dnengMainRecord(DDN);

                return DDN;
            }
        }



        private void UpdateDNprocessTime(string dn, TimeSpan dif)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.ConnStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE TrafficRoutine3 SET traf3_procTime = @dif WHERE traf3_domname = @domname";
                cmd.Parameters.AddWithValue("@dif", dif.TotalSeconds); 
                cmd.Parameters.AddWithValue("@domname", dn);
                cmd.Connection = conn;
                int rtn = cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE TrafficRoutine4 SET traf4_procTime = @dif2 WHERE traf4_domname = @domname2 AND traf4_bl_domainname = ''";
                cmd.Parameters.AddWithValue("@dif2", dif.TotalSeconds);
                cmd.Parameters.AddWithValue("@domname2", dn);
                cmd.Connection = conn;
                rtn = cmd.ExecuteNonQuery();

            }
        }

        private string webAddr(string url)
        {
            try
            { 
                Uri myUri = new Uri(url);
                return myUri.Host.ToString();
            }
            catch(Exception ex)
            {
                string exc = ex.Message;
                return url;
            }
            
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
            //using (SqlConnection conn = new SqlConnection())
            //{
            //    conn.ConnectionString = Properties.Settings.Default.ConnStr;
                
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "INSERT INTO CycleTimes (@domname, @location, @execTime)";
            //    cmd.Connection = conn;
            //    cmd.Parameters.AddWithValue("@domname", DN);
            //    cmd.Parameters.AddWithValue("@location", location);
            //    cmd.Parameters.AddWithValue("@execTime", execTime);
            //    conn.Open();
            //    int rtn = cmd.ExecuteNonQuery();

            //    return rtn == 1;
            //}
        }

        private void btnSpider_Click(object sender, EventArgs e)
        {
            string Dname;
            string line;
            int cntr = 0;
            double timeAvg = 0;
            double timeTotal = 0;

            ProxyShuttler PrxShtlr = new ProxyShuttler();

            while ((Dname = GetDN()) != null)
            {
                DateTime startTime = DateTime.Now;
                M4Spider Spidey = new M4Spider();
                Spidey.MaxOutboundLinks = 3;
                Spidey.ProxyDomain = PrxShtlr.CurrentProxy.ToString();
                Spidey.AddUnspidered("http://www.alexa.com/site/linksin/" + Dname);//"http://www.databasejobs.com/");//
                Spidey.BackLinkDN = Dname;
                ArrayList BackLinksList;
                if (AV_links > 0)
                {
                    bool lb_rtn = Spidey.CrawlNext();
                    if (!lb_rtn) continue;

                    if (Spidey.SR1 != null)
                    {
                        BackLinksList = Spidey.SR1.HyperLinks;
                    }
                    else
                    {
                        BackLinksList = new ArrayList();
                        BackLinksList.Add(Dname);
                    }
                }
                else
                {
                    BackLinksList = new ArrayList();
                    BackLinksList.Add(Dname);
                }
                


                LinkRaker LR = new LinkRaker();
                LR.lb_Proxy = true; // While at work . . .
                LR.RunAlexa(Dname);
                
                LR.RunCompete(Dname);

                //Run against Quantcast
                LR.RunQuantcast(Dname);

                DateTime StopTime = DateTime.Now;
                TimeSpan timeDif = StopTime - startTime;
                double procTime = timeDif.TotalSeconds;

                bool blrtn = CycleTime("Parent Domain", Dname, (float)procTime);

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = Properties.Settings.Default.ConnStr;
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO TrafficRoutine4"
                        + " (traf4_traf3_seqno, traf4_domname, traf4_bl_domainname, traf4_compete_esttraffic, traf4_compete_perdiff,"
                        + " traf4_quantcast_esttraffic, traf4_alexa_tr, traf4_alexa_reach, traf4_alexa_search, traf4_alexa_country1,"
                        + " traf4_alexa_country1_per, traf4_alexa_country2, traf4_alexa_country2_per, traf4_alexa_country3, traf4_alexa_country3_per, traf4_procTime)"
                        + " VALUES (@seqno, @domname, @bl_domname, @compete_esttraf, @compete_perdiff, @quantcast_esttraf, @alexa_tr,"
                        + " @alexa_reach, @alexa_search, @alexa_ctry1, @alexa_ctry1_per, @alexa_ctry2, @alexa_ctry2_per, @alexa_ctry3, @alexa_ctry3_per, @timedif)";
                    cmd.Parameters.AddWithValue("@seqno", traf3seqno);
                    cmd.Parameters.AddWithValue("@domname", Dname);
                    cmd.Parameters.AddWithValue("@bl_domname", "");
                    cmd.Parameters.AddWithValue("@compete_esttraf", LR.CmptRec.CmptMonth1);
                    cmd.Parameters.AddWithValue("@compete_perdiff", 0);
                    cmd.Parameters.AddWithValue("@quantcast_esttraf", LR.dnRec.QuantcastCtr);
                    cmd.Parameters.AddWithValue("@alexa_tr", LR.AlxRec.AlxHighRank);
                    cmd.Parameters.AddWithValue("@alexa_reach", LR.AlxRec.AlxHighReach);
                    cmd.Parameters.AddWithValue("@alexa_search", LR.AlxRec.AlxHighSearch);
                    cmd.Parameters.AddWithValue("@alexa_ctry1", (LR.AlxRec.AlxCountry1.ToString() == null ? "" : LR.AlxRec.AlxCountry1.ToString()));
                    cmd.Parameters.AddWithValue("@alexa_ctry1_per", LR.AlxRec.AlxCntry1_per);
                    cmd.Parameters.AddWithValue("@alexa_ctry2", (LR.AlxRec.AlxCountry2.ToString() == null ? "" : LR.AlxRec.AlxCountry2.ToString()));
                    cmd.Parameters.AddWithValue("@alexa_ctry2_per", LR.AlxRec.AlxCntry2_per);
                    cmd.Parameters.AddWithValue("@alexa_ctry3", (LR.AlxRec.AlxCountry3.ToString() == null ? "" : LR.AlxRec.AlxCountry3.ToString()));
                    cmd.Parameters.AddWithValue("@alexa_ctry3_per", LR.AlxRec.AlxCntry3_per);
                    cmd.Parameters.AddWithValue("@timedif", procTime);
                    cmd.Connection = conn;

                    int rtn = cmd.ExecuteNonQuery();
                }

                DateTime bckLnkStartTime = DateTime.Now;
                int count = 0;
                foreach (object ALobj in BackLinksList)
                {
                    if (ALobj.ToString().ToUpper().Contains(Dname.ToUpper())) continue;
                    count++;
                    LinkRaker LR1 = new LinkRaker();
                    LR1.lb_Proxy = true; // While at work . . .
                    LR1.RunAlexa(ALobj.ToString());

                    LR1.RunCompete(ALobj.ToString());

                    LR.RunQuantcast(ALobj.ToString());

                    StopTime = DateTime.Now;
                    TimeSpan timeDifA = StopTime - bckLnkStartTime;
                    double subProcTime = timeDifA.TotalSeconds;
                    blrtn = CycleTime("Backlink" + count.ToString() + " Domain", Dname, (float)subProcTime);

                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = Properties.Settings.Default.ConnStr;
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "INSERT INTO TrafficRoutine4"
                            + " (traf4_traf3_seqno, traf4_domname, traf4_bl_domainname, traf4_compete_esttraffic, traf4_compete_perdiff,"
                            + " traf4_quantcast_esttraffic, traf4_alexa_tr, traf4_alexa_reach, traf4_alexa_search, traf4_alexa_country1,"
                            + " traf4_alexa_country1_per, traf4_alexa_country2, traf4_alexa_country2_per, traf4_alexa_country3, traf4_alexa_country3_per, traf4_procTime)"
                            + " VALUES (@seqno, @domname, @bl_domname, @compete_esttraf, @compete_perdiff, @quantcast_esttraf, @alexa_tr,"
                            + " @alexa_reach, @alexa_search, @alexa_ctry1, @alexa_ctry1_per, @alexa_ctry2, @alexa_ctry2_per, @alexa_ctry3, @alexa_ctry3_per, @timedif)";
                        cmd.Parameters.AddWithValue("@seqno", traf3seqno);
                        cmd.Parameters.AddWithValue("@domname", Dname);
                        cmd.Parameters.AddWithValue("@bl_domname", webAddr(ALobj.ToString()));
                        cmd.Parameters.AddWithValue("@compete_esttraf", LR1.CmptRec.CmptMonth1);
                        cmd.Parameters.AddWithValue("@compete_perdiff", 0);
                        cmd.Parameters.AddWithValue("@quantcast_esttraf", LR1.dnRec.QuantcastCtr);
                        cmd.Parameters.AddWithValue("@alexa_tr", LR1.AlxRec.AlxHighRank);
                        cmd.Parameters.AddWithValue("@alexa_reach", LR1.AlxRec.AlxHighReach);
                        cmd.Parameters.AddWithValue("@alexa_search", LR1.AlxRec.AlxHighSearch);
                        cmd.Parameters.AddWithValue("@alexa_ctry1", (LR1.AlxRec.AlxCountry1.ToString() == null ? "" : LR1.AlxRec.AlxCountry1.ToString()));
                        cmd.Parameters.AddWithValue("@alexa_ctry1_per", LR1.AlxRec.AlxCntry1_per);
                        cmd.Parameters.AddWithValue("@alexa_ctry2", (LR1.AlxRec.AlxCountry2.ToString() == null ? "" : LR1.AlxRec.AlxCountry2.ToString()));
                        cmd.Parameters.AddWithValue("@alexa_ctry2_per", LR1.AlxRec.AlxCntry2_per);
                        cmd.Parameters.AddWithValue("@alexa_ctry3", (LR1.AlxRec.AlxCountry3.ToString() == null ? "" : LR1.AlxRec.AlxCountry3.ToString()));
                        cmd.Parameters.AddWithValue("@alexa_ctry3_per", LR1.AlxRec.AlxCntry3_per);
                        cmd.Parameters.AddWithValue("@timedif", subProcTime);
                        cmd.Connection = conn;

                        bckLnkStartTime = DateTime.Now;

                        int rtn = cmd.ExecuteNonQuery();

                        StopTime = DateTime.Now;
                        timeDifA = StopTime - bckLnkStartTime;
                        subProcTime = timeDifA.TotalSeconds;
                        blrtn = CycleTime("Backlink" + count.ToString() + " UpdateTime", Dname, (float)subProcTime);
                    }

                    LR1 = null;
                }

                LR = null;
                Spidey = null;
                txtBxExecTime.Text = timeDif.TotalSeconds.ToString();
                UpdateDNprocessTime(Dname, timeDif);
                cntr++;
                label2.Text = cntr.ToString();

                StopTime = DateTime.Now;
                timeDif = StopTime - startTime;

                line = cntr.ToString() + " - " + Dname + " -- Processing time: " + timeDif.TotalSeconds.ToString();
                timeTotal = timeTotal + timeDif.TotalSeconds;
                timeAvg = timeTotal / cntr;

                label3.Text = timeAvg.ToString();
                //listBox1.SelectedItem = listBox1.Items.Add(line.ToString());

                Application.DoEvents();
                
                
            }

            MessageBox.Show("It is finished! Total: " + cntr.ToString() + " Domains cycled.");


            

        }

        
    }
}
