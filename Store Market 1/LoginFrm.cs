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
    public partial class LoginFrm : Form
    {
        #region MOve Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public LoginFrm()
        {
            InitializeComponent();
        }
        Functions Fun = new Functions();
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new AdminRGForm()).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fun.LoginUser(new Admin
                {
                    Username = usertxt.Text,
                    Password = passtxt.Text,
                    accessCode = accesstxt.Text
                }))
                {
                    MessageBox.Show("به فروشگاه خوش آمدید");
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.AdminNameL.Text = accesstxt.Text;
                    (mainForm).ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show("نام و رمز کاربری اشتباه است ", "خطا در ورود");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("اطلاعات وارد نشده است");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            (new ResetPassForm()).ShowDialog();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {

        }

        private void LoginFrm_MouseDown(object sender, MouseEventArgs e)
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
    }
}
