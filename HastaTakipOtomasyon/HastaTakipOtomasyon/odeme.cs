using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

using System.Data.OleDb;

namespace HastaTakipOtomasyon
{
    public partial class odeme : Form
    {
        public odeme()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hasta.accdb");
        OleDbCommand komut = new OleDbCommand();

        private void button1_Click(object sender, EventArgs e)
        {

            comboBox1.Items.Add("Ödeme Yapılmadı");
            comboBox1.Items.Add("Ödeme Yapıldı");
            bool kayitarama = false;
            if (textBox1.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("Select *from muane where tc like '" + textBox1.Text + "%'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;

                    label9.Text = kayitokuma.GetValue(0).ToString();
                    label10.Text = kayitokuma.GetValue(1).ToString();
                    comboBox2.Text = kayitokuma.GetValue(2).ToString();
                    label12.Text = kayitokuma.GetValue(3).ToString();
                    //label13.Text = kayitokuma.GetValue(4).ToString();
                    label14.Text = kayitokuma.GetValue(4).ToString();
                    comboBox1.Text = kayitokuma.GetValue(5).ToString();
                   
                    break;
                }
                if (kayitarama == false)
                {
                    MessageBox.Show("Aranan Kayıt Bulunamadı...", "Yılmaz Yazılım Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Tc Kimlik Numarasını 11 Haneli Olarak Giriniz", "Yılmaz Yazılım Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
            {
                errorProvider1.SetError(textBox1, "Tc Kimlik 11 Kararkterli Olmalıdır.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void odeme_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 11; // Textbox 1 deki kararkter sayısı 11 olacak.
           
            toolTip1.SetToolTip(this.textBox1, "Tc Kimlik No 11 Karakter Olmalıdır...");
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            baglanti.Open();
            OleDbCommand guncelle = new OleDbCommand("UPDATE muane SET teshis='" + label10.Text + "', durum='" + comboBox2.Text + "', sonuc='" + label12.Text + "', ucret='" + label14.Text + "', odeme='" + (comboBox1.Text).ToString() + "'  where tc='" + label9.Text + "'", baglanti);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("İşem Başarılı Şekilde Yapıldı", "Yılmaz Yazılım Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        
             }
    }
}
