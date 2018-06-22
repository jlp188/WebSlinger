using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace WebSlinger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSling_Click(object sender, EventArgs e)
        {

            LinkRaker LR = new LinkRaker();

            LR.AlexaReach("www.flkr.com");
            
            
            treeViewWebList.BeginUpdate();
            treeViewWebList.Nodes.Clear();
            treeViewWebList.Nodes.Add("Web Search Results");
            TreeNodeCollection tnode = treeViewWebList.Nodes;
            treeViewWebList.EndUpdate();
            
            string line = "http://www.6packabs.com";
            //StreamReader input = new StreamReader(txtBxInput.Text);
            while (line != null)//= input.ReadLine()) 
            {
                //match = Regex.Match(line, "http");
                while (!reelItIn(line,3,tnode))
                {}
            }
        }

        private bool reelItIn(string addr, int depth, TreeNodeCollection tnode)
        {
            treeViewWebList.BeginUpdate();
            tnode.Add(addr);
            treeViewWebList.EndUpdate();
            if (depth == 0) return true;

            for (int i = 0; i < 5; i++ )
            {
                string waddr = wrapit(addr,i);

                Match match, match1;
                string line, found;
                WebRequest request = WebRequest.Create(waddr);
                WebResponse response = request.GetResponse();
                StreamReader webbing = new StreamReader(response.GetResponseStream());
                while ((line = webbing.ReadLine()) != null)
                {
                    listBox1.Items.Add(line);
                    match = Regex.Match(line, "<a href");
                    if (match.Index > 0)
                    {
                        match1 = Regex.Match(line, "</a>");
                        found = line.Substring(match.Index, (match1.Index - match.Index) + 4);
                        if (found.Length > 0)
                        {
                            depth--;
                            reelItIn(found, depth, tnode);
                        }

                    }
                }
            }
            return true;
        }

        private string wrapit(string addr, int blse)
        {
            switch (blse)
            {
                case 0: //"Alexa":
                    return "http://www.alexa.com/data/ds/linksin/" + addr + "?q=link:" + addr + "/";
                    //break;
                case 1: //"Google":
                    return "http://www.google.com/search?q=link:" + addr + "&filter=0";
                    //break;
                case 2: //"Yahoo":
                    return "http://siteexplorer.search.yahoo.com/search?p=http://" + addr + "&bwm=i&bwms=p&bwmf=u&fr=sfp&fr2=seo-rd-se";
                    //break;
                case 3: //"Altavista":
                    return "http://www.altavista.com/web/results?q=link%3A" + addr;
                    //break;
                case 4: //"Alltheweb":
                    return "http://www.alltheweb.com/search?q=link%3A" + addr;
                    //break;
                default:
                    return "";
                    //break;
            }
        }

    }
}