using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace kutuphane_otomasyonu
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox4.Text == "" || textBox3.Text == "" || textBox2.Text == "" || textBox5.Text == "") 
            {
                MessageBox.Show("Lütfen Bütün Bilgileri Eksiksiz Girin","Error");
            }
            else
            {
                OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data source= dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");               
                baglanti.Open();

                string ekle = "insert into kitaplar(ISBNo,ad,yazar,basım_yılı,yayınevi,durum) values (@ISBNo,@ad,@yazar,@basım_yılı,@yayınevi,@durum)";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);

                string no = "SELECT ISBNo FROM kitaplar";
                OleDbCommand komut2 = new OleDbCommand(no, baglanti);
                OleDbDataReader oku = komut2.ExecuteReader();
                int kontrol = 1;
               
                while (oku.Read())
                {
                    if (textBox1.Text == oku[0].ToString())
                    {
                        kontrol = 0;
                        break;
                    }
                }
                if (kontrol == 1)
                {                    
                    komut.Parameters.AddWithValue("@ISBNo", textBox1.Text);
                    komut.Parameters.AddWithValue("@ad", textBox2.Text);
                    komut.Parameters.AddWithValue("@yazar", textBox3.Text);
                    komut.Parameters.AddWithValue("@yayınevi", textBox5.Text);
                    komut.Parameters.AddWithValue("@basım_yılı", textBox4.Text);
                    komut.Parameters.AddWithValue("@durum", checkBox1.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kitap Başarıyla Kaydedildi", "Bildirim");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Girdiğiniz ISBNo Daha Önce Kaydedilmiş", "Error");
                }
                baglanti.Close();
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void Form4_Load(object sender, EventArgs e)
        {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
