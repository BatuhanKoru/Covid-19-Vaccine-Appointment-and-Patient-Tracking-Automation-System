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

namespace HastaTakipOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=hasta.accdb");
        public static string tcno, adi, soyadi, kuladi,sifre,eposta,tel,yetki;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        bool durum = false;
                                              
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanici", baglanti);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {


                if (kayitokuma["kuladi"].ToString() == textBox1.Text && kayitokuma["sifre"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Admin")
                {
                    durum = true;
                    tcno = kayitokuma.GetValue(0).ToString();
                    adi = kayitokuma.GetValue(1).ToString();
                    soyadi = kayitokuma.GetValue(2).ToString();
                    kuladi = kayitokuma.GetValue(3).ToString();
                    sifre = kayitokuma.GetValue(4).ToString();
                    eposta = kayitokuma.GetValue(5).ToString();
                    tel = kayitokuma.GetValue(6).ToString();
                    yetki = kayitokuma.GetValue(7).ToString();
                    this.Hide();
                    AdminPanel frm2 = new AdminPanel();
                    frm2.Show();
                    break;
                }

                if (kayitokuma["kuladi"].ToString() == textBox1.Text && kayitokuma["sifre"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Doktor")
                {
                    durum = true;
                    tcno = kayitokuma.GetValue(0).ToString();
                    adi = kayitokuma.GetValue(1).ToString();
                    soyadi = kayitokuma.GetValue(2).ToString();
                    kuladi = kayitokuma.GetValue(3).ToString();
                    sifre = kayitokuma.GetValue(4).ToString();
                    eposta = kayitokuma.GetValue(5).ToString();
                    tel = kayitokuma.GetValue(6).ToString();
                    yetki = kayitokuma.GetValue(7).ToString();
                    this.Hide();
                    DoktorPanel frm3 = new DoktorPanel();
                    frm3.Show();
                    break;
                }
            }
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Boş Geçilmez...");
            }
            else if (durum == false)
            {
                MessageBox.Show("Kullanıcı Bulunamadı");
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
