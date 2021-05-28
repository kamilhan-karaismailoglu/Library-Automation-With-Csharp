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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }
        public string user { get; set; }
        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        DataSet ds = new DataSet();

        void griddoldur()
        {            
                baglanti.Open();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                OleDbDataAdapter adtr = new OleDbDataAdapter("select * from odunc", baglanti);
                adtr.Fill(ds, "odunc");
                dataGridView1.DataSource = ds.Tables["odunc"];
                baglanti.Close();
                dataGridView1.Columns[0].HeaderText = "ISBNo";
                dataGridView1.Columns[1].HeaderText = "Kitap Adı";
                dataGridView1.Columns[2].HeaderText = "Yazar";
                dataGridView1.Columns[3].HeaderText = "Alan Kullanıcı";
                dataGridView1.Columns[4].HeaderText = "İşlem Tarihi";
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 123;
                dataGridView1.ClearSelection();
        }
        private void Form14_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
    }
}
