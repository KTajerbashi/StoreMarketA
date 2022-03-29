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
    public partial class AdminRGForm : Form
    {
        public AdminRGForm()
        {
            InitializeComponent();
        }
        Functions Fun = new Functions();

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            (new RoleAccessForm()).ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Fun.selectStatus(nametxt.Text, familytxt.Text))
            {
                MessageBox.Show("کاربر موجود است");
            }
            else
            {
                MessageBox.Show("کاربر موجود نیست");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Fun.RegisterAdmin(new Admin
            {
                Name = nametxt.Text,
                Family = familytxt.Text,
                RoleId = Fun.Role(comboBox1.Text),
                Phone = Convert.ToInt64(phonetxt.Text),
                Email = emailtxt.Text,
                Address=addresstxt.Text,
                IsActive=Convert.ToBoolean(comboBox2.Text == "فعال" ? 1 : 0),
                accessCode= Fun.Role(comboBox1.Text)==1 ? accesstxt.Text : "@Coleader",
                Username=usertxt.Text,
                Password=passtxt.Text
            }))
            {
                MessageBox.Show("با موفقیت ثبت نام شدید");
                Fun.ClearTextBoxes(this.Controls);
            }
            else
            {
                MessageBox.Show("ثبت نام ناموفق بود");
            }

        }
        private void phonetxt_Leave(object sender, EventArgs e)
        {
            usertxt.Text = phonetxt.Text;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Fun.AccessCode(accesstxt.Text))
            {
                MessageBox.Show("کد دسترسی تایید شد");
            }
            else
            {
                MessageBox.Show("کد دسترسی اشتباه است","خطا");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
