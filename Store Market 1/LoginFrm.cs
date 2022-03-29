using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Market_1
{
    public partial class LoginFrm : Form
    {
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
            if(Fun.Enter(new Admin
            {
                Username = usertxt.Text,
                Password = passtxt.Text,
                accessCode = accesstxt.Text
            }))
            {
                MessageBox.Show("به فروشگاه خوش آمدید");
                this.Hide();
                (new MainForm()).ShowDialog();
            }
            else
            {
                MessageBox.Show("نام و رمز کاربری اشتباه است ","خطا در ورود");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            (new ResetPassForm()).ShowDialog();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {

        }
    }
}
