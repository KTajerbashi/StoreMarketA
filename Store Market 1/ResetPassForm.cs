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
    public partial class ResetPassForm : Form
    {
        public ResetPassForm()
        {
            InitializeComponent();
        }
        Functions Fun = new Functions();
        private void button1_Click(object sender, EventArgs e)
        {
            if (Fun.CheckAdmin(new Admin
            {
                Username = AdminTxt.Text,
                Phone = Convert.ToInt64(AdminPhontxt.Text),
                accessCode = AddAcctxt.Text
            }))
            {
                MessageBox.Show("ادمین تایید کرد");
                groupBox2.Enabled = true;
            }
            else
            {
                MessageBox.Show("اطلاعات اشتباه است");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String Result = Fun.CheckColeader(new Admin
            {
                Username = usernametxt.Text,
                accessCode = useraccesstxt.Text,
                Phone = Convert.ToInt64(userphonetxt.Text),
                Email = useremailtxt.Text
            });
            if (Result=="0")
            {
                MessageBox.Show("اطلاعات اشتباه است","خطا");
            }
            else
            {
                DialogResult R = MessageBox.Show("آیا میخواهید رمز را عوض کنید؟", "تایید اطلاعات", MessageBoxButtons.YesNo);
                label11.Text = Result;
                if (R == DialogResult.Yes)
                {
                    groupBox3.Enabled = true;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(pass1txt.Text == pass2txt.Text)
            {
                if(Fun.Newpass(usernametxt.Text, pass1txt.Text))
                {
                    MessageBox.Show("رمز با موفقیت عوض شد!!!");
                    Fun.ClearTextBoxes(this.Controls);
                    groupBox2.Enabled = false;
                }
                
            }
            else
            {
                MessageBox.Show("رمز همخوانی ندارد", "خطا ورودی", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetPassForm_Load(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }
    }
}
