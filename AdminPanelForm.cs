using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TehnickiPregled
{
    public partial class AdminPanelForm : Form
    {
        public AdminPanelForm()
        {
            InitializeComponent();

            label1.Text = Properties.Settings.Default.logedinuser;
            label2.Text = Properties.Settings.Default.usertype;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GeneralnaStatistikaForm gsf = new GeneralnaStatistikaForm();
            gsf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminUpravljanjeNalozimaForm aunf = new AdminUpravljanjeNalozimaForm();
            aunf.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MijenjanjeSifreForm msf = new MijenjanjeSifreForm();
            msf.Show();
            this.Hide();
        }
    }
}
