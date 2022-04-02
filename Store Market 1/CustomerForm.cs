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
    public partial class CustomerForm : Form
    {
        #region MOve Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public CustomerForm()
        {
            InitializeComponent();
        }

        Functions Fun = new Functions();
        static int id;
        static bool Sw=true;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (new DBCode1()).customers.ToList();
            contextMenuStrip1.Enabled = false;

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            try{
                if (Fun.RegisterCustomer(new Customer
                {
                    Name = nametxt.Text,
                    Family = familytxt.Text,
                    Phone = Int64.Parse(phonetxt.Text),
                    BuyCost = Double.Parse(Buytxt.Text),
                    RoleId = (Roletxt.Text) == "مشتری" ? 5 : 4,
                    IsActive = ((statustxt.Text) == "فعال" ? true : false),
                    Percent = int.Parse(percenttxt.Text)
                }) && Sw)
                {
                    StatusLable.Text = "ثبت نام با موفقیت انجام شد";
                    Fun.ClearTextBoxes(this.Controls);
                }
                else if (!Sw)
                {
                    DBCode1 dbc = new DBCode1();
                    Customer customer = dbc.customers.Where(c => c.id == id).FirstOrDefault();
                    customer.Name = nametxt.Text;
                    customer.Family = familytxt.Text;
                    customer.Phone = Int64.Parse(phonetxt.Text);
                    customer.BuyCost = Double.Parse(Buytxt.Text);
                    customer.RoleId = (Roletxt.Text) == "مشتری" ? 5 : 4;
                    customer.IsActive = ((statustxt.Text) == "فعال" ? true : false);
                    customer.Percent = int.Parse(percenttxt.Text);
                    dbc.SaveChanges();
                    savebtn.Text = "ذخیره";
                    Sw = true;
                    StatusLable.Text = "بروزرسانی شد";
                    Fun.ClearTextBoxes(this.Controls);
                }
                else
                {
                    StatusLable.Text = "عملیات ناموفق";
                }
                dataGridView1.DataSource = (new DBCode1().customers).ToList();
            }
            catch
            {
                MessageBox.Show("اطلاعاتی وارد نشده است");
            }
        }

        private void CustomerForm_MouseClick(object sender, MouseEventArgs e)
        {
            StatusLable.Text = "پنل مشتریان";
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (id == -1)
            {
                contextMenuStrip1.Enabled = false;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            else
            {

                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                contextMenuStrip1.Enabled = true;
            }
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            String name = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String Family = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
            if (DialogResult.Yes== MessageBox.Show("ایا میخواهید " + name + " " + Family + " را ویرایش کنید؟", "تایید درخواست", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                nametxt.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
                familytxt.Text= dataGridView1.CurrentRow.Cells[3].Value.ToString();
                phonetxt.Text= dataGridView1.CurrentRow.Cells[4].Value.ToString();
                Buytxt.Text= dataGridView1.CurrentRow.Cells[5].Value.ToString();
                Roletxt.Text= (dataGridView1.CurrentRow.Cells[6].Value.ToString()) == "5" ? "مشتری" : "مشتری ویژه";
                statustxt.Text= (dataGridView1.CurrentRow.Cells[1].Value.ToString()) == "true" ? "فعال" : "غیر فعال"; 
                percenttxt.Text= dataGridView1.CurrentRow.Cells[7].Value.ToString();
                savebtn.Text = "بروزرسانی"; 
                Sw = false;
            }
           

            contextMenuStrip1.Enabled = false;
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            String name = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String Family = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
           if(DialogResult.Yes == MessageBox.Show("ایا میخواهید " + name + " " + Family + " را حذف کنید؟", "تایید درخواست", MessageBoxButtons.YesNo,MessageBoxIcon.Question))
            {
                DBCode1 dbc = new DBCode1();
                Customer cu = dbc.customers.Where(c => c.id == id).FirstOrDefault();
                dbc.customers.Remove(cu);
                dbc.SaveChanges();
                dataGridView1.DataSource = (new DBCode1().customers).ToList();
                StatusLable.Text = name + " " + Family + " حذف شد  ";

            }

            contextMenuStrip1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (new DBCode1().customers).Where(c => c.IsActive == true).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (new DBCode1().customers).ToList();
        }

        private void CustomerForm_MouseDown(object sender, MouseEventArgs e)
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
