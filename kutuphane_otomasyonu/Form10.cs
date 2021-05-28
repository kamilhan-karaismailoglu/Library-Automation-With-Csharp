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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        public string tc { get; set; }
        public string sifre { get; set; }
        public string user { get; set; }

        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source= dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        private void getir()
        {
            OleDbDataReader reader;
            string ara = "select ad,soyad,tel_no from login where tc_no=" + tc + "";
            OleDbCommand komut = new OleDbCommand(ara, baglanti);
            baglanti.Open();
            reader = komut.ExecuteReader();
            while (reader.Read())
            {
                label3.Text = (string)reader[0];
                label4.Text = (string)reader[1];
                label5.Text = "+90 "+(string)reader[2]+"";
            }
            baglanti.Close();
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            label1.Text = tc;
            getir();
            if (tc == "12345678900")
            {
                textBox1.Enabled = true;
                Form11 f11 = new Form11();
                f11.Hide();
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            string srg = "SELECT sifre FROM login where tc_no=" + tc + "";
            OleDbCommand sorgu = new OleDbCommand(srg,baglanti);                  
            baglanti.Open();
          
            OleDbDataReader oku = sorgu.ExecuteReader();
            
            while (oku.Read())
            {
                if (textBox5.Text != oku[0].ToString())
                {
                    MessageBox.Show("Şifre Doğrulanamadı, Lütfen Mevcut Şifrenizi Kontrol Ediniz","Error");
                }
                else               
                {
                    if (textBox1.Enabled && textBox1.Text != "")
                    {
                        if (tc == "12345678900")
                        {
                            if (textBox1.Text.Length == 11)
                            {
                                string tc = "SELECT tc_no FROM login";
                                OleDbCommand komut2 = new OleDbCommand(tc, baglanti);
                                OleDbDataReader oku2 = komut2.ExecuteReader();
                                int kontrol = 1;
                                while (oku2.Read())
                                {
                                    if (textBox1.Text == oku2[0].ToString())
                                    {
                                        kontrol = 0;
                                        break;
                                    }
                                }
                                if (kontrol == 0)
                                {
                                    MessageBox.Show("Girdiğiniz Tc Kimlik Numarası Daha Önce Kaydedilmiş", "Error");
                                }

                                else
                                {
                                    if (MessageBox.Show("TC Kimlik Numaranızı Yanlızca Bir Kere Değiştirebilirsiniz. İşleme Devam Etmek İstiyor Musunuz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        long txt = long.Parse(textBox1.Text);
                                        string update = "UPDATE login SET tc_no=" + txt + " WHERE kullanıcı ='admin*' ";
                                        OleDbCommand komut3 = new OleDbCommand(update, baglanti);
                                        komut3.ExecuteNonQuery();
                                        string a = "select * into " + textBox1.Text + " from 12345678900";
                                        OleDbCommand komut4 = new OleDbCommand(a,baglanti);
                                        komut4.ExecuteNonQuery();
                                        OleDbCommand komut5 = new OleDbCommand("DROP TABLE 12345678900", baglanti);
                                        komut5.ExecuteNonQuery();
                                        MessageBox.Show("Güncelleme İşlemi Başarılı. Lütfen Tekrar Giriş Yapın", "Bildirim");
                                        Form1 f1 = new Form1();
                                        this.Hide();
                                        f1.ShowDialog();
                                    }
                                }                        
                            }
                            else
                            {
                                MessageBox.Show("TC Kimlik Numarası 11 Haneli Olmalıdır", "Error");
                            }                                                 
                        }
                        else
                        {                          
                            MessageBox.Show("TC Kimlik Numarası Daha Önce Değiştirildiği İçin Değiştirilemedi", "Error");                           
                        }                      
                    }
                    
                    
                    if (textBox2.Text != "")
                    {
                        string skmt1 = "UPDATE login SET ad=@ad WHERE tc_no = " + tc + "";
                        OleDbCommand kmt1 = new OleDbCommand(skmt1, baglanti);
                        kmt1.Parameters.AddWithValue("@ad", textBox2.Text);
                        kmt1.ExecuteNonQuery();
                        if (textBox3.Text == "" && textBox4.Text == "" && textBox6.Text == "")
                        {
                            MessageBox.Show("Güncelleme İşlemi Başarılı", "Bildirim");
                        }
                    }
                    if (textBox3.Text != "")
                    {
                        string skmt2 = "UPDATE login SET soyad=@soyad WHERE tc_no = " + tc + "";
                        OleDbCommand kmt2 = new OleDbCommand(skmt2, baglanti);
                        kmt2.Parameters.AddWithValue("@soyad", textBox3.Text);
                        kmt2.ExecuteNonQuery();
                        if (textBox4.Text == "" && textBox6.Text == "")
                        {
                            MessageBox.Show("Güncelleme İşlemi Başarılı", "Bildirim");
                        }
                    }
                    if (textBox4.Text != "")
                    {
                        string skmt3 = "UPDATE login SET sifre=@sifre WHERE tc_no = " + tc + "";
                        OleDbCommand kmt3 = new OleDbCommand(skmt3, baglanti);
                        kmt3.Parameters.AddWithValue("@sifre", textBox4.Text);
                        kmt3.ExecuteNonQuery();
                        if (textBox6.Text == "")
                        {
                            MessageBox.Show("Güncelleme İşlemi Başarılı", "Bildirim");
                        }
                    }
                    if (textBox6.Text != "")
                    {
                        if (textBox6.Text.Length != 10)
                        {
                            MessageBox.Show("Telefon Numarası 10 Haneli Olmalıdır", "Error");
                        }
                        else
                        {
                            string skmt4 = "UPDATE login SET tel_no=@tel WHERE tc_no = " + tc + "";
                            OleDbCommand kmt4 = new OleDbCommand(skmt4, baglanti);
                            kmt4.Parameters.AddWithValue("@tel", textBox6.Text);
                            kmt4.ExecuteNonQuery();
                            MessageBox.Show("Güncelleme İşlemi Başarılı", "Bildirim");
                        }                        
                    }                   
                }                              
            }
            baglanti.Close();
            getir();
        }

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tc=="12345678900")
            {
                Application.Exit();
            }
        }
    }      
}
