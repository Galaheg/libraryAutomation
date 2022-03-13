using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace a
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uye uye = new Uye();     // Üye Formu açılır    
            uye.ShowDialog();          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kitap kitap = new Kitap();        // Kitap Formu açılır     
            kitap.ShowDialog();            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UyeKitap uyekitap = new UyeKitap();       // Üye-Kitap Formu açılır        
            uyekitap.ShowDialog();           
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
          
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
          
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}
