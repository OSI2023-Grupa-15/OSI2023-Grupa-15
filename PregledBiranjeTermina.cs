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
    public partial class PregledBiranjeTermina : Form
    {
        public PregledBiranjeTermina()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }

        private void PregledBiranjeTermina_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0)
            {
                MessageBox.Show("Morate unijeti neku vrijednost u polje");
            } else if (!textBox1.Text.All(char.IsDigit))
            {
                MessageBox.Show("U polje smijete unijeti samo brojeve");
            }
            else
            {
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                SqlCommand ncmd = new SqlCommand("SELECT * FROM termini WHERE rednibroj = '"+textBox1.Text+"' AND status = 'neobradjen'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(ncmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count <= 0) {
                    MessageBox.Show("Ne postoji termin sa tim rednim brojem, unesite ispravnu vrijednost");
                }
                else
                {
                    Properties.Settings.Default.brojtermina = textBox1.Text;
                    PregledUnosPodataka pup = new PregledUnosPodataka();
                    pup.Show();
                    this.Hide();
                }
            }
        }
    }
}
