using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Net;
using System.IO;

namespace TwitchStocks_Helper
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            metroComboBox1.SelectedIndex = 0;
            metroComboBox2.SelectedIndex = 0;
        }

        private string StreamerName = "";
        private string Message = "null";
        private string Amount;
        private int AmountTimes;
        private bool isTimerDisabled = false;

        private string[] getStreamerNames()
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (process.MainWindowTitle.StartsWith("TwitchStocks - "))
                {
                    string processName = process.MainWindowTitle.Replace("TwitchStocks - ", "").Replace("Google Chrome", "");
                    string[] names = processName.Split(' ');
                    return names;
                }
            }
            return null;
        }

        private void selectAmountChanged(object sender, EventArgs e)
        {
            Amount = metroComboBox2.SelectedItem.ToString();
            switch (Amount)
            {
                case "x1":
                    AmountTimes = 1;
                    break;
                case "x4":
                    AmountTimes = 4;
                    break;
                case "x8":
                    AmountTimes = 8;
                    break;
                case "x12":
                    AmountTimes = 12;
                    break;
                case "x16":
                    AmountTimes = 16;
                    break;
                case "x20":
                    AmountTimes = 20;
                    break;
            }
        }

        private void copyClick(object sender, EventArgs e)
        {
            StreamerName = metroTextBox1.Text;
            switch (metroComboBox1.SelectedItem.ToString())
            {
                case "Buy":
                    Message = ":chart_with_upwards_trend: BUY **" + StreamerName + "** :chart_with_upwards_trend: \n";
                    break;
                case "Sell":
                    Message = ":chart_with_downwards_trend: SELL **" + StreamerName + "** :chart_with_downwards_trend: \n";
                    break;
                case "Hold":
                    Message = ":raised_hand: HOLD **" + StreamerName + "** :raised_hand: \n";
                    break;
                case "Warn":
                    Message = ":warning: **" + StreamerName + "** MIGHT BE RISKY; TRY 1000 SHARES :warning: \n";
                    break;
            }
            Clipboard.SetText(String.Concat(Enumerable.Repeat(Message, AmountTimes)).ToString());
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void changeName_Tick(object sender, EventArgs e)
        {
            if (getStreamerNames() != null)
            {
                Console.WriteLine(getStreamerNames()[0]);
                if ((getStreamerNames()[0] != "Home") && (getStreamerNames()[0] != "Leaderboards") && (getStreamerNames()[0] != "History") && (getStreamerNames()[0] != "Portfolio") & (getStreamerNames()[0] != "FAQ") & (getStreamerNames()[0] != "Legal"))
                {
                    metroTextBox1.Text = getStreamerNames()[0] + " (" + getStreamerNames()[1].ToUpper() + ")";
                }
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "")
            {
                if (!isTimerDisabled)
                {
                    changeName.Start();
                }
            }
            else
            {
                changeName.Stop();
            }
        }

        private void metroCheckBox1_MouseHover(object sender, EventArgs e)
        {

            metroToolTip1.Show("Disables Auto Detection of the Streamer Name \n" +
                "Check this if you encounter bugs", metroCheckBox1);
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked)
            {
                changeName.Stop();
                isTimerDisabled = true;
            }
            else
            {
                changeName.Start();
                isTimerDisabled = false;
            }
        }
    }
}
