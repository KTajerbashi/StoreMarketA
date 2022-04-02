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
using System.Data.SqlClient;
namespace Store_Market_1
{
    public partial class Agentfrm : Form
    {
        #region MOve Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public Agentfrm()
        {
            InitializeComponent();
        }
        DBCode1 dbc = new DBCode1();
        Functions Fun = new Functions();
        bool Sw = true;
        bool SS = false;
        int ID = -1;
        public void ShowCompanyName()
        {
            SqlConnection conn = new SqlConnection("Data Source=TAJERBASHI;Initial Catalog=StoreMarketDB;Integrated Security=True");
            string query = "SELECT Companyname FROM [Companies]";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Open();
            cmd.ExecuteScalar();
            conn.Close();
            companytxt.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                companytxt.Items.Add(dt.Rows[i]["Companyname"]);
            }
        }
        private void Agentfrm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void Agentfrm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (new DBCode1().agents).ToList();
            dataGridView2.DataSource = (new DBCode1().companies).ToList();
            dataGridView1.ForeColor = Color.Black;
            dataGridView2.ForeColor = Color.Black;
            contextMenuStrip1.Enabled = false;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (new DBCode1().agents).ToList();
            dataGridView2.DataSource = (new DBCode1().companies).ToList();

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void companytxt_DropDown(object sender, EventArgs e)
        {
            companytxt.Items.Clear();
            ShowCompanyName();
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ID == -1)
            {
                contextMenuStrip1.Enabled = false;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            else
            {
                Sw = true;
                ID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                contextMenuStrip1.Enabled = true;
            }
        }
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (ID == -1)
            {
                contextMenuStrip1.Enabled = false;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            else
            {
                Sw = false;
                ID = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
                contextMenuStrip1.Enabled = true;
            }
        }
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Sw)
            {
                String AName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                String AFamily = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("آیا اطلاعات " + AName + " " + AFamily + " ویرایش شود ؟", "تایید درخواست", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {   // Agent
                    
                    SS = true;
                    Asavebtn.Text = "بروزرسانی";

                    nametxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    familytxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    phonetxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    companytxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    statusID.Text = (dataGridView1.CurrentRow.Cells[1].Value.ToString()) == "true" ? "فعال" : "غیر فعال";
                    
                }
            }
            else
            {   //  Company
                String Company = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                String Manager = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("آیا اطلاعات\n" + Company + "\nاز\n" + Manager + "\nویرایش شود ؟", "تایید درخواست", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SS = true;
                    Csavebtn.Text = "بروزرسانی";

                    var com = from i in dbc.companies where i.id == ID select i;

                    foreach(var item in com)
                    {
                        comNametxt.Text = item.CompanyName;
                        mangNametxt.Text = item.CompanyManager;
                        comPhontxt.Text = (item.Phone).ToString();
                        comStatustxt.Text = (item.Status.ToString()) == "true" ? "فعال" : "غیر فعال";
                        emailtxt.Text =item.Email;
                        Addresstxt.Text = item.Address;
                        sitetxt.Text = item.Site;
                    }
                }
            }
            

            
            contextMenuStrip1.Enabled = false;
        }
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Sw)
            {   // Agent
                String AName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                String AFamily = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("آیا اطلاعات " + AName + " " + AFamily + " حذف شود ؟", "تایید درخواست", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    Agent agent = dbc.agents.Where(c => c.Id == ID).FirstOrDefault();
                    dbc.agents.Remove(agent);
                    dbc.SaveChanges();
                }
            }
            else
            {   //  Company
                String Company = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                String Manager = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("آیا اطلاعات\n" + Company + "\nاز\n" + Manager + "\nحذف شود ؟", "تایید درخواست", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    Company company = dbc.companies.Where(c => c.id == ID).FirstOrDefault();
                    dbc.companies.Remove(company);
                    dbc.SaveChanges();
                }
            }

            contextMenuStrip1.Enabled = false;

        }

        private void تغییروضعیتToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            if (Sw)
            {// Agent
                String StatusNow = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Agent agent = dbc.agents.Where(c => c.Id == ID).FirstOrDefault();
                agent.IsActive = StatusNow == "True" ? false : true;
                dbc.SaveChanges();
                dataGridView1.DataSource = dbc.agents.ToList();

            }
            else
            {
                String StatusNow = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                Company company = dbc.companies.Where(c => c.id == ID).FirstOrDefault();
                company.Status = StatusNow == "True" ? false : true;
                dbc.SaveChanges();
                dataGridView2.DataSource = dbc.companies.ToList();

            }
            contextMenuStrip1.Enabled = false;
            Sw = false;
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            DBCode1 dbc = new DBCode1();
            if (ComCheck.Checked && AgentCheck.Checked)
            {
                var q = from item in dbc.agents where (item.Name).Contains(searchtxt.Text) || (item.Family).Contains(searchtxt.Text) select item;
                dataGridView1.DataSource = q.ToList();
                var h = from item in dbc.companies where (item.CompanyName).Contains(searchtxt.Text) || (item.CompanyManager).Contains(searchtxt.Text) select item;
                dataGridView2.DataSource = h.ToList();
            }
            else if (ComCheck.Checked)
            {
                var h = from item in dbc.companies where (item.CompanyName).Contains(searchtxt.Text) || (item.CompanyManager).Contains(searchtxt.Text) select item;
                dataGridView2.DataSource = h.ToList();
            }
            else if (AgentCheck.Checked)
            {
                var q = from item in dbc.agents where (item.Name).Contains(searchtxt.Text) || (item.Family).Contains(searchtxt.Text) select item;
                dataGridView1.DataSource = q.ToList();
            }
        }

        private void Asavebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SS && Fun.RegisterAgent(new Agent
                {
                    Name = nametxt.Text,
                    Family = familytxt.Text,
                    Phone = Int64.Parse(phonetxt.Text),
                    CompanyName = companytxt.Text,
                    IsActive = (statusID.Text) == "فعال" ? true : false
                }))
                {
                    StatusLable.Text = "اطلاعات ثبت شد";
                    
                }else if (SS)
                {
                    Agent agent = dbc.agents.Where(c => c.Id == ID).FirstOrDefault();
                    var CO=from i in dbc.companies where i.CompanyName == companytxt.Text select i;
                    foreach(var item in CO)
                    {
                        agent.CompanyID = item.id;
                    }
                    agent.Name = nametxt.Text;
                    agent.Family = familytxt.Text;
                    agent.Phone = Int64.Parse(phonetxt.Text);
                    agent.CompanyName = companytxt.Text;
                    agent.IsActive = (statusID.Text) == "فعال" ? true : false;
                    dbc.SaveChanges();
                    Csavebtn.Text = "ذخیره";
                    Asavebtn.Text = "ذخیره";
                    StatusLable.Text = "اطلاعات بروزرسانی شد";
                    Fun.ClearTextBoxes(this.Controls);
                    SS = false;
                }
                else
                {
                    StatusLable.Text = "اطلاعات تکراری است";

                }
            }
            catch (Exception)
            {
                StatusLable.Text = "اطلاعات درست نمی باشد";

            }
            dataGridView1.DataSource = (new DBCode1().agents).ToList();
        }

        private void companytxt_DropDown_1(object sender, EventArgs e)
        {
            ShowCompanyName();
        }

        private void Csavebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SS && Fun.RegisterCompany(new Company
                {
                    CompanyName = comNametxt.Text,
                    CompanyManager = mangNametxt.Text,
                    Phone = Int64.Parse(comPhontxt.Text),
                    Email = emailtxt.Text,
                    Address = Addresstxt.Text,
                    Site = sitetxt.Text,
                    Status = (comStatustxt.Text) == "فعال" ? true : false
                }))
                {
                    StatusLable.Text = "اطلاعات ثبت شد";
                    
                }else if (SS)
                {
                    Company company = dbc.companies.Where(c => c.id == ID).FirstOrDefault();
                    
                    company.CompanyName = comNametxt.Text;
                    company.CompanyManager = mangNametxt.Text;
                    company.Phone = Int64.Parse(comPhontxt.Text);
                    company.Email = emailtxt.Text;
                    company.Address = Addresstxt.Text;
                    company.Site = sitetxt.Text;
                    company.Status = (comStatustxt.Text) == "فعال" ? true : false;
                    dbc.SaveChanges();
                    Csavebtn.Text = "ذخیره";
                    Asavebtn.Text = "ذخیره";
                    StatusLable.Text = "اطلاعات بروزرسانی شد";
                    Fun.ClearTextBoxes(this.Controls);
                    SS = false;
                }
                else
                {
                    StatusLable.Text = "اطلاعات تکراری است";
                }
                dataGridView2.DataSource = (new DBCode1().companies).ToList();
            }
            catch (Exception)
            {
                StatusLable.Text = "اطلاعات درست نمی باشد";
            }
        }
    }
}
