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
            if (txtime.Text.Length > 0 && txtsifra.Text.Length > 8 && comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString().Length > 0)
            {
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                SqlCommand ncmd = new SqlCommand("select * from login where username='" + txtime.Text + "'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(ncmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count <= 0)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO login (username, password, usertype) VALUES ('" + txtime.Text + "', '" + txtsifra.Text + "', '" + comboBox1.SelectedItem.ToString() + "')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Napravljen je novi nalog");
                }
                else
                {
                    MessageBox.Show("Nalog sa tim korisnickim imenom vec postoji");
                }
            } else
            {
                MessageBox.Show("Svako polje mora biti popunjeno i sifra mora biti duga najmanje 8 karaktera");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE login SET password = @password, usertype = @usertype WHERE username = @username", conn);

                cmd.Parameters.AddWithValue("@username", txtime.Text);
                cmd.Parameters.AddWithValue("@password", txtsifra.Text);
                cmd.Parameters.AddWithValue("@usertype", comboBox1.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtime.Text != Properties.Settings.Default.logedinuser)
            {
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM login WHERE username = '" + txtime.Text + "' AND password = '" + txtsifra.Text + "' AND usertype = '" + comboBox1.SelectedItem.ToString() + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Izbrisali ste zeljeni nalog");
            } else
            {
                MessageBox.Show("Ne mozete izbrisati nalog na kojem ste trenutno ulogovani");
            }
        }

        private void AdminUpravljanjeNalozimaForm_Load(object sender, EventArgs e)
        {

        }
    }
}