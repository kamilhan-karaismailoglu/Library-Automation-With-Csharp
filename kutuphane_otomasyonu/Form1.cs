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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data source=dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
            OleDbCommand komut;
            OleDbDataReader reader;

            if (txt_tc_no.Text != "" || txt_sifre.Text != "" )
            {
                long tc_no = Convert.ToInt64(txt_tc_no.Text);
                string sifre = txt_sifre.Text;
                string yol = "select * from login where tc_no= " + tc_no + " AND StrComp(sifre,'" + sifre + "',0)=0";
                komut = new OleDbCommand(yol,baglanti);
                
                baglanti.Open();                             
                reader = komut.ExecuteReader();                
                if (reader.Read())
                {
                    Form3 f3 = new Form3();
                    
                    string user = (string)reader[5];                   
                    f3.tc = txt_tc_no.Text;
                    f3.sifre = sifre;
                    f3.user = user;

                    Form11 f11 = new Form11();
                    f11.tc = txt_tc_no.Text;
                    f11.sifre = sifre;
                    f11.user = user;

                    if ((string)reader[5]=="admin" || (string)reader[5] == "admin*")
                    {
                        this.Hide();                        
                        f11.ShowDialog();         
                    }
                    else
                    {
                        this.Hide();
                        f3.ShowDialog();                        
                    }
                }
                else
                {
                    MessageBox.Show("Tc Kimlik No Veya Şifre Hatalı","Error");                    
                    baglanti.Close();
                    this.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Bilgileri Eksiksiz Girin", "Error");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void txt_tc_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
