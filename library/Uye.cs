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

namespace a
{
    public partial class Uye : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        public Uye()
        {
            InitializeComponent();
        }

        static string constring = @"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True";
        SqlConnection connect = new SqlConnection(constring);

        void MusteriGetir()
        {
            baglanti = new SqlConnection(@"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("Select * from Uye ", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();          
        }

        private void Uye_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed) // bağlantı kapalıysa bağlantıyı aç komutunu girdik.
                {
                    connect.Open();
                }

                string kayit = "insert into Uye (adsoyad,cinsiyet,telefonno,tcno,mail) values (@adsoyad,@cinsiyet,@telefonno,@tc,@mail)";
                SqlCommand komut = new SqlCommand(kayit, connect);

                komut.Parameters.AddWithValue("@adsoyad", textBox1.Text);
                if (radioButton1.Checked == true)
                {
                    komut.Parameters.AddWithValue("@cinsiyet", radioButton1.Text);
                }
                else if (radioButton2.Checked == true)                          //******
                {
                    komut.Parameters.AddWithValue("@cinsiyet", radioButton2.Text);
                }

                komut.Parameters.AddWithValue("@mail", textBox2.Text);
                komut.Parameters.AddWithValue("@telefonno", textBox3.Text);
                komut.Parameters.AddWithValue("@tc", textBox4.Text);

                komut.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarılı");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi " + hata.Message);
            }

            MusteriGetir();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();                    
            textBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Uye where tcno = @tcno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@tcno" , textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();//sql kaydetme
            baglanti.Close();
            MusteriGetir();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            komut = new SqlCommand("select * from Uye where tcno like '"+textBox5.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while(read.Read())
            {
                textBox1.Text = read["adsoyad"].ToString();
                textBox2.Text = read["mail"].ToString();
                textBox3.Text = read["telefonno"].ToString();
                textBox4.Text = read["tcno"].ToString();
            }
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
    }
}
