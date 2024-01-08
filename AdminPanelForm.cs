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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminPrikazStatistikeForm apsf = new AdminPrikazStatistikeForm();
            apsf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminUpravljanjeNalozimaForm aunf = new AdminUpravljanjeNalozimaForm();
            aunf.Show();
            this.Hide();
        }
    }
}
