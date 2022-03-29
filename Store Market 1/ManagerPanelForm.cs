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
    public partial class ManagerPanelForm : Form
    {
        public ManagerPanelForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new AdminManagerForm()).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {   //باشگاه مشتریان

        }
    }
}
