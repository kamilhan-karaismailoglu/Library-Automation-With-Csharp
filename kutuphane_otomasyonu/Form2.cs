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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Lütfen Bütün Bilgileri Eksiksiz Girin","Error");
            }
            else if (textBox1.Text.Length != 11)
            {
                MessageBox.Show("Tc Kimlik Numarası 11 Haneli Olmalı", "Error");
            }
            else if (textBox5.Text.Length != 10)
            {
                MessageBox.Show("Telefon Numarası 10 Haneli Olmalı", "Error");
            }
            else 
            {              
                OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data source= dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");               
                baglanti.Open();
                
                string ekle = "insert into login(tc_no,sifre,ad,soyad,tel_no,kullanıcı) values (@tc_no,@sifre,@ad,@soyad,@tel_no,@kullanıcı)";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);

                string tc = "SELECT tc_no FROM login";
                OleDbCommand komut2 = new OleDbCommand(tc, baglanti);
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
                    komut.Parameters.AddWithValue("@tc_no", textBox1.Text);
                    komut.Parameters.AddWithValue("@ad", textBox2.Text);
                    komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox4.Text);
                    komut.Parameters.AddWithValue("@tel_no", textBox5.Text);
                    komut.Parameters.AddWithValue("@kullanıcı", "kullanıcı");
                    komut.ExecuteNonQuery();

                    string creat = "Create Table " + textBox1.Text;
                    OleDbCommand komut3 = new OleDbCommand(creat, baglanti);
                    komut3.ExecuteNonQuery();

                    OleDbCommand kmt1 = new OleDbCommand("ALTER TABLE " + textBox1.Text + " ADD ISBNo string AUTO_INCREMENT", baglanti);
                    kmt1.ExecuteNonQuery();
                    OleDbCommand kmt2 = new OleDbCommand("ALTER TABLE " + textBox1.Text + " ADD ad string", baglanti);
                    kmt2.ExecuteNonQuery();
                    OleDbCommand kmt3 = new OleDbCommand("ALTER TABLE " + textBox1.Text + " ADD yazar string", baglanti);
                    kmt3.ExecuteNonQuery();
                    OleDbCommand kmt4 = new OleDbCommand("ALTER TABLE " + textBox1.Text + " ADD yayınevi string", baglanti);
                    kmt4.ExecuteNonQuery();
                    OleDbCommand kmt5 = new OleDbCommand("ALTER TABLE " + textBox1.Text + " ADD basım_yılı string", baglanti);
                    kmt5.ExecuteNonQuery();
                    OleDbCommand kmt6 = new OleDbCommand("ALTER TABLE " + textBox1.Text + " ADD durum string", baglanti);
                    kmt6.ExecuteNonQuery();

                    MessageBox.Show("Kayıt Başarılı Lütfen Giriş Yapın","Bildirim");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Girdiğiniz Tc Kimlik Numarası Daha Önce Kaydedilmiş", "Error");
                }
                baglanti.Close();                
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }
    }
}




                
            