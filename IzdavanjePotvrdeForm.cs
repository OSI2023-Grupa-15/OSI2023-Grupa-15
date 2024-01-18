using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TehnickiPregled
{
    public partial class IzdavanjePotvrdeForm : Form
    {
        public IzdavanjePotvrdeForm()
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
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }

        private void IzdavanjePotvrdeForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM pregledi", conn);

            var reader = cmd.ExecuteReader();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("hash", "Sifra pregleda");
            dataGridView1.Columns.Add("vlasnik", "Vlasnik");
            dataGridView1.Columns.Add("jmbg", "Jmbg");
            dataGridView1.Columns.Add("vrijeme", "Vrijeme termina");
            dataGridView1.Columns.Add("radnik", "Tehnicar");
            dataGridView1.Columns.Add("prolaznost", "Prolaznost");
            dataGridView1.Columns.Add("proizvodjac", "Marka auta");
            dataGridView1.Columns.Add("registracija", "Registarski broj vozila");
            dataGridView1.Columns.Add("brojsasije", "Broj sasije");

            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["hash"], reader["vlasnik"], reader["jmbg"], reader["vrijeme"], reader["radnik"], reader["prolaznost"], reader["proizvodjac"], reader["registracija"],  reader["brojsasije"]);
            }

            dataGridView1.Sort(dataGridView1.Columns["vrijeme"], ListSortDirection.Ascending);
            reader.Close();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM pregledi WHERE hash = '"+ textBox1.Text +"'", conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0) {

                string directoryPath = @"C:\POTVRDE ZA TEHNICKI PREGLED";
                string filePath = Path.Combine(directoryPath, textBox1.Text + ".txt");

                Directory.CreateDirectory(directoryPath);

                using (StreamWriter writetext = new StreamWriter(filePath))
                {
                    writetext.WriteLine("POTVRDA O IZVRSAVANJU TEHNICKOG PREGLEDA");
                    writetext.WriteLine("*Potvrda traje 30 dana od izvrsavanja pregleda*"); 
                    writetext.WriteLine("Jedinstvena sifra pregleda: " + dt.Rows[0]["hash"].ToString());
                    writetext.WriteLine("Vlasnik vozila: " + dt.Rows[0]["vlasnik"].ToString());
                    writetext.WriteLine("Jmbg: " + dt.Rows[0]["jmbg"].ToString());
                    writetext.WriteLine("Datum i vrijeme pregleda: " + dt.Rows[0]["vrijeme"].ToString());
                    writetext.WriteLine("Tehnicar koji je obavljao tehnicki pregled: " + dt.Rows[0]["radnik"].ToString());
                    writetext.WriteLine("Registarski broj vozila: " + dt.Rows[0]["registracija"].ToString());
                    writetext.WriteLine("Broj sasije vozila: " + dt.Rows[0]["brojsasije"].ToString());
                    writetext.WriteLine("Proizvodjac vozila: " + dt.Rows[0]["proizvodjac"].ToString());
                    writetext.WriteLine("Model vozila: " + dt.Rows[0]["model"].ToString());
                    writetext.WriteLine("Godina proizvodnje vozila: " + dt.Rows[0]["godina"].ToString());
                    writetext.WriteLine("Boja vozila: " + dt.Rows[0]["boja"].ToString());
                    writetext.WriteLine("Vrsta motora: " + dt.Rows[0]["gorivo"].ToString());
                    writetext.WriteLine("Zapremina motora: " + dt.Rows[0]["zapremina"].ToString());
                    writetext.WriteLine("Kategorija vozila: " + dt.Rows[0]["kategorija"].ToString());
                    writetext.WriteLine("Ispravnost uredjaja za upravljanje vozilom: " + dt.Rows[0]["ispravnostUpravljanje"].ToString());
                    writetext.WriteLine("Ispravnost uredjaja za kocenje: " + dt.Rows[0]["ispravnostKocenje"].ToString());
                    writetext.WriteLine("Ispravnost uredjaja za osvjetljavanje i svjetlosnu signalizaciju: " + dt.Rows[0]["ispravnostOsvjetljenje"].ToString());
                    writetext.WriteLine("Ispravnost uredjaja koji omogucavaju normalnu vidljivost: " + dt.Rows[0]["ispravnostVidljivost"].ToString());
                    writetext.WriteLine("Ispravnost samonosive karoserije te sasije sa kabinom i nadogradnjom: " + dt.Rows[0]["ispravnostSamonosivost"].ToString());
                    writetext.WriteLine("Ispravnost elemenata ovjesa, osovine, i tockova: " + dt.Rows[0]["ispravnostTockovi"].ToString());
                    writetext.WriteLine("Ispravnost motora: " + dt.Rows[0]["ispravnostMotor"].ToString());
                    writetext.WriteLine("Nivo buke vozila: " + dt.Rows[0]["ispravnostBuka"].ToString());
                    writetext.WriteLine("Ispravnost elektro uredjaja i elektro-instalacije: " + dt.Rows[0]["ispravnostElektronike"].ToString());
                    writetext.WriteLine("Ispravnost prijenosnog mehanizma: " + dt.Rows[0]["ispravnostPrijenos"].ToString());
                    writetext.WriteLine("Ispravnost kontrolnih i signalnih uredjaja: " + dt.Rows[0]["ispravnostKontrolniUredjaji"].ToString());
                    writetext.WriteLine("Nivo izduvnih gazova vozila: " + dt.Rows[0]["ispravnostGasovi"].ToString());
                    writetext.WriteLine("Ispravnost uredjaja za spajanje vucnog i prikljucnog vozila: " + dt.Rows[0]["ispravnostSpajanje"].ToString());
                    writetext.WriteLine("Ispravnost osnovnih dijelova i uredjaja vozila: " + dt.Rows[0]["ispravnostOsnovniDijelovi"].ToString());
                    writetext.WriteLine("Ispravnost dodatne opreme vozila: " + dt.Rows[0]["ispravnostOprema"].ToString());
                    writetext.WriteLine("Ispravnost registarskih tablica i oznaka vozila: " + dt.Rows[0]["ispravnostTablice"].ToString());
                    writetext.WriteLine("Ispravnost gasnih instalacija vozila: " + dt.Rows[0]["ispravnostGasneInstalacije"].ToString());
                    writetext.WriteLine("----------------------------------------------------------------------------------");
                    writetext.WriteLine("Prolaznost vozila na tehnickom pregledu: " + dt.Rows[0]["prolaznost"].ToString());
                    writetext.WriteLine("----------------------------------------------------------------------------------");
                    writetext.WriteLine("Potvrdu izdao: " + Properties.Settings.Default.logedinuser);
                    writetext.WriteLine("Datum i vrijeme vadjenja potvrde: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                MessageBox.Show("Potvrda je kreirana");
            } else
            {
                MessageBox.Show("Nije pronadjena potvrda sa tom sifrom");   
            }
        }
    }
}
