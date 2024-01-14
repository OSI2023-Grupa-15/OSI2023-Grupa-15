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
    public partial class UpravljanjeTerminimaForm : Form
    {
        public UpravljanjeTerminimaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
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
                dataGridView1.Columns.Add("radnik", "Radnik");

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["rednibroj"], reader["imeklijenta"], reader["jmbg"], reader["vrijeme"], reader["status"], reader["radnik"]);
                }

                dataGridView1.Sort(dataGridView1.Columns["vrijeme"], ListSortDirection.Ascending);
                reader.Close();
                conn.Close();
            } else if (checkBox1.Checked)
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
                dataGridView1.Columns.Add("radnik", "Radnik");

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["rednibroj"], reader["imeklijenta"], reader["jmbg"], reader["vrijeme"], reader["status"], reader["radnik"]);
                }

                dataGridView1.Sort(dataGridView1.Columns["vrijeme"], ListSortDirection.Ascending);
                reader.Close();
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                    if (dt.Rows.Count <= 0)
                    {
                        TimeSpan trajanjeTermina = new TimeSpan(0, 45, 0);
                        DateTime izabraniKrajTermina = dateTimePicker1.Value.Add(trajanjeTermina);
                        SqlCommand ncmd2 = new SqlCommand("SELECT * FROM termini WHERE (vrijeme < '"+dateTimePicker1.Value+"' AND krajtermina > '"+dateTimePicker1.Value+"') OR (vrijeme > '"+dateTimePicker1.Value+"' AND vrijeme < '"+izabraniKrajTermina+"')", conn);
                        SqlDataAdapter sda2 = new SqlDataAdapter(ncmd2);
                        DataTable dt2 = new DataTable();
                        sda2.Fill(dt2);
                        if (dt2.Rows.Count <= 0) {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("INSERT INTO termini (imeklijenta, vrijeme, krajtermina, status, jmbg, rednibroj) VALUES ('" + textBox1.Text + "', '" + dateTimePicker1.Value + "', '"+dateTimePicker1.Value.Add(trajanjeTermina)+"', 'neobradjen', '" + textBox3.Text + "', '" + textBox2.Text + "')", conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Memorisan je novi zeljeni termin");
                        } else
                        {
                            MessageBox.Show("Izabrani termin se preklapa sa vec postojecim terminima");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Termin sa tim jedinstvenim rednim brojem vec postoji");
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            BrisanjeTerminaForm btf = new BrisanjeTerminaForm();
            btf.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IzmjenaTerminaForm itf = new IzmjenaTerminaForm();
            itf.Show();
            this.Hide();
        }
    }
}
