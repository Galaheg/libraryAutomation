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
    public partial class UyeKitap : Form
    {
        public UyeKitap()
        {
            InitializeComponent();
        }

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        static string constring = @"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True";
        SqlConnection connect = new SqlConnection(constring);

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();          
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        void combo1()
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True";

            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM kitap";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["kitapisim"]);
            }
            baglanti.Close();
        }

        void combo2()
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True";

            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM Uye";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["adsoyad"]);
            }
            baglanti.Close();
        }

        private void UyeKitap_Load(object sender, EventArgs e)
        {
            MusteriGetir();
            combo1();
            combo2();
            

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed) // bağlantı kapalıysa bağlantıyı aç komutunu girdik.
                {
                    connect.Open();
                }

                string kayit = "insert into uyekitap (emanetkitap,emanetkisi,alinmatarih,iadetarih) values (@emanetkitap,@emanetkisi,@alinmatarih,@iadetarih)";
                SqlCommand komut = new SqlCommand(kayit, connect);

                komut.Parameters.AddWithValue("@emanetkitap", comboBox1.Text);
                komut.Parameters.AddWithValue("@emanetkisi", comboBox2.Text);
                komut.Parameters.AddWithValue("@alinmatarih", dateTimePicker1.Value.Date);
                komut.Parameters.AddWithValue("@iadetarih", dateTimePicker2.Value.Date);

                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarılı");

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi " + hata.Message);
            }
            MusteriGetir();
        }

        void MusteriGetir()
        {
            baglanti = new SqlConnection(@"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("Select * from uyekitap ", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
            {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM uyekitap where emanetkitap = @emanetkitap";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@emanetkitap", comboBox1.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MusteriGetir();
        }
    }
}
