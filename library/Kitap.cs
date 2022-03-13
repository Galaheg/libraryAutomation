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
    public partial class Kitap : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        public Kitap()
        {
            InitializeComponent();
        }

        static string constring = @"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True";
        SqlConnection connect = new SqlConnection(constring);
       
        void yenile()
        {
            txtkitapisim.Text = "";
            txtyayinevi.Text = "";
            txtyazar.Text = "";
            txtciltno.Text = "";
            txtbaskino.Text = "";
            txtbasimtarihi.Text = "";
        }

        void MusteriGetir()
        {
            baglanti = new SqlConnection(@"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("Select * from kitap ", baglanti);
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

        private void Kitap_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            yenile();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed) // bağlantı kapalıysa bağlantıyı aç komutunu girdik.
                {
                    connect.Open();
                }

                string kayit = "insert into kitap (kitapisim,yazar,yayinevi,baskino,basimtarihi,ciltno) values (@kitapisim,@yazar,@yayinevi,@baskino,@basimtarihi,@ciltno)";
                SqlCommand komut = new SqlCommand(kayit, connect);

                komut.Parameters.AddWithValue("@kitapisim", txtkitapisim.Text);
                komut.Parameters.AddWithValue("@yazar", txtyazar.Text);
                komut.Parameters.AddWithValue("@yayinevi", txtyayinevi.Text);
                komut.Parameters.AddWithValue("@baskino", txtbaskino.Text);
                komut.Parameters.AddWithValue("@basimtarihi", txtbasimtarihi.Text);
                komut.Parameters.AddWithValue("@ciltno", txtciltno.Text);

                komut.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarılı");

                MusteriGetir();
                yenile();


            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi " + hata.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtkitapisim.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtyazar.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtyayinevi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtbaskino.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtbasimtarihi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtciltno.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void txtarama_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            komut = new SqlCommand("select * from kitap where kitapisim like '" + txtarama.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtkitapisim.Text = read["kitapisim"].ToString();
                txtyazar.Text = read["yazar"].ToString();
                txtyayinevi.Text = read["yayinevi"].ToString();
                txtbaskino.Text = read["baskino"].ToString();
                txtbasimtarihi.Text = read["basimtarihi"].ToString();
                txtciltno.Text = read["ciltno"].ToString();
            }
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM kitap where kitapisim = @kitapisim";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kitapisim", txtkitapisim.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MusteriGetir();
        }
    }
}
