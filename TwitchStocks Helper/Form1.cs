using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchStocks_Helper
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            this.Icon = new Icon("Resources/baseline-bar_chart-24px.ico");
            InitializeComponent();
        }

        private string StreamerName;
        private string Message;

        private void selectMessageIndexChanged(object sender, EventArgs e)
        {
            Message = metroComboBox1.SelectedItem.ToString();
        }

        private void streamerNameTextChanged(object sender, EventArgs e)
        {
            StreamerName = metroTextBox1.Text;
        }

        private void copyClick(object sender, EventArgs e)
        {
            switch (Message)
            {
                case "Buy":
                    Message = ":chart_with_upwards_trend: BUY " + StreamerName + " :chart_with_upwards_trend:";
                    break;
                case "Sell":
                    Message = ":chart_with_downwards_trend: SELL " + StreamerName + " :chart_with_downwards_trend:";
                    break;
                case "Hold":
                    Message = ":raised_hand: " + StreamerName + " :raised_hand:";
                    break;
                case "Warn":
                    Message = ":warning: " + StreamerName + " MIGHT BE RISKY; TRY 1000 SHARES :warning:";
                    break;
            }

            if (Message != null)
            {
                Clipboard.SetText(Message);
            }
        }
    }
}
