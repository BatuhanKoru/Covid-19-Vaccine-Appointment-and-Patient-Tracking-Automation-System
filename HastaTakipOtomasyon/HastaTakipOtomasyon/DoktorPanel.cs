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
    public partial class DoktorPanel : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hasta.accdb");
        OleDbCommand komut = new OleDbCommand();
        
        
        public DoktorPanel()
        {
            InitializeComponent();
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void DoktorPanel_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimleri\\" + Form1.tcno + ".jpg");
            }
            catch 
            {
                
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimleri\\oytun.jpg");
            }
            //label3.ForeColor = Color.DarkRed;
            label3.Text = Form1.adi + " " + Form1.soyadi;
            label5.Text = Form1.kuladi;
            label8.Text = Form1.tel;
            label9.Text = Form1.eposta;

            textBox1.MaxLength = 11; // Textbox 1 deki kararkter sayısı 11 olacak.
            textBox2.MaxLength = 11;
            toolTip1.SetToolTip(this.textBox1, "Tc Kimlik No 11 Karakter Olmalıdır...");
            toolTip1.SetToolTip(this.textBox2, "Tc Kimlik No 11 Karakter Olmalıdır...");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayitarama = false;
            if (textBox1.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("Select *from hastalar where tc like '" + textBox1.Text + "%'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    
                    label23.Text = kayitokuma.GetValue(1).ToString();
                    label24.Text = kayitokuma.GetValue(2).ToString();
                    label25.Text = kayitokuma.GetValue(3).ToString();
                    label26.Text = kayitokuma.GetValue(4).ToString();
                    label27.Text = kayitokuma.GetValue(5).ToString();
                    label28.Text = kayitokuma.GetValue(6).ToString();
                    label29.Text = kayitokuma.GetValue(7).ToString();
                    label30.Text = kayitokuma.GetValue(8).ToString();
                    label31.Text = kayitokuma.GetValue(9).ToString();
                    label32.Text = kayitokuma.GetValue(10).ToString();
                    label33.Text = kayitokuma.GetValue(11).ToString();
                    label34.Text = kayitokuma.GetValue(12).ToString();
                    label37.Text = kayitokuma.GetValue(13).ToString();
                    label38.Text = kayitokuma.GetValue(14).ToString();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
   
        private void button2_Click(object sender, EventArgs e)
        {
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

            if (kayitkontrol == false)
            {
                //TC Kontrol
                if (textBox2.Text.Length < 11 || textBox2.Text == "")
                    label39.ForeColor = Color.Red;
                else
                    label39.ForeColor = Color.White;

                // teşhis
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label40.ForeColor = Color.Red;
                else
                    label40.ForeColor = Color.White;

                //Sonuç
                if (textBox4.Text.Length < 2 || textBox4.Text == "")
                    label41.ForeColor = Color.Red;
                else
                    label41.ForeColor = Color.White;


                if (textBox2.Text.Length == 11 && textBox2.Text != "" && textBox3.Text.Length >1 && textBox3.Text != "" && textBox4.Text.Length > 1 && textBox4.Text != "")
                {
                    try
                    {
                        baglanti.Open();
                        OleDbCommand ekle = new OleDbCommand("Insert Into muane Values ('" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','"+textBox7.Text+"')", baglanti);
                        ekle.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt Başarıyla Oluşturuldu..", "Covid-19 Aşı Randevu ve Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //temizle();
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
        double mr = 0, kan = 0, idrar = 0, rontgen = 0, ultrason = 0, endoskopi = 0, muane = 10;

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                mr = muane + 50;
            }
            if (checkBox2.Checked)
            {
                kan = muane + 10;
            }
            if (checkBox3.Checked)
            {
                idrar = muane + 15;
            }
            if (checkBox4.Checked)
            {
                rontgen = muane + 20;
            }
            if (checkBox5.Checked)
            {
                ultrason = muane + 30;
            }
            if (checkBox6.Checked)
            {
                endoskopi = muane + 40;
            }
        
          
            textBox6.Text = (muane + mr + idrar + rontgen + ultrason + endoskopi + " TL").ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 11)
            {
                errorProvider1.SetError(textBox2, "Tc Kimlik 11 Kararkterli Olmalıdır.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool kayitarama = false;
            if (textBox2.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("Select *from muane where tc like '" + textBox2.Text + "%'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    
                    //textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(1).ToString();
                    comboBox1.Text = kayitokuma.GetValue(2).ToString();
                    textBox4.Text = kayitokuma.GetValue(3).ToString();
                    //textBox5.Text = kayitokuma.GetValue(4).ToString();
                    textBox6.Text = kayitokuma.GetValue(4).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    
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
            Application.Exit();
        }       
    }
}
