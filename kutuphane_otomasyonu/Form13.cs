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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }
        public string user { get; set; }
        public string sifre { get; set; }
        
        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        DataSet ds = new DataSet();

        void griddoldur()
        {
            ds.Tables.Clear();
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select tc_no,ad,soyad,tel_no,kullanıcı from login", baglanti);
            adtr.Fill(ds, "login");
            dataGridView1.DataSource = ds.Tables["login"];
            baglanti.Close();

            dataGridView1.Columns[0].HeaderText = "TC Kimlik Numarası";
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "Telefon Numarası";
            dataGridView1.Columns[4].HeaderText = "Kayıt Tipi";
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.ClearSelection();
        }
        private void Form13_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            griddoldur();
            if (user == "admin*")
            {
                button1.Enabled = true;
                button3.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Arama Türünü Seçin", "Error");
            }

            else if (checkedListBox1.SelectedIndex == 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = string.Format("CONVERT(tc_no, System.String) like '{0}%'", textBox1.Text);
                dataGridView1.DataSource = dv;
            }
            else if (checkedListBox1.SelectedIndex == 1)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = "ad Like '" + textBox1.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            dataGridView1.ClearSelection();
        }
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != e.Index)
                    checkedListBox1.SetItemChecked(i, false);
            }
        }
        void KayıtSil(long tc)
        {
            string dlt = "DELETE FROM login WHERE tc_no="+tc+"";
            OleDbCommand komut2 = new OleDbCommand(dlt, baglanti);
            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();

            string tablo = "DROP TABLE "+tc+"";
            OleDbCommand komut3 = new OleDbCommand(tablo,baglanti);
            baglanti.Open();
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silme İşlemi Başarılı","Bildirim");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçilen Kullanıcıları Silmek İstediğinize Emin Misiniz ? Bu işlem Geri Alınamaz.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {                
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    long tc = Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[0].Value);
                    if ((string)dataGridView1.SelectedRows[i].Cells[4].Value == "kullanıcı")
                    {                        
                        KayıtSil(tc);
                        griddoldur();
                    }
                    else if ((string)dataGridView1.SelectedRows[i].Cells[4].Value == "admin")
                    {                      
                        KayıtSil(tc);
                        griddoldur();
                    }
                    else
                    {
                        MessageBox.Show("Seçili admin ( "+tc+" ) silinemez.");
                    }
                }
            }         
        }
        void adminyap(long tc)
        {
            string adminyap ="update login set kullanıcı='admin' where tc_no="+tc+" ";
            OleDbCommand komut4 = new OleDbCommand(adminyap,baglanti);

            baglanti.Open();
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Seçilen Kullanıcı ( "+tc+" ) Başarıyla Admin Yapıldı", "Bildirim");
        }

        private void button3_Click(object sender, EventArgs e)
        {           
            if (MessageBox.Show("Seçilen Kullanıcıları Admin Yapmak İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    long tc = Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[0].Value);
                    if ( (string)dataGridView1.SelectedRows[i].Cells[4].Value == "kullanıcı")
                    {
                        adminyap(tc);
                        griddoldur();                                                                  
                    }
                    else
                    {
                        MessageBox.Show("Seçilen Kullanıcı ( " + tc + " ) zaten admin", "Error");
                    }
                }                
            }
        }
        void kullanıcıyap(long tc)
        {
            string kullanıcı = "update login set kullanıcı='kullanıcı' where tc_no=" + tc + " ";
            OleDbCommand komut5 = new OleDbCommand(kullanıcı, baglanti);

            baglanti.Open();
            komut5.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Seçilen admin ( " + tc + " ) Başarıyla kullanıcı Yapıldı", "Bildirim");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçilen Adminleri Kullanıcı Yapmak İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    long tc = Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[0].Value);
                    if ((string)dataGridView1.SelectedRows[i].Cells[4].Value == "admin")
                    {
                        kullanıcıyap(tc);
                        griddoldur();
                    }
                    else if((string)dataGridView1.SelectedRows[i].Cells[4].Value == "admin*") 
                    {
                        MessageBox.Show("Seçilen admin ( " + tc + " ) kullanıcı yapılamaz", "Error");
                    }
                    else
                    {
                        MessageBox.Show("Seçilen Kullanıcı ( " + tc + " ) zaten admin değil", "Error");
                    }
                }
            }
        }
    }
}
