using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace ForexQuote
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private bool Hiddend = false;
        private NotifyIcon notifyIcon = null;
        private Timer time = null;
        private delegate void NoArgDelegate();
        private delegate void OneArgDelegate(string arg);

        public Window1()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 30;
            try
            {
                //托盘图标
                notifyIcon = new NotifyIcon();
                //notifyIcon.BalloonTipText = "Forex!";
                notifyIcon.Text = "Forex!";
                notifyIcon.Icon = new System.Drawing.Icon("favicon.ico");
                notifyIcon.Visible = true;
                notifyIcon.MouseDoubleClick += OnNotifyIconDoubleClick;
                notifyIcon.ShowBalloonTip(1000);
            }
            catch { }
            this.time = new Timer();
            time.Interval = 30000;
            time.Tick += new EventHandler(time_Tick);
            time_Tick(null, null);
            time.Enabled = true;
        }

        private void time_Tick(object sender, EventArgs e)
        {
            NoArgDelegate nad = new NoArgDelegate(search);
            nad.BeginInvoke(null, null);
        }

        private void search()
        {
            string strNum = "0.0000";
            try
            {
                WebRequest request = WebRequest.Create("http://qq.ip138.com/hl.asp?from=EUR&to=USD&q=1");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));

                string html = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();

                int iStart = html.IndexOf("<td>1</td><td>") + 14;
                int iEnd = html.IndexOf("</td>", iStart);
                if (iStart > 0 && iEnd > iStart)
                {
                    strNum = html.Substring(iStart, iEnd - iStart);
                }
            }
            catch { strNum = "0.0000"; }
            try
            {
                double.Parse(strNum);
            }
            catch { strNum = "0.0001"; }

            panel.Dispatcher.BeginInvoke(new OneArgDelegate(output), strNum);
        }

        private void output(string strNum)
        {
            if (text1.Text == "")
            {
                text1.Opacity = 1;
                text1.Text = strNum;
            }
            else if (text2.Text == "")
            {
                text1.Opacity = 0.9;
                text2.Opacity = 1;
                text2.Text = strNum;
                try
                {
                    double d1 = double.Parse(text1.Text);
                    double d2 = double.Parse(text2.Text);
                    if (d1 > d2)
                    {
                        text2.Foreground = Brushes.Red;
                    }
                    else if (d1 < d2)
                    {
                        text2.Foreground = Brushes.Green;
                    }
                }
                catch { }
            }
            else if (text3.Text == "")
            {
                text1.Opacity = 0.8;
                text2.Opacity = 0.9;
                text3.Opacity = 1.0;
                text3.Text = strNum;
                try
                {
                    double d1 = double.Parse(text2.Text);
                    double d2 = double.Parse(text3.Text);
                    if (d1 > d2)
                    {
                        text3.Foreground = Brushes.Red;
                    }
                    else if (d1 < d2)
                    {
                        text3.Foreground = Brushes.Green;
                    }
                }
                catch { }
            }
            else if (text4.Text == "")
            {
                text1.Opacity = 0.7;
                text2.Opacity = 0.8;
                text3.Opacity = 0.9;
                text4.Opacity = 1;
                text4.Text = strNum;
                try
                {
                    double d1 = double.Parse(text3.Text);
                    double d2 = double.Parse(text4.Text);
                    if (d1 > d2)
                    {
                        text4.Foreground = Brushes.Red;
                    }
                    else if (d1 < d2)
                    {
                        text4.Foreground = Brushes.Green;
                    }
                }
                catch { }
            }
            else if (text5.Text == "")
            {
                text1.Opacity = 0.6;
                text2.Opacity = 0.7;
                text3.Opacity = 0.8;
                text4.Opacity = 0.9;
                text5.Opacity = 1;
                text5.Text = strNum;
                try
                {
                    double d1 = double.Parse(text4.Text);
                    double d2 = double.Parse(text5.Text);
                    if (d1 > d2)
                    {
                        text5.Foreground = Brushes.Red;
                    }
                    else if (d1 < d2)
                    {
                        text5.Foreground = Brushes.Green;
                    }
                }
                catch { }
            }
            else if (text6.Text == "")
            {
                text1.Opacity = 0.2;
                text2.Opacity = 0.3;
                text3.Opacity = 0.4;
                text4.Opacity = 0.6;
                text5.Opacity = 0.8;
                text6.Opacity = 1;
                text6.Text = strNum;
                try
                {
                    double d1 = double.Parse(text5.Text);
                    double d2 = double.Parse(text6.Text);
                    if (d1 > d2)
                    {
                        text6.Foreground = Brushes.Red;
                    }
                    else if (d1 < d2)
                    {
                        text6.Foreground = Brushes.Green;
                    }
                }
                catch { }
            }
            else
            {
                text1.Foreground = text2.Foreground;
                text1.Text = text2.Text;

                text2.Foreground = text3.Foreground;
                text2.Text = text3.Text;

                text3.Foreground = text4.Foreground;
                text3.Text = text4.Text;

                text4.Foreground = text5.Foreground;
                text4.Text = text5.Text;

                text5.Foreground = text6.Foreground;
                text5.Text = text6.Text;

                text6.Text = strNum;
                try
                {
                    double d1 = double.Parse(text5.Text);
                    double d2 = double.Parse(text6.Text);
                    if (d1 > d2)
                    {
                        text6.Foreground = Brushes.Red;
                    }
                    else if (d1 < d2)
                    {
                        text6.Foreground = Brushes.Green;
                    }
                    else
                    {
                        text6.Foreground = Brushes.Black;
                    }
                }
                catch { }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                try
                {
                    notifyIcon.Dispose();
                }
                catch { }
                this.Close();
            }
        }
        //托盘图标双击
        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            if (this.Hiddend)
            {
                this.Show();
                this.Hiddend = false;
                this.time.Start();
            }
            else
            {
                this.Hide();
                this.Hiddend = true;
                this.time.Stop();
            }
            
        }
    }
}
