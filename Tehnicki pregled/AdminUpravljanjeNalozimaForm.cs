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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TehnickiPregled
{
    public partial class AdminUpravljanjeNalozimaForm : Form
    {
        public AdminUpravljanjeNalozimaForm()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AdminPanelForm apf = new AdminPanelForm();
            apf.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM login", conn);

            var reader = cmd.ExecuteReader();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("username", "Username");
            dataGridView1.Columns.Add("password", "Password");
            dataGridView1.Columns.Add("usertype", "User Type");

            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["username"], reader["password"], reader["usertype"]);
            }

            reader.Close();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO login (username, password, usertype) VALUES ('" + txtime.Text + "', '" + txtsifra.Text + "', '" + txttip.Text + "')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE login SET password = @password, usertype = @usertype WHERE username = @username", conn);

                cmd.Parameters.AddWithValue("@username", txtime.Text);
                cmd.Parameters.AddWithValue("@password", txtsifra.Text);
                cmd.Parameters.AddWithValue("@usertype", txttip.Text);

                cmd.ExecuteNonQuery();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM login WHERE username = '" + txtime.Text + "' AND password = '"+txtsifra.Text+"' AND usertype = '"+txttip.Text+"'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}