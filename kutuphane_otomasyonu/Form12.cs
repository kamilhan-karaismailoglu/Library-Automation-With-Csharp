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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source= dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        DataSet ds = new DataSet();

        void griddoldur()
        {
            ds.Tables.Clear();
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select sıra,tarih,kullanıcı,islem,ISBNo,Ad,Yazar from gecmis Order By sıra DESC", baglanti);
            adtr.Fill(ds, "gecmis");
            dataGridView1.DataSource = ds.Tables["gecmis"];
            baglanti.Close();
            dataGridView1.Columns[0].HeaderText = "Sıra";           
            dataGridView1.Columns[1].HeaderText = "İşlem Tarihi";           
            dataGridView1.Columns[2].HeaderText = "Kullanıcı";
            dataGridView1.Columns[3].HeaderText = "İşlem";
            dataGridView1.Columns[4].HeaderText = "ISBNo";
            dataGridView1.Columns[5].HeaderText = "Kitap Adı";
            dataGridView1.Columns[6].HeaderText = "Yazar";

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[4].Width = 52;
            dataGridView1.Columns[5].Width = 140;
            dataGridView1.ClearSelection();

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                Application.DoEvents();
                DataGridViewCellStyle renk = new DataGridViewCellStyle();

                if ((string)dataGridView1.Rows[i].Cells["islem"].Value == "Ödünç Aldı")
                {
                    renk.BackColor = Color.Gray;
                    renk.ForeColor = Color.White;
                }
                dataGridView1.Rows[i].DefaultCellStyle = renk;
            }
        }
        private void Form12_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            griddoldur();
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
                dv.RowFilter = "ad Like '" + textBox1.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            else if (checkedListBox1.SelectedIndex == 1)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = "ISBNo Like '" + textBox1.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            else if (checkedListBox1.SelectedIndex == 2)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = "kullanıcı Like '" + textBox1.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            dataGridView1.ClearSelection();

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                Application.DoEvents();
                DataGridViewCellStyle renk = new DataGridViewCellStyle();

                if ((string)dataGridView1.Rows[i].Cells["islem"].Value == "Ödünç Aldı")
                {
                    renk.BackColor = Color.Gray;
                    renk.ForeColor = Color.White;
                }
                dataGridView1.Rows[i].DefaultCellStyle = renk;
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != e.Index) 
                { 
                    checkedListBox1.SetItemChecked(i, false);
                }
            }    
        }
        void KayıtSil(int sıra)
        {
            string dlt = "DELETE FROM gecmis WHERE sıra=" + sıra + "";
            OleDbCommand komut2 = new OleDbCommand(dlt, baglanti);
            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçilen Kayıtları Silmek İstediğinize Emin Misiniz ? Bu işlem Geri Alınamaz.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    int sıra = (int)dataGridView1.SelectedRows[i].Cells[0].Value;
                    KayıtSil(sıra);
                }
                MessageBox.Show("Kayıt Silme İşlemi Başarılı", "Bildirim");
                griddoldur();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Kayıt Geçmişini Silmek İstediğinize Emin Misiniz ? Bu işlem Geri Alınamaz.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OleDbCommand komut = new OleDbCommand("DELETE FROM gecmis", baglanti);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                griddoldur();
            }
        }
    }
}
