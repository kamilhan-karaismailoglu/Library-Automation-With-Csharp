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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data source= dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        DataSet ds = new DataSet();

        private void Form5_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                dv.RowFilter = "Ad Like '" + textBox1.Text + "%'";
                dataGridView1.DataSource = dv;
            }

            else if (checkedListBox1.SelectedIndex == 1)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = "yazar Like '" + textBox1.Text + "%'";
                dataGridView1.DataSource = dv;
            }

            else if (checkedListBox1.SelectedIndex == 2)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = "yayınevi Like '" + textBox1.Text + "%'";
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
    }
}
