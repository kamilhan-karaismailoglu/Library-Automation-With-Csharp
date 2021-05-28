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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        public string tc { get; set; }
        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        DataSet ds = new DataSet();
        void griddoldur()
        {
            baglanti.Open();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from "+tc+"", baglanti);
            adtr.Fill(ds, tc);
            dataGridView1.DataSource = ds.Tables[tc];
            baglanti.Close();
            dataGridView1.Columns[0].HeaderText = "ISBNo";
            dataGridView1.Columns[1].HeaderText = "Kitap Adı";
            dataGridView1.Columns[2].HeaderText = "Yazar";
            dataGridView1.Columns[3].HeaderText = "Yayınevi";
            dataGridView1.Columns[4].HeaderText = "Basım Yılı";
            dataGridView1.Columns[5].HeaderText = "Durumu";

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.ClearSelection();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            griddoldur();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "Ad Like '" + textBox1.Text + "%'";
            dataGridView1.DataSource = dv;
            dataGridView1.ClearSelection();
        }
        void iade(int ISBNo)
        {
            string dlt = "DELETE FROM "+tc+" WHERE ISBNo = @ISBNo";
            OleDbCommand komut2 = new OleDbCommand(dlt, baglanti);
            komut2.Parameters.AddWithValue("@ISBNo", ISBNo);
            baglanti.Open();
            komut2.ExecuteNonQuery();

            string odunc = "Kütüphanede";
            string upt = "UPDATE kitaplar SET durum=@durum WHERE ISBNo = @ISBNo";
            OleDbCommand komut3 = new OleDbCommand(upt,baglanti);
            komut3.Parameters.AddWithValue("@durum",odunc);
            komut3.Parameters.AddWithValue("@ISBNo",ISBNo);
            komut3.ExecuteNonQuery();

            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int ISBNo = Convert.ToInt32(drow.Cells[0].Value);
                iade(ISBNo);
            }
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                OleDbCommand komut4 = new OleDbCommand();
                komut4.Connection = baglanti;
                komut4.CommandText = "insert into gecmis(tarih,kullanıcı,islem,ISBNo,ad,yazar) values (@tarih,@kullanıcı,@islem,@ISBNo,@ad,@yazar)";
                string bugun = Convert.ToString(DateTime.Now);
                komut4.Parameters.AddWithValue("@tarih", bugun);
                komut4.Parameters.AddWithValue("@kullanıcı", tc);
                komut4.Parameters.AddWithValue("@islem", "İade Etti");
                komut4.Parameters.AddWithValue("@ISBNo", dataGridView1.SelectedRows[i].Cells[0].Value);
                komut4.Parameters.AddWithValue("@ad", dataGridView1.SelectedRows[i].Cells[1].Value);
                komut4.Parameters.AddWithValue("@yazar", dataGridView1.SelectedRows[i].Cells[2].Value);                
                baglanti.Open();
                komut4.ExecuteNonQuery();
                baglanti.Close();

                string ISBNo = (string)dataGridView1.SelectedRows[i].Cells[0].Value;
                OleDbCommand komut5 = new OleDbCommand();
                komut5.Connection = baglanti;
                komut5.CommandText = "DELETE FROM odunc WHERE ISBNo='"+ dataGridView1.SelectedRows[i].Cells[0].Value + "'";
                baglanti.Open();
                komut5.ExecuteNonQuery();
                baglanti.Close();
            }
            MessageBox.Show("Seçilen Kitaplar İade Edildi","Bildirim");
            ds.Tables.Clear();
            griddoldur();
        }
    }
}
