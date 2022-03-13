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
    public partial class Login : Form
    {
        SqlConnection con;  // sql bağlantı
        SqlCommand cmd;     // sql komut girişi
        SqlDataReader dr;   // sql içindeki verileri okuma

        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.Trim();
            string pass = textBox2.Text.Trim();

            con = new SqlConnection(@"Data Source=DESKTOP-86CTNL5\SQLEXPRESS;Initial Catalog=UKutuphane;Integrated Security=True"); //adres
            cmd = new SqlCommand();//komut

            con.Open();   //bağlantıyı açma
            cmd.Connection = con;//bağlantı
            cmd.CommandText = "SELECT * FROM Login where kullaniciadi='" + user + "' AND sifre='" + pass + "'";  //komut***** bir yerden seçme
            dr = cmd.ExecuteReader(); // satır satır okuma işlemi yapar.
            if (dr.Read())  // satırları okuma
            {
                MessageBox.Show("Giriş Başarılı");
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
            }

            con.Close(); //bağlantıyı kapatma


        }
    }
}
