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

namespace TehnickiPregled
{
    public partial class IzmjenaTerminaForm : Form
    {
        public IzmjenaTerminaForm()
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
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length == 13)
            {
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                int userInput;
                if (int.TryParse(textBox2.Text, out userInput) && textBox3.Text.All(char.IsDigit))
                {
                    SqlCommand ncmd = new SqlCommand("SELECT * FROM termini WHERE rednibroj = @userInput", conn);
                    ncmd.Parameters.AddWithValue("@userInput", userInput);
                    SqlDataAdapter sda = new SqlDataAdapter(ncmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("UPDATE termini SET imeklijenta = @imeklijenta, jmbg = @jmbg, vrijeme = @vrijeme WHERE rednibroj = @rednibroj", conn);

                        cmd.Parameters.AddWithValue("@imeklijenta", textBox1.Text);
                        cmd.Parameters.AddWithValue("@rednibroj", textBox2.Text);
                        cmd.Parameters.AddWithValue("@jmbg", textBox3.Text);
                        cmd.Parameters.AddWithValue("@vrijeme", dateTimePicker1.Value);


                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Memorisana je izmjena za zeljeni termin");
                    }
                    else
                    {
                        MessageBox.Show("Termin sa tim jedinstvenim rednim brojem ne postoji");
                    }
                }
                else
                {
                    MessageBox.Show("U polja za unos jedinstvenog rednog broja termina i jmbg broja klijenta smijete unijeti samo brojeve");
                }
            }
            else
            {
                MessageBox.Show("Morate popuniti sva polja i jmbg mora imati 13 cifara kako bi ste memorisali novi termin");
            }
        }
    }
}
