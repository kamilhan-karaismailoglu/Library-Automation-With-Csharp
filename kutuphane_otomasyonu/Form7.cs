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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public string tc { get; set; }

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
        private void Form7_Load_1(object sender, EventArgs e)
        {
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
        
        private void button2_Click(object sender, EventArgs e)
        {
            for(int i=0; i< dataGridView1.SelectedRows.Count;i++)
            {
                if ((string)dataGridView1.SelectedRows[i].Cells[5].Value == "Ödünç Alındı")
                {
                    MessageBox.Show("Seçilen Kitap ( "+(string)dataGridView1.SelectedRows[i].Cells[1].Value+" ) Zaten Ödünç Alınmış","Error");

                }
                else
                {
                    baglanti.Open();
                    OleDbCommand komut2 = new OleDbCommand();                   
                    komut2.Connection = baglanti;
                    komut2.CommandText = "update kitaplar set durum= 'Ödünç Alındı' where ISBNo = '" + dataGridView1.SelectedRows[i].Cells[0].Value + "'";
                    komut2.ExecuteNonQuery();

                    OleDbCommand komut3 = new OleDbCommand();
                    komut3.Connection = baglanti;
                    komut3.CommandText = "insert into " + tc + "(ISBNo,ad,yazar,yayınevi,basım_yılı,durum) values (@ISBNo,@ad,@yazar,@yayınevi,@basım_yılı,@durum)";
                    komut3.Parameters.AddWithValue("@ISBNo", dataGridView1.SelectedRows[i].Cells[0].Value);
                    komut3.Parameters.AddWithValue("@ad", dataGridView1.SelectedRows[i].Cells[1].Value);
                    komut3.Parameters.AddWithValue("@yazar", dataGridView1.SelectedRows[i].Cells[2].Value);
                    komut3.Parameters.AddWithValue("@yayınevi", dataGridView1.SelectedRows[i].Cells[4].Value);
                    komut3.Parameters.AddWithValue("@basım_yılı", dataGridView1.SelectedRows[i].Cells[3].Value);
                    komut3.Parameters.AddWithValue("@durum", "Ödünç alındı");
                    komut3.ExecuteNonQuery();

                    OleDbCommand komut4 = new OleDbCommand();
                    komut4.Connection = baglanti;
                    komut4.CommandText = "insert into gecmis(tarih,kullanıcı,islem,ISBNo,ad,yazar) values (@tarih,@kullanıcı,@islem,@ISBNo,@ad,@yazar)";
                    string bugun = Convert.ToString(DateTime.Now);
                    komut4.Parameters.AddWithValue("@tarih", bugun);
                    komut4.Parameters.AddWithValue("@kullanıcı", tc);
                    komut4.Parameters.AddWithValue("@islem", "Ödünç Aldı");
                    komut4.Parameters.AddWithValue("@ISBNo", dataGridView1.SelectedRows[i].Cells[0].Value);
                    komut4.Parameters.AddWithValue("@ad", dataGridView1.SelectedRows[i].Cells[1].Value);
                    komut4.Parameters.AddWithValue("@yazar", dataGridView1.SelectedRows[i].Cells[2].Value);
                                     
                    komut4.ExecuteNonQuery();


                    DateTime dateAndTime = DateTime.Now;
                    OleDbCommand komut5 = new OleDbCommand();
                    komut5.Connection = baglanti;
                    komut5.CommandText = "insert into odunc(ISBNo,ad,yazar,kullanıcı,tarih) values (@ISBNo,@ad,@yazar,@kullanıcı,tarih)";
                    komut5.Parameters.AddWithValue("@ISBNo", dataGridView1.SelectedRows[i].Cells[0].Value);
                    komut5.Parameters.AddWithValue("@ad", dataGridView1.SelectedRows[i].Cells[1].Value);
                    komut5.Parameters.AddWithValue("@yazar", dataGridView1.SelectedRows[i].Cells[2].Value);
                    komut5.Parameters.AddWithValue("@kullanıcı", tc);
                    komut5.Parameters.AddWithValue("@tarih", dateAndTime.ToString("dd/MM/yyyy"));
                    komut5.ExecuteNonQuery();
                    baglanti.Close();

                    if (i==(dataGridView1.SelectedRows.Count)-1)
                    {
                        MessageBox.Show("Seçilen Kitaplar Başarıyla Ödünç Alındı","Bildirim");
                    }
                }
            }
            ds.Tables.Clear();
            griddoldur();
        }
    }
}
