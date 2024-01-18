using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TehnickiPregled
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            SqlCommand cmd = new SqlCommand("select * from login where username='" + txtBoxUsername.Text + "' and password='" + txtBoxPassword.Text + "'", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["usertype"].ToString() == "user")
                {
                    Properties.Settings.Default.logedinuser = txtBoxUsername.Text;
                    Properties.Settings.Default.usertype = dt.Rows[0]["usertype"].ToString();
                    Properties.Settings.Default.Save();
                    MainForm mf = new MainForm();
                    mf.Show();
                    this.Hide();
                } else if (dt.Rows[0]["usertype"].ToString() == "admin")
                {
                    Properties.Settings.Default.logedinuser = txtBoxUsername.Text;
                    Properties.Settings.Default.usertype = dt.Rows[0]["usertype"].ToString();
                    Properties.Settings.Default.Save();
                    AdminPanelForm apf = new AdminPanelForm();
                    apf.Show();
                    this.Hide();
                }
            } else
            {
                MessageBox.Show("Pogresan unos. Provjerite podatke i pokusajte ponovo.");
            }
        }
    }
}
