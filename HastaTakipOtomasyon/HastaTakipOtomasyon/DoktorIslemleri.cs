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
    public partial class DoktorIslemleri : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hasta.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public DoktorIslemleri()
        {
            InitializeComponent();
        }
        private void goster()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter getir = new OleDbDataAdapter("select tc AS[Tc Kimlik No],d_adi AS[Adı],d_soyadi AS[Soyadı],d_bolum AS[Bölüm],adres AS[Adresi],tel AS[Telefon], hastalari AS[Hastaları],resim AS[Resmi] from doktor Order By d_adi ASC", baglanti);
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
        comboBox1.SelectedIndex = -1;
        textBox4.Clear();
        maskedTextBox1.Clear();
        comboBox2.SelectedIndex = -1;
        textBox5.Clear();
    }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            goster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                textBox5.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Doktor Ekle Kodları


            textBox5.Text = pictureBox1.ImageLocation;
            bool kayitkontrol = false;
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("select *from doktor where tc='" + textBox1.Text + "'", baglanti);
            OleDbDataReader kayitokuma = sorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglanti.Close();

            if (kayitkontrol == false)
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
                //Adres Kontrol
                if (textBox4.Text.Length < 2 || textBox4.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.White;



                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "")
                {
                    try
                    {
                        baglanti.Open();
                        OleDbCommand ekle = new OleDbCommand("Insert Into doktor Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','"+comboBox1.Text+"','" + textBox4.Text + "','"+maskedTextBox1.Text+"','"+comboBox2.Text+"','" + textBox5.Text + "')", baglanti);
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

        private void button4_Click(object sender, EventArgs e)
        {
            //Silme Kodları
            if (textBox1.Text.Length == 11)
            {
                bool kayitarama = false;
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("select *from doktor where tc='" + textBox6.Text + "'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    OleDbCommand sil = new OleDbCommand("Delete *from doktor where tc='" + textBox6.Text + "'", baglanti);
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarıyla silindi", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("Tc kİmlik No 11 Karakterli Olarak Yazınız.", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[7].Value.ToString();
           // textBox5.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool kayitarama = false;
            if (textBox7.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("Select *from doktor where tc like '" + textBox7.Text + "%'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    textBox1.Text = kayitokuma.GetValue(0).ToString();
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();                  
                    comboBox1.Text = kayitokuma.GetValue(3).ToString();
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    maskedTextBox1.Text = kayitokuma.GetValue(5).ToString();
                    comboBox2.Text = kayitokuma.GetValue(6).ToString();
                    textBox5.Text = kayitokuma.GetValue(7).ToString();
                    
                    break;
                }
                if (kayitarama == false)
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

        private void button6_Click(object sender, EventArgs e)
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


            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "")
            {
                try
                {
                    baglanti.Open();
                    OleDbCommand guncelle = new OleDbCommand("update doktor set  d_adi='" + textBox2.Text + "',d_soyadi='" + textBox3.Text + "',d_bolum='" + comboBox1.Text + "',adres='" + textBox4.Text + "',tel='" + maskedTextBox1.Text + "',hastalari='" + comboBox2.Text + "', resim='" + textBox5.Text + "' where tc='" + textBox1.Text + "'", baglanti);
                    guncelle.ExecuteNonQuery();
                    baglanti.Close();
                    goster();
                    MessageBox.Show("Kullanıcı Kaydı Başarıyla Güncelledi..", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    temizle();
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
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "update doktor set d_adi='" + textBox2.Text + "', d_soyadi='" + textBox3.Text + "', d_bolum='" + comboBox1.Text + "', adres='" + textBox4.Text + "',tel='"+maskedTextBox1.Text+"',hastalari='"+comboBox2.Text+"', resim='"+textBox5.Text+"' where tc='" + textBox1.Text + "'";
            MessageBox.Show("Kayıt Başarıyla Güncellendi...");
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Clear();
            goster();
        }

        private void DoktorIslemleri_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 11; // Textbox 1 deki kararkter sayısı 11 olacak.
            textBox6.MaxLength = 11;
            textBox7.MaxLength = 11; 
            toolTip1.SetToolTip(this.textBox1, "Tc Kimlik No 11 Karakter Olmalıdır...");
            toolTip1.SetToolTip(this.textBox6, "Tc Kimlik No 11 Karakter Olmalıdır...");
            toolTip1.SetToolTip(this.textBox7, "Tc Kimlik No 11 Karakter Olmalıdır...");
           
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length < 11)
            {
                errorProvider1.SetError(textBox6, "Tc Kimlik 11 Kararkterli Olmalıdır.");
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

            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                //e.Handled = false;
            //else
                //e.Handled = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
