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
    public partial class AdminInformationForm : Form
    {
        #region MOve Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public AdminInformationForm()
        {
            InitializeComponent();
        }
        
        Functions Fun = new Functions();

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(this.Text== "ویرایش اطلاعات")
            {
                int id = int.Parse(IDTXT.Tag.ToString());
                DBCode1 dbc = new DBCode1();
                Admin admin = dbc.admins.Where(c => c.id == id).FirstOrDefault();
                admin.Name = nametxt.Text;
                admin.Family = familytxt.Text;
                admin.Phone = Convert.ToInt64(phonetxt.Text);
                admin.Email = emailtxt.Text;
                admin.Address = addresstxt.Text;
                admin.Username = usernametxt.Text;
                admin.Password = userpasstxt.Text;
                admin.accessCode = useraccesstxt.Text;
                admin.RoleId = Fun.Role(roletxt.Text);
                admin.IsActive = Convert.ToBoolean(statustxt.Text == "فعال" ? 1 : 0);
                dbc.SaveChanges();
                Fun.ClearTextBoxes(this.Controls);
            }
            else
            {
                try
                {
                    #region RegisterCode
                    if (Fun.RegisterAdmin(new Admin
                    {
                        Name = nametxt.Text,
                        Family = familytxt.Text,
                        Phone = Convert.ToInt64(phonetxt.Text),
                        Email = emailtxt.Text,
                        Address = addresstxt.Text,
                        Username = usernametxt.Text,
                        Password = userpasstxt.Text,
                        accessCode = useraccesstxt.Text,
                        RoleId = Fun.Role(roletxt.Text),
                        IsActive = Convert.ToBoolean(statustxt.Text == "فعال" ? 1 : 0),
                    }))
                    {
                        StatusLable.Text = "ثبت نام با موفقیت انجام شد";
                        Fun.ClearTextBoxes(this.Controls);
                    }
                    else
                    {
                        MessageBox.Show("ثبت نام نا موفق بود","خطا در عملیات",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        StatusLable.Text = "ثبت نام نا موفق بود";
                    }
                    #endregion

                }
                catch (Exception)
                {
                    StatusLable.Text = "اطلاعات ذکر نشده است";
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminInformationForm_Load(object sender, EventArgs e)
        {

        }

        private void phonetxt_Leave(object sender, EventArgs e)
        {
            usernametxt.Text = phonetxt.Text;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            StatusLable.Text = "نمایش وضعیت";
        }

        private void AdminInformationForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void AdminInformationForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
