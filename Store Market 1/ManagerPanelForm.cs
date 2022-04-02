using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Store_Market_1
{
    public partial class ManagerPanelForm : Form
    {
        #region MOve Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion
        public ManagerPanelForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   //مدیریت ادمین ها
            (new AdminManagerForm()).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {   //باشگاه مشتریان
            (new CustomerForm()).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {   // مدیریت شرکت ها
            (new CompanyManagerForm()).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {   // مدیریت نماینده ها
            (new Agentfrm()).ShowDialog();
        }

        private void ManagerPanelForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}
