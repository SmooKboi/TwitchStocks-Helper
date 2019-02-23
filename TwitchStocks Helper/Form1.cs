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
using System.Threading;

namespace TwitchStocks_Helper
{
    public partial class Form1 : MetroForm
    {
        private string StreamerName = "";
        private string Message = "";
        private string Amount;
        private int AmountTimes;
        private bool isTimerDisabled = false;
        private bool isNameChangeable = false;

        public Form1()
        {
            InitializeComponent();
            metroToggle1.ResetText();
            metroComboBox1.SelectedIndex = 0;
            metroComboBox2.SelectedIndex = 0;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }


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

        private void streamerNameLoop()
        {
            while (true)
            {
                if (metroTextBox1.Text == "" && !metroCheckBox1.Checked)
                {
                    if ((getStreamerNames() != null) && (getStreamerNames()[0] != "Home") && (getStreamerNames()[0] != "Leaderboards") && (getStreamerNames()[0] != "History") && (getStreamerNames()[0] != "Portfolio") && (getStreamerNames()[0] != "FAQ") && (getStreamerNames()[0] != "Legal"))
                    {
                        BeginInvoke((Action)(() => metroTextBox1.Text = getStreamerNames()[0] + " (" + getStreamerNames()[1].ToUpper() + ")"));
                    }
                }
                Thread.Sleep(1000);
                Application.DoEvents();
            }
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
            if (!metroToggle1.Checked)
            {
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
            }
            else
            {
                Message = metroTextBox2.Text + "\n";
            }
            Clipboard.SetText(String.Concat(Enumerable.Repeat(Message, AmountTimes)).ToString());
        }


        private void metroCheckBox1_MouseHover(object sender, EventArgs e)
        {

            metroToolTip1.Show("Disables Auto Detection of the Streamer Name \n" +
                "Check this if you encounter bugs", metroCheckBox1);
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)
            {
                metroComboBox1.Hide();
                metroCheckBox1.Hide();
                metroTextBox1.Hide();
                metroTextBox2.Show();
                metroButton2.Location = new Point(434, 187);
            }
            else
            {
                metroComboBox1.Show();
                metroCheckBox1.Show();
                metroTextBox1.Show();
                metroTextBox2.Hide();
                metroButton2.Location = new Point(434, 237);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            streamerNameLoop();
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            StreamerName = metroTextBox1.Text;
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)
            {
                metroTextBox2.Clear();
            }
            else
            {
                metroTextBox1.Clear();

            }
        }
    }
}
