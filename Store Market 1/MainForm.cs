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
    public partial class MainForm : Form
    {
        #region MOve Code
        const int HT_CAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("آیا میخواهید خارج شوید ؟", "خروج", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
            {
                Application.Exit();
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            (new ManagerPanelForm()).ShowDialog();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void button2_Click(object sender, EventArgs e)
        {   //  اجناس فروشگاه
            ProductsForm productsForm= new ProductsForm();
            productsForm.AdminName.Text = AdminNameL.Text;
            this.WindowState = FormWindowState.Minimized;
            productsForm.ShowDialog();
            productsForm.WindowState = FormWindowState.Normal;

        }
    }
}
