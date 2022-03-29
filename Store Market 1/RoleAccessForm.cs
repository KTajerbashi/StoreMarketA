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
    public partial class RoleAccessForm : Form
    {
        public RoleAccessForm()
        {
            InitializeComponent();
        }
        Functions Fun = new Functions();
        private void button1_Click(object sender, EventArgs e)
        {
            RoleAccess role = new RoleAccess();
            if (textBox2.Text == textBox3.Text && textBox1.Text==role.access)
            {
                DBCode1 dbc = new DBCode1();
                role.access = textBox2.Text;
                dbc.roleAccesses.Add(role);
                dbc.SaveChanges();
                this.Text = "ذخیره شد";
                Fun.ClearTextBoxes(this.Controls);
            }
            else
            {
                MessageBox.Show("ناموفق بود");
            }
        }
    }
}
