using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphane_otomasyonu
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        public string tc { get; set; }
        public string sifre { get; set; }
        public string user { get; set; }

        void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.tc = tc;
            f7.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.tc = tc;
            f8.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.tc = tc;
            f9.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form10 f10 = new Form10();
            f10.tc = tc;
            f10.sifre = sifre;
            f10.user = user;         
            f10.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string uyarı = "Kullanıcı Değişikliği Yapmak İstediğinize Emin Misiniz ?";
            if (MessageBox.Show(uyarı, "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 f1 = new Form1();
                this.Hide();
                f1.ShowDialog();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form12 f12 = new Form12();
            f12.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form13 f13 = new Form13();
            f13.user = user;
            f13.sifre = sifre;
            f13.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form14 f14 = new Form14();
            f14.user = user;
            f14.ShowDialog();
        }      
    }
}
