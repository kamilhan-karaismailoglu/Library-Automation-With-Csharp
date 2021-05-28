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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        DataSet ds = new DataSet();

        void griddoldur()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from kitaplar", baglanti);
            adtr.Fill(ds, "kitaplar");
            dataGridView1.DataSource = ds.Tables["kitaplar"];
            baglanti.Close();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                Application.DoEvents();
                DataGridViewCellStyle renk = new DataGridViewCellStyle();

                if ((string)dataGridView1.Rows[i].Cells[5].Value == "Ödünç Alındı")
                {
                    renk.BackColor = Color.Gray;
                    renk.ForeColor = Color.White;
                }
                dataGridView1.Rows[i].DefaultCellStyle = renk;
            }
            dataGridView1.ClearSelection();
        }
        private void Form6_Load_1(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            griddoldur();
        }
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != e.Index)
                    checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Arama Türünü Seçin","Error");
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
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                Application.DoEvents();
                DataGridViewCellStyle renk = new DataGridViewCellStyle();

                if ((string)dataGridView1.Rows[i].Cells[5].Value == "Ödünç Alındı")
                {
                    renk.BackColor = Color.Gray;
                    renk.ForeColor = Color.White;
                }
                dataGridView1.Rows[i].DefaultCellStyle = renk;
            }
            dataGridView1.ClearSelection();
        }
        void KayıtSil(int ISBNo)
        {
            string dlt = "DELETE FROM kitaplar WHERE ISBNo=@ISBNo";
            OleDbCommand komut2 = new OleDbCommand(dlt,baglanti);
            komut2.Parameters.AddWithValue("@ISBNo", ISBNo);
            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Seçilen Kitapları Silmek İStediğinize Emin Misiniz ? Bu işlem Geri Alınamaz.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        if ((string)dataGridView1.SelectedRows[i].Cells[5].Value == "Ödünç Alındı")
                        {
                            MessageBox.Show("kitap ödünçteyken silinemez");
                        }
                        else
                        {
                            int ISBNo = Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[0].Value);
                            KayıtSil(ISBNo);
                            MessageBox.Show("Kayıt Silme İşlemi Başarılı", "Bildirim");
                        }                        
                    }                   
                    ds.Tables.Clear();
                    griddoldur();
                }
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tüm Kitapları Silmek İstediğinize Emin Misiniz ? Bu işlem Geri Alınamaz.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OleDbCommand komut = new OleDbCommand("DELETE FROM kitaplar where durum='Kütüphanede'", baglanti);
                MessageBox.Show("Ödünç Verilenler Kitaplar Hariç Tüm Kitaplar Silindi. Ödünç Verilen Kitapları Silmek İçin Ödünçten Gelmesini Bekleyin.","Bildirim");
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                ds.Tables.Clear();
                griddoldur();
            }
        }
    }
}

