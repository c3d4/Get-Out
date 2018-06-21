using System;
using System.Drawing;
using System.Windows.Forms;

namespace Get_Out_V0._0._1
{
    public partial class AppForm : Form
    {
        //Weather Variables
        string weatherC, weatherF;

        //Can Drag? Bool
        private bool _dragging;
        //Location of mouse
        private Point _startPoint = new Point(0, 0);

        //Create reference to the Settings Form
        SettingsForm settingsForm = new SettingsForm(); 

        public AppForm()
        {
            InitializeComponent();  //Make the form a form
            this.ShowInTaskbar = false; //Don't show program in taskbar
            timerGetTime.Start();   //Start getting the current UI Time
            timerTheme.Start(); //Start Updating theme
            timerCheckCF.Start();   //Start checking if celsius is true or not, same with farhenheit  
            updateWeather.Start();  //Start updating the weather every 5 minutes
            webBrowser1.ScriptErrorsSuppressed = true;  //Don't show any errors in the WebBrowser 
            webBrowser1.Navigate("https://www.google.com/?gws_rd=ssl#q=weather+");  //Fetch Weather 
        }

        #region Move Form
        //Whole Form
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                    {
                        m.Result = (IntPtr)0x2; 
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        //Top Form 
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            Point p = PointToScreen(e.Location);
            Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            Point p = PointToScreen(e.Location);
            Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            Point p = PointToScreen(e.Location);
            Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
        }
        #endregion

        #region X Button
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(panel1.BackColor.R + 30, panel1.BackColor.G + 30, panel1.BackColor.B + 30);   //Make a nice contrast color whenever we are inside of the label
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.Transparent; //Reset the color if we are outside the label 
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }
        #endregion

        #region Get Time and Date
        private void timerGetTime_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();  //Get the time every 100 ms
        }

        #endregion

        #region Get Weather
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            timerWeather.Start();   //Start the timer that gets the weather 
        }

        private void timerWeather_Tick(object sender, EventArgs e)
        {
            try
            {
                labelLoc.Text = webBrowser1.Document.GetElementById("wob_loc").InnerText;   //Gets Location
                labelDesc.Text = webBrowser1.Document.GetElementById("wob_dc").InnerText;   //Gets Mood
                labelTemp.Text = webBrowser1.Document.GetElementById("wob_tm").InnerText + "°C";   //Gets Temp C
                weatherC = webBrowser1.Document.GetElementById("wob_tm").InnerText;  //Gets check C
                labelTemp2.Text = webBrowser1.Document.GetElementById("wob_ttm").InnerText + "°F";   //Gets Temp F
                weatherF = webBrowser1.Document.GetElementById("wob_ttm").InnerText;   //Gets check F
                timerWeather.Stop();
            }
            catch (Exception f)    //Don't catch anything 
            {  

            }

            //Algorithm that checks the day, time, weather, then notifies you if it's good out 
            int time = DateTime.Now.Hour; //Gets the current hour in Military Time
                
            if (time >= 8 && time <= 17)    //Check if time is later than 9 AM yet before 5 PM
            {
                if (Properties.Settings.Default.Celsius == true)    //If it's celsius
                {
                    int intweatherC = Int32.Parse(weatherC);    //Convert the weather to an int
                    if (intweatherC <= 24 && intweatherC >= 20)
                    {
                        showBalloon("Get Out!", "The temperature is " + labelTemp.Text + "! It's " + labelDesc.Text + " outside!");
                    }

                }
                else //If it's farenheit
                {
                    int intweatherF = Int32.Parse(weatherF);    //Convert the weather to an int
                    if (intweatherF <= 75 && intweatherF >= 69)
                    {
                        showBalloon("Get Out!", "The temperature is " + labelTemp2.Text + "! It's " + labelDesc.Text + " outside!");
                    }
                }
            }
        }
        #endregion

        #region Settings Button
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(panel1.BackColor.R + 30, panel1.BackColor.G + 30, panel1.BackColor.B + 30);  //Highlight the background
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;  //Reset the color to what it was 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();  //Show the settings form  instead of the app
        }
        #endregion

        #region Update Theme
        private void timerTheme_Tick(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(Properties.Settings.Default.R, Properties.Settings.Default.G, Properties.Settings.Default.B);     //Update the theme from the settings
            this.BackColor = Color.FromArgb(Properties.Settings.Default.R2, Properties.Settings.Default.G2, Properties.Settings.Default.B2);    //Update the back theme from the settings
        }

        #endregion

        #region Update C/F
        private void timerCheckCF_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Celsius == true)    //If Celsius is true 
            {
                labelTemp.Visible = true;   //C is visible
                labelTemp2.Visible = false;   //F is invisible
                notifyIcon.Text = "It's currently " + labelTemp.Text + "!";
            }
            else
            {
                labelTemp.Visible = false;   //C is visible
                labelTemp2.Visible = true;   //F is invisible
                notifyIcon.Text = "It's currently " + labelTemp2.Text + "!";
            }
        }
        #endregion
        
        #region Update Weather
        private void updateWeather_Tick(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com/?gws_rd=ssl#q=weather+");  //Navigate to weather
        }
        #endregion

        #region Show Notification 
        private void showBalloon(string title, string body)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;

            if (title != null)
            {
                notifyIcon.BalloonTipTitle = title;
            }

            if (body != null)
            {
                notifyIcon.BalloonTipText = body;
            }
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.ShowBalloonTip(30000);
        }
        #endregion

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
                this.WindowState = FormWindowState.Normal;
        }
    }
}
