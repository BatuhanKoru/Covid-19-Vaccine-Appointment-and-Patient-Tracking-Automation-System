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
using System.Text.RegularExpressions;
using System.IO;

namespace HastaTakipOtomasyon
{

    
    public partial class kullanici : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hasta.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();
    
        public kullanici()
        {
            InitializeComponent();
        }
       
        private void goster()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter getir = new OleDbDataAdapter("select tcno AS[Tc Kimlik No],adi AS[Adı],soyadi AS[Soyadı],kuladi AS[Kullanıcı Adı],sifre AS[Şifre],eposta AS[E-Posta], tel AS[Telefon],yetki AS[Yetkisi] from kullanici Order By adi ASC", baglanti);
                DataSet hafiza = new DataSet();
                getir.Fill(hafiza);
                dataGridView1.DataSource = hafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamesaj)
            {

                MessageBox.Show(hatamesaj.Message, "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        
        }

        private void temizle() 
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox8.Clear();
            maskedTextBox1.Clear();
            comboBox1.SelectedIndex=- 1;
        
        }

        private void kullanici_Load(object sender, EventArgs e)
        {
            //goster();
            textBox1.MaxLength = 11; // Textbox 1 deki kararkter sayısı 11 olacak.
            textBox7.MaxLength = 11;
            toolTip1.SetToolTip(this.textBox1, "Tc Kimlik No 11 Karakter Olmalıdır...");
            toolTip1.SetToolTip(this.textBox7, "Tc Kimlik No 11 Karakter Olmalıdır...");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            goster();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //Kayıt Ekle

            bool kayitkontrol = false;
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("select *from kullanici where tcno='" + textBox1.Text + "'", baglanti);
            OleDbDataReader kayitokuma = sorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglanti.Close();

            if (kayitkontrol==false)
            {
                //TC Kontrol
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.White;

                // Ad Kontrol
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.White;

                //Soyad Kontrol
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.White;

                //Kullanıcı Adı Kontrol
                if (textBox4.Text.Length < 2 || textBox4.Text == "")
                    label4.ForeColor = Color.Red;
                else
                    label4.ForeColor = Color.White;

                //Parola Kontorl
                if (textBox5.Text ==""|| parola_skoru<70)
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.White;

                //Parola Tekrar Kontrol
                if (textBox8.Text == "" || textBox5.Text!=textBox8.Text)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.White;

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox8.Text != "" && textBox5.Text == textBox8.Text && parola_skoru >= 70)
                {
                    try
                    {
                        baglanti.Open();
                        OleDbCommand ekle = new OleDbCommand("Insert Into kullanici Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + maskedTextBox1.Text + "','" + comboBox1.Text + "')",baglanti);
                        ekle.ExecuteNonQuery();                       
                        baglanti.Close();
                        MessageBox.Show("Yeni Kullanıcı Kaydı Başarıyla Oluşturuldu..", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        temizle();
                    }
                    catch (Exception hatamesaj)
                    {

                        MessageBox.Show(hatamesaj.Message);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden geçiriniz", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
             else
                {
                    MessageBox.Show("Girilen Tc Kimlik Numarası Daha Önceden Kayıtlıdır..", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Güncelleme İşlemleri

            //TC Kontrol
            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.White;

            // Ad Kontrol
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.White;

            //Soyad Kontrol
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.White;

            //Kullanıcı Adı Kontrol
            if (textBox4.Text.Length < 2 || textBox4.Text == "")
                label4.ForeColor = Color.Red;
            else
                label4.ForeColor = Color.White;

            //Parola Kontorl
            if (textBox5.Text == "" || parola_skoru < 70)
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.White;

            //Parola Tekrar Kontrol
            if (textBox8.Text == "" || textBox5.Text != textBox8.Text)
                label14.ForeColor = Color.Red;
            else
                label14.ForeColor = Color.White;

            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox8.Text != "" && textBox5.Text == textBox8.Text && parola_skoru >= 70)
            {
                try
                {
                    baglanti.Open();
                    OleDbCommand guncelle = new OleDbCommand("update kullanici set  adi='" + textBox2.Text + "',soyadi='" + textBox3.Text + "',kuladi='" + textBox4.Text + "',sifre='" + textBox5.Text + "',eposta='" + textBox6.Text + "',tel='" + maskedTextBox1.Text + "', yetki='" + comboBox1.Text + "' where tcno='" + textBox1.Text + "'", baglanti);
                    guncelle.ExecuteNonQuery();                   
                    baglanti.Close();
                    goster();
                    MessageBox.Show("Kullanıcı Kaydı Başarıyla Güncelledi..", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                }
                catch (Exception hatamesaj)
                {

                    MessageBox.Show(hatamesaj.Message);
                    baglanti.Close();
                }
                baglanti.Close();
               
            }
            else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden geçiriniz", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            baglanti.Close();      
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();            
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
              
        }

        private void button3_Click(object sender, EventArgs e)
        {

            bool kayitarama = false;
            if (textBox7.Text.Length==11)
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("Select *from kullanici where tcno like '" + textBox7.Text + "%'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    textBox1.Text = kayitokuma.GetValue(0).ToString();
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    textBox4.Text = kayitokuma.GetValue(3).ToString();
                    textBox5.Text = kayitokuma.GetValue(4).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    maskedTextBox1.Text = kayitokuma.GetValue(6).ToString();
                    comboBox1.Text = kayitokuma.GetValue(7).ToString();
                    break;
                }
                if (kayitarama==false)
                {
                    MessageBox.Show("Aranan Kayıt Bulunamadı...", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Tc Kimlik Numarasını 11 Haneli Olarak Giriniz", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Silme Kodları
            if (textBox1.Text.Length==11)
            {
                bool kayitarama = false;
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("select *from kullanici where tcno='" + textBox7.Text + "'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    OleDbCommand sil = new OleDbCommand("Delete *from kullanici where tcno='" + textBox7.Text + "'", baglanti);
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarıyla silindi", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    goster();
                    break;
                }
                if (kayitarama == false)
                {
                    MessageBox.Show("Silinecek Kayıt Bulunamadı", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }
              
            }  
            else
                {
                    MessageBox.Show("Tc kİmlik No 11 Karakterli Olarak Yazınız.","Covid-19 Aşı Randevu ve Hasta Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Textbox1 a girilen değer 11 den küçük olduğunda hata yanıp sönecek...
            if (textBox1.Text.Length < 11)
            {
                errorProvider1.SetError(textBox1, "Tc Kimlik 11 Kararkterli Olmalıdır.");
            }
            else
	        {
                errorProvider1.Clear();
	        }
                        
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text.Length < 11)
            {
                errorProvider1.SetError(textBox7, "Tc Kimlik 11 Kararkterli Olmalıdır.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // tc
            
            if ((int)e.KeyChar>=48 && (int)e.KeyChar<=57|| (int)e.KeyChar==8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // yazı
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        int parola_skoru = 0; 
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            //R
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');
            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!");
            }
            //1 küçük harf 10 puan, 2 ve üzeri 20 puan
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;
            //1 büyük harf 10 puan, 2 ve üzeri 20 puan
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;
            //1 rakam 10 puan, 2 ve üzeri 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;
            //1 sembol 10 puan, 2 ve üzeri 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;
            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;
            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label13.Text = "Büyük harf, küçük harf, rakam ve sembol mutlaka kullanmalısın!";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label13.Text = "";

            if (parola_skoru < 70)
                parola_seviyesi = "Kabul edilemez!";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";
            label11.Text = "%" + Convert.ToString(parola_skoru);
            label12.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != textBox5.Text)
                errorProvider1.SetError(textBox8, "Şifre Tekrarı Uyuşmuyor...");
            else
                errorProvider1.Clear();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
