using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TehnickiPregled
{
    public partial class MijenjanjeSifreForm : Form
    {
        public MijenjanjeSifreForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE login SET password = @newpassword WHERE username = @username AND password = @password", conn);

                cmd.Parameters.AddWithValue("@username", Properties.Settings.Default.logedinuser);
                cmd.Parameters.AddWithValue("@password", textBox1.Text);
                cmd.Parameters.AddWithValue("@newpassword", textBox2.Text);

                cmd.ExecuteNonQuery();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminPanelForm apf = new AdminPanelForm();
            apf.Show();
            this.Hide();
        }
    }
}
