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
    public partial class CompanyManagerForm : Form
    {
        #region Move Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public CompanyManagerForm()
        {
            InitializeComponent();
        }

        Functions Fun = new Functions();
        bool Sw = true;
        int id=-1;

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Company = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String Manager = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            if (DialogResult.Yes == MessageBox.Show("آیا اطلاعات\n"+ Company + "\nاز\n" + Manager + "\nویرایش شود ؟", "تایید درخواست", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                savebtn.Text = "بروزرسانی";
                nametxt.Tag = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                nametxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                managertxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                phonetxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                emailtxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                addresstxt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                sitetxt.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                statustxt.Text = (dataGridView1.CurrentRow.Cells[1].Value.ToString()) == "true" ? "فعال" :"غیر فعال";

                Sw = false;
            }

            contextMenuStrip1.Enabled = false;

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Company= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String Manager= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
            if (DialogResult.Yes == MessageBox.Show("آیا اطلاعات\n" + Company + "\nاز\n" + Manager + "\nحذف شود ؟", "تایید درخواست", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                DBCode1 dbc = new DBCode1();
                Company co = dbc.companies.Where(c => c.id == id).FirstOrDefault();
                dbc.companies.Remove(co);
                dbc.SaveChanges();
                StatusLable.Text = "اطلاعات مورد نظر حذف شد";
                dataGridView1.DataSource = (new DBCode1().companies).ToList();
            }
            contextMenuStrip1.Enabled = false;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sw && Fun.RegisterCompany(new Company
                {
                    CompanyName = nametxt.Text,
                    CompanyManager = managertxt.Text,
                    Phone = Int64.Parse(phonetxt.Text),
                    Email = emailtxt.Text,
                    Address = addresstxt.Text,
                    Site = sitetxt.Text,
                    Status = ((statustxt.Text) == "فعال" ? true : false)
                }))
                {

                    StatusLable.Text = "اطلاعات ذخیره شد";
                    Fun.ClearTextBoxes(this.Controls);
                    dataGridView1.DataSource = (new DBCode1().companies).ToList();
                }
                else if (!Sw)
                {// ویرایش اطلاعات
                    int id = int.Parse(nametxt.Tag.ToString());

                    DBCode1 dbc = new DBCode1();
                    Company company = dbc.companies.Where(co => co.id == id).FirstOrDefault();

                    company.CompanyName = nametxt.Text;
                    company.CompanyManager = managertxt.Text;
                    company.Phone = Int64.Parse(phonetxt.Text);
                    company.Email = emailtxt.Text;
                    company.Address = addresstxt.Text;
                    company.Site = sitetxt.Text;
                    company.Status = ((statustxt.Text) == "فعال" ? true : false);
                    dbc.SaveChanges();

                    Fun.ClearTextBoxes(this.Controls);
                    dataGridView1.DataSource = (new DBCode1().companies).ToList();

                    StatusLable.Text = "اطلاعات مورد نظر بروزرسانی شد";
                    savebtn.Text = "ذخیره";
                    Sw = true;
                }
                else
                {
                    StatusLable.Text = "اطلاعات تکراری است";
                }
            }
            catch (Exception)
            {
                Fun.ClearTextBoxes(this.Controls);
                StatusLable.Text = "اطلاعات درست نیست";
            }
        }

        private void CompanyManagerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (new DBCode1().companies).ToList();
            contextMenuStrip1.Enabled = false;

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void CompanyManagerForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
