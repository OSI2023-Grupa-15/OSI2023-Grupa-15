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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TehnickiPregled
{
    public partial class PregledUnosPodataka : Form
    {
        public PregledUnosPodataka()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PregledBiranjeTermina pbt = new PregledBiranjeTermina();
            pbt.Show();
            this.Hide();
        }

        private void PregledUnosPodataka_Load(object sender, EventArgs e)
        {
            label1.Text = Properties.Settings.Default.logedinuser;
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            SqlCommand ncmd = new SqlCommand("SELECT * FROM termini WHERE rednibroj = '" + Properties.Settings.Default.brojtermina + "'", conn);
            SqlDataAdapter sda = new SqlDataAdapter(ncmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                label4.Text = dt.Rows[0]["imeklijenta"].ToString();
                label6.Text = dt.Rows[0]["jmbg"].ToString();
                label8.Text = dt.Rows[0]["vrijeme"].ToString();
            } else
            {
                label4.Text = "Ime";
                label6.Text = "Jmbg";
                label8.Text = "Trenutno";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length <= 0 || comboBox2.Text.Length <= 0 || comboBox3.Text.Length <= 0 || comboBox4.Text.Length <= 0 || comboBox5.Text.Length <= 0 || textBox2.Text.Length <= 0 || textBox6.Text.Length <= 0 || textBox7.Text.Length <= 0)
            {
                MessageBox.Show("Popunite sva polja");
            }
            else
            {
                Properties.Settings.Default.vlasnik = label4.Text;
                Properties.Settings.Default.jmbg = label6.Text;
                Properties.Settings.Default.kategorija = comboBox1.Text;
                Properties.Settings.Default.proizvodjac = comboBox2.Text;
                Properties.Settings.Default.model = textBox2.Text;
                Properties.Settings.Default.godinaproizvodnje = comboBox3.Text;
                Properties.Settings.Default.boja = comboBox4.Text;
                Properties.Settings.Default.gorivo = comboBox5.Text;
                Properties.Settings.Default.zapremina = textBox6.Text;
                Properties.Settings.Default.brojsasije = textBox7.Text;

                PregledUnosPodataka2Form pup2f = new PregledUnosPodataka2Form();
                pup2f.Show();
                this.Hide();
            }
        }
    }
}
