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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TehnickiPregled
{
    public partial class BrisanjeTerminaForm : Form
    {
        public BrisanjeTerminaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpravljanjeTerminimaForm utf = new UpravljanjeTerminimaForm();
            utf.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM termini", conn);

                var reader = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("rednibroj", "Jedinstveni br. termina");
                dataGridView1.Columns.Add("imeklijenta", "Ime klijenta");
                dataGridView1.Columns.Add("jmbg", "Jmbg");
                dataGridView1.Columns.Add("vrijeme", "Vrijeme termina");
                dataGridView1.Columns.Add("status", "Status termina");

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["rednibroj"], reader["imeklijenta"], reader["jmbg"], reader["vrijeme"], reader["status"]);
                }

                dataGridView1.Sort(dataGridView1.Columns["vrijeme"], ListSortDirection.Ascending);
                reader.Close();
                conn.Close();
            }
            else if (checkBox1.Checked)
            {
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM termini WHERE status = 'neobradjen'", conn);

                var reader = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("rednibroj", "Jedinstveni br. termina");
                dataGridView1.Columns.Add("imeklijenta", "Ime klijenta");
                dataGridView1.Columns.Add("jmbg", "Jmbg");
                dataGridView1.Columns.Add("vrijeme", "Vrijeme termina");
                dataGridView1.Columns.Add("status", "Status termina");

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["rednibroj"], reader["imeklijenta"], reader["jmbg"], reader["vrijeme"], reader["status"]);
                }

                dataGridView1.Sort(dataGridView1.Columns["vrijeme"], ListSortDirection.Ascending);
                reader.Close();
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.All(char.IsDigit))
            {
                using (SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM termini WHERE rednibroj = @rednibroj", conn))
                    {
                        cmd.Parameters.AddWithValue("@rednibroj", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Izbrisali ste zeljeni termin");
                }
            } else
            {
                MessageBox.Show("Niste unijeli validnu vrijednost u polje, pokusajte ponovo");
            }
        }
    }
}
