using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Get_Out_V0._0._1
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        { 
            InitializeComponent();  //Make the form a form 
            this.ShowInTaskbar = false; //Dont show form in taskbar
            timerRGB.Start();   //Start updating themes when we move our slider 
        }

        #region Return Button
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(panel1.BackColor.R + 30, panel1.BackColor.G + 30, panel1.BackColor.B + 30);  //Give it a nice contrast color
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;  //Reset the color
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();   //Close the form 
        }

        #endregion

        #region C/F
        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            bunifuCheckbox1.Checked = true; //Check celsius 
            bunifuCheckbox2.Checked = false;    //Uncheck farhenheit 
            Properties.Settings.Default.Celsius = true; //Set celsius to true 
            Properties.Settings.Default.Farhenheit = false; //Set farhenheit to false
        }

        private void bunifuCheckbox2_OnChange(object sender, EventArgs e)
        {
            bunifuCheckbox1.Checked = false;                            //THE SAME THING AS THE BLOCK OF CODE ABOVE EXC. VICE VERSA
            bunifuCheckbox2.Checked = true;
            Properties.Settings.Default.Celsius = false;
            Properties.Settings.Default.Farhenheit = true;
        }
        #endregion

        #region Custom Themes
        private void timerRGB_Tick(object sender, EventArgs e)
        {
            labelR.Text = trackbarR.Value.ToString();   //Set the text = to the value of the slider
            labelG.Text = trackbarG.Value.ToString();   //Same til end of labels
            labelB.Text = trackbarB.Value.ToString();
            labelR2.Text = trackbarR2.Value.ToString();
            labelG2.Text = trackbarG2.Value.ToString();
            labelB2.Text = trackbarB2.Value.ToString();

            panel1.BackColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);   //Change the panel color to the high contrast one
            this.BackColor = Color.FromArgb(trackbarR2.Value, trackbarG2.Value, trackbarB2.Value);  //Change the form color to the low contrast one 

            trackbarR.IndicatorColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);   //Change color of slider *SO IT LOOKS COOL*
            trackbarR2.IndicatorColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);  //Same with code below 
            trackbarG.IndicatorColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);
            trackbarG2.IndicatorColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);
            trackbarB.IndicatorColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);
            trackbarB2.IndicatorColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);

            bunifuCheckbox1.CheckedOnColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);
            bunifuCheckbox2.CheckedOnColor = Color.FromArgb(trackbarR.Value, trackbarG.Value, trackbarB.Value);

        }
        #endregion

        #region Settings
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            trackbarR.Value = Properties.Settings.Default.R;    //Save the RGB settings to the RGB trackbars 
            trackbarG.Value = Properties.Settings.Default.G;    //Same below 
            trackbarB.Value = Properties.Settings.Default.B;
            trackbarR2.Value = Properties.Settings.Default.R2;
            trackbarG2.Value = Properties.Settings.Default.G2;
            trackbarB2.Value = Properties.Settings.Default.B2;

            bunifuCheckbox1.Checked = Properties.Settings.Default.Celsius;  //Save if C is checked
            bunifuCheckbox2.Checked = Properties.Settings.Default.Farhenheit;   //Same below exc. Vice Versa 
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.R = trackbarR.Value;    //Rewrite saved value to save settings for RGB
            Properties.Settings.Default.G = trackbarG.Value;    //Same below
            Properties.Settings.Default.B = trackbarB.Value;
            Properties.Settings.Default.R2 = trackbarR2.Value;
            Properties.Settings.Default.G2 = trackbarG2.Value;
            Properties.Settings.Default.B2 = trackbarB2.Value;

            Properties.Settings.Default.Celsius = bunifuCheckbox1.Checked;  //Rewrite C to save settings
            Properties.Settings.Default.Farhenheit = bunifuCheckbox2.Checked;   //Same below exc. Vice Versa 

            Properties.Settings.Default.Save(); //Save the new settings 
        }
        #endregion

        #region Bugs/Feedback
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://goo.gl/forms/zh7QPnvy9CSK3KW33");    //Go to poll link 
        }
        #endregion

        #region Premade Themes
        private void premadeBlue_Click(object sender, EventArgs e)
        {   
            trackbarR.Value = 0;    //Set to blue premade themes
            trackbarG.Value = 99;   //Same below    
            trackbarB.Value = 212;

            trackbarR2.Value = 34;
            trackbarG2.Value = 45;
            trackbarB2.Value = 57;
        }

        private void premadePink_Click(object sender, EventArgs e)
        {            
            trackbarR.Value = 225; //Set to orange premade themes
            trackbarG.Value = 127;   //Same below
            trackbarB.Value = 67;

            trackbarR2.Value = 53;
            trackbarG2.Value = 53;
            trackbarB2.Value = 53;
        }

        private void premadeCyan_Click(object sender, EventArgs e)
        {
            trackbarR.Value = 44;   //Set to cyan premade themes
            trackbarG.Value = 148;  //Same below 
            trackbarB.Value = 134;

            trackbarR2.Value = 43; 
            trackbarG2.Value = 53;
            trackbarB2.Value = 59;
        }

        private void premadeGreen_Click(object sender, EventArgs e)
        {
            trackbarR.Value = 132;   //Set to red premade themes
            trackbarG.Value = 18;  //Same below
            trackbarB.Value = 13;

            trackbarR2.Value = 20;
            trackbarG2.Value = 20;
            trackbarB2.Value = 20;
        }
        #endregion
    }
}
