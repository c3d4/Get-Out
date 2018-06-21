using System.Windows.Forms;
using Microsoft.Win32;

namespace Get_Out_V0._0._1
{
    public partial class Form1 : Form
    {
        RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);   //Reference Registry Key
        AppForm appForm = new AppForm();    //Reference AppForm
        public Form1()
        {
            InitializeComponent();
            reg.SetValue("Get Out!", Application.ExecutablePath.ToString());    //Make Launcher Run at Start-time
            this.Hide();    //Hide the launcher
            appForm.Show(); // This shows the form and returns immediately
            Application.Run(appForm); // Run a standard application message loop on the current thread
            this.Close();   //Close the Launcher
        }
    }
}
