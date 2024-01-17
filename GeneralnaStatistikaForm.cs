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

namespace TehnickiPregled
{
    public partial class GeneralnaStatistikaForm : Form
    {
        public GeneralnaStatistikaForm()
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
            AdminPanelForm apf = new AdminPanelForm();
            apf.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TimeSpan duzinaDana = new TimeSpan(24, 0, 0);
            TimeSpan duzinaSedmice = new TimeSpan(7, 0, 0, 0);
            TimeSpan duzinaMjeseca = new TimeSpan(30, 0, 0, 0);
            TimeSpan duzinaGodine = new TimeSpan(365, 0, 0, 0);

            if ((checkBox2.Checked && (checkBox3.Checked || checkBox4.Checked || checkBox5.Checked)) || (checkBox3.Checked && (checkBox4.Checked || checkBox5.Checked)) || (checkBox4.Checked && checkBox5.Checked))
            {
                MessageBox.Show("Mozete izabrati samo jedan okvir vremena pregleda statistike");
            }
            else {
                SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                SqlCommand cmdo = new SqlCommand("", conn);
                SqlCommand cmdno = new SqlCommand("", conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dtObradjeni = new DataTable();
                DataTable dtNeobradjeni = new DataTable();
                if (textBox1.Text.Length > 0)
                {
                    if (checkBox2.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaDana) + "' AND radnik = '"+textBox1.Text+"'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else if (checkBox3.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaSedmice) + "' AND radnik = '"+textBox1.Text+"'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else if (checkBox4.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaMjeseca) + "' AND radnik = '"+textBox1.Text+"'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else if (checkBox5.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaGodine) + "' AND radnik = '"+textBox1.Text+"'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE radnik = '"+textBox1.Text+"'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                }
                else if (!checkBox1.Checked)
                {
                    if (checkBox2.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaDana) + "'", conn);
                        cmdo = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaDana) + "' AND status = 'obradjen'", conn);
                        cmdno = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaDana) + "' AND status = 'neobradjen'", conn);

                        sda = new SqlDataAdapter(cmdo);
                        sda.Fill(dtObradjeni);
                        sda = new SqlDataAdapter(cmdno);
                        sda.Fill(dtNeobradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = dtNeobradjeni.Rows.Count.ToString();
                        label6.Text = (dtObradjeni.Rows.Count + dtNeobradjeni.Rows.Count).ToString();
                    }
                    else if (checkBox3.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaSedmice) + "'", conn);
                        cmdo = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaSedmice) + "' AND status = 'obradjen'", conn);
                        cmdno = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaSedmice) + "' AND status = 'neobradjen'", conn);

                        sda = new SqlDataAdapter(cmdo);
                        sda.Fill(dtObradjeni);
                        sda = new SqlDataAdapter(cmdno);
                        sda.Fill(dtNeobradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = dtNeobradjeni.Rows.Count.ToString();
                        label6.Text = (dtObradjeni.Rows.Count + dtNeobradjeni.Rows.Count).ToString();
                    }
                    else if (checkBox4.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaMjeseca) + "'", conn);
                        cmdo = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaMjeseca) + "' AND status = 'obradjen'", conn);
                        cmdno = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaMjeseca) + "' AND status = 'neobradjen'", conn);

                        sda = new SqlDataAdapter(cmdo);
                        sda.Fill(dtObradjeni);
                        sda = new SqlDataAdapter(cmdno);
                        sda.Fill(dtNeobradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = dtNeobradjeni.Rows.Count.ToString();
                        label6.Text = (dtObradjeni.Rows.Count + dtNeobradjeni.Rows.Count).ToString();
                    }
                    else if (checkBox5.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaGodine) + "'", conn);
                        cmdo = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaGodine) + "' AND status = 'obradjen'", conn);
                        cmdno = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaGodine) + "' AND status = 'neobradjen'", conn);

                        sda = new SqlDataAdapter(cmdo);
                        sda.Fill(dtObradjeni);
                        sda = new SqlDataAdapter(cmdno);
                        sda.Fill(dtNeobradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = dtNeobradjeni.Rows.Count.ToString();
                        label6.Text = (dtObradjeni.Rows.Count + dtNeobradjeni.Rows.Count).ToString();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT * FROM termini", conn);
                        cmdo = new SqlCommand("SELECT * FROM termini WHERE status = 'obradjen'", conn);
                        cmdno = new SqlCommand("SELECT * FROM termini WHERE status = 'neobradjen'", conn);

                        sda = new SqlDataAdapter(cmdo);
                        sda.Fill(dtObradjeni);
                        sda = new SqlDataAdapter(cmdno);
                        sda.Fill(dtNeobradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = dtNeobradjeni.Rows.Count.ToString();
                        label6.Text = (dtObradjeni.Rows.Count + dtNeobradjeni.Rows.Count).ToString();
                    }
                }
                else if (checkBox1.Checked)
                {
                    if (checkBox2.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaDana) + "' AND status = 'obradjen'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else if (checkBox3.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaSedmice) + "' AND status = 'obradjen'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else if (checkBox4.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaMjeseca) + "' AND status = 'obradjen'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else if (checkBox5.Checked)
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE vrijeme > '" + dateTimePicker1.Value.Date + "' AND vrijeme < '" + dateTimePicker1.Value.Date.Add(duzinaGodine) + "' AND status = 'obradjen'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT * FROM termini WHERE status = 'obradjen'", conn);

                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtObradjeni);

                        label1.Text = dtObradjeni.Rows.Count.ToString();
                        label2.Text = "0";
                        label6.Text = dtObradjeni.Rows.Count.ToString();
                    }
                }

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

        private void GeneralnaStatistikaForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=VUKOV-KOMPJUTOR\\SQLEXPRESS;Initial Catalog=TehnickiPregledLogin;Integrated Security=True;Encrypt=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM login", conn);

            var reader = cmd.ExecuteReader();

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add("username", "Username");

            while (reader.Read())
            {
                dataGridView2.Rows.Add(reader["username"]);
            }

            reader.Close();
            conn.Close();
        }
    }
}
