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
    public partial class AdminManagerForm : Form
    {
        #region MOve Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public AdminManagerForm()
        {
            InitializeComponent();
        }
        AdminInformationForm adminfrm = new AdminInformationForm();
        private void AdminManagerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (new DBCode1()).admins.ToList();
            button2.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.IndianRed;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            DBCode1 dbc = new DBCode1();
            Admin admin = dbc.admins.Where(c => c.id == id).FirstOrDefault();
            if (admin.IsActive)
            {
                admin.IsActive = false;
            }
            else
            {
                admin.IsActive = true;
            }
            
            dbc.SaveChanges();
            dataGridView1.DataSource = (new DBCode1()).admins.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Save BTN
            (new AdminInformationForm()).ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;

            adminfrm.IDTXT.Tag = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            adminfrm.statustxt.Text = (dataGridView1.CurrentRow.Cells[1].Value.ToString()) == "true" ? "فعال":"غیر فعال";
            adminfrm.nametxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            adminfrm.familytxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            adminfrm.phonetxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            adminfrm.emailtxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            adminfrm.usernametxt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            adminfrm.userpasstxt.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            adminfrm.useraccesstxt.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            adminfrm.roletxt.Text = ((dataGridView1.CurrentRow.Cells[9].Value.ToString()) == "1" ? "ادمین" : "فروشنده");
            adminfrm.addresstxt.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            adminfrm.Text = "ویرایش اطلاعات";

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Edit BTN
            //MessageBox.Show(Convert.ToString(adminfrm.nametxt.Tag));
            if (adminfrm.IDTXT.Tag != null)
            {
                (adminfrm).ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            DBCode1 dbc = new DBCode1();
            Admin admin = dbc.admins.Where(c => c.id == id).FirstOrDefault();
            dbc.admins.Remove(admin);
            dbc.SaveChanges();
            dataGridView1.DataSource = dbc.admins.ToList();
        }

        private void AdminManagerForm_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void AdminManagerForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
