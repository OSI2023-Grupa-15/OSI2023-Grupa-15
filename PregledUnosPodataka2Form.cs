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
    public partial class PregledUnosPodataka2Form : Form
    {
        public PregledUnosPodataka2Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PregledBiranjeTermina pbt = new PregledBiranjeTermina();
            pbt.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length <= 0 || comboBox2.Text.Length <= 0 || comboBox3.Text.Length <= 0 || comboBox4.Text.Length <= 0 || comboBox5.Text.Length <= 0 || comboBox6.Text.Length <= 0 || comboBox7.Text.Length <= 0 || comboBox8.Text.Length <= 0 || comboBox9.Text.Length <= 0 || comboBox10.Text.Length <= 0 || comboBox11.Text.Length <= 0 || comboBox11.Text.Length <= 0 || comboBox12.Text.Length <= 0 || comboBox13.Text.Length <= 0 || comboBox15.Text.Length <= 0 || comboBox16.Text.Length <= 0 || comboBox17.Text.Length <= 0)
            {
                MessageBox.Show("Morate popuniti sva polja da bi zavrsili tehnicki pregled");
            }
            else
            {
                string prolaznost = "";
                if (comboBox1.Text != "Ispravno" || comboBox2.Text != "Ispravno" || comboBox3.Text != "Ispravno" || comboBox4.Text != "Ispravno" || comboBox5.Text != "Ispravno" || comboBox6.Text != "Ispravno" || comboBox7.Text != "Ispravno" || comboBox8.Text != "Ispravno" || comboBox9.Text != "Ispravno" || comboBox10.Text != "Ispravno" || comboBox11.Text != "Ispravno" || comboBox12.Text != "Ispravno" || comboBox13.Text != "Ispravno" || comboBox14.Text != "Ispravno" || comboBox15.Text != "Ispravno" || comboBox16.Text != "Ispravno" || comboBox17.Text != "Ispravno")
                {
                    prolaznost = "Nije prosao";
                } else
                {
                    prolaznost = "Prosao";
                }
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                conn.Open();
                SqlCommand ncmd = new SqlCommand("SELECT * FROM termini WHERE rednibroj = '"+ Properties.Settings.Default.brojtermina +"'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(ncmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DateTime vrijemetermina = (DateTime)dt.Rows[0]["vrijeme"];
                SqlCommand cmd = new SqlCommand("INSERT INTO pregledi (vlasnik, jmbg, vrijeme, kategorija, proizvodjac, model, godina, boja, gorivo, zapremina, brojsasije, ispravnostUpravljanje, ispravnostKocenje, ispravnostOsvjetljenje, ispravnostVidljivost, ispravnostSamonosivost, ispravnostTockovi, ispravnostMotor, ispravnostBuka, ispravnostElektronike, ispravnostPrijenos, ispravnostKontrolniUredjaji, ispravnostGasovi, ispravnostSpajanje, ispravnostOsnovniDijelovi, ispravnostOprema, ispravnostTablice, ispravnostGasneInstalacije, prolaznost, radnik) VALUES ('"+ Properties.Settings.Default.vlasnik + "', '"+ Properties.Settings.Default.jmbg + "', '" + vrijemetermina + "', '" + Properties.Settings.Default.kategorija + "', '"+ Properties.Settings.Default.proizvodjac + "', '"+ Properties.Settings.Default.model + "', '"+ Properties.Settings.Default.godinaproizvodnje + "', '"+ Properties.Settings.Default.boja + "', '"+ Properties.Settings.Default.gorivo + "', '"+ Properties.Settings.Default.zapremina + "', '"+ Properties.Settings.Default.brojsasije + "', '" + comboBox1.Text + "', '"+ comboBox2.Text + "', '"+ comboBox3.Text + "', '"+ comboBox4.Text + "', '"+ comboBox5.Text + "', '"+ comboBox6.Text + "', '"+ comboBox7.Text + "', '"+ comboBox8.Text + "', '"+ comboBox9.Text + "', '"+ comboBox10.Text + "', '"+ comboBox11.Text + "', '"+ comboBox12.Text + "', '"+ comboBox13.Text + "', '"+ comboBox14.Text + "', '"+ comboBox15.Text + "', '"+ comboBox16.Text + "', '"+ comboBox17.Text + "', '"+ prolaznost + "', '"+ Properties.Settings.Default.logedinuser + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Rezultati pregleda su arhivirani");
                MainForm mf = new MainForm();
                mf.Show();
                this.Hide();
            }
        }
    }
}
