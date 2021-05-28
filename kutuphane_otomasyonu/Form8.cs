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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        public string tc { get; set; }

        OleDbConnection baglanti = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=dblogin.mdb;Jet OLEDB:Database Password=%MK7Nged=N");
        DataSet ds = new DataSet();

        public void Form8_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from "+tc+" ", baglanti);
            adtr.Fill(ds, "tc");
            dataGridView1.DataSource = ds.Tables["tc"];
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

        public void button1_Click(object sender, EventArgs e)
        {
        DataView dv = ds.Tables[0].DefaultView;
        dv.RowFilter = "Ad Like '" + textBox1.Text + "%'";
        dataGridView1.DataSource = dv;
        dataGridView1.ClearSelection();
        }
    }
}
