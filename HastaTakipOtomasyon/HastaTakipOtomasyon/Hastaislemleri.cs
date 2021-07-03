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
    public partial class Hastaislemleri : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hasta.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();


        public Hastaislemleri()
        {
            InitializeComponent();
        }

        private void goster()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter getir = new OleDbDataAdapter("select tc AS[Tc Kimlik No],hastaadi AS[Hasta Adı],hastasoyadi AS[Hasta Soyadı],dogumtarihi AS[Doğum Tarihi],dogumyeri AS[Doğum Yeri],cinsiyet AS[Cinsiyeti],medenihali AS[Medeni Hali],kangurubu AS[Kan Grubu],alerji AS[Hasta Alerji Durumu],hastagiris AS[Hasta Giriş Tarihi],hastacikis AS[Hasta Çıkıç Tarihi],adres AS[Adresi],tel AS[Telefon],doktor AS[Doktoru],bolum AS[Bölüm] from hastalar Order By hastaadi ASC", baglanti);
                DataSet hafiza = new DataSet();
                getir.Fill(hafiza);
                dataGridView1.DataSource = hafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamesaj)
            {

                MessageBox.Show(hatamesaj.Message, "Hasta Taki Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }

        }
        private void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            textBox6.Clear();
            textBox9.Clear();
            maskedTextBox1.Clear();
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            goster();
        }
        
       // String.Format("{0:M/d/yyyy}", dt);
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
           // dateTimePicker4.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            comboBox4.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            comboBox5.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            //Güncelleme Kodları

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

            //&& textBox5.Text != "" && textBox6.Text != "" && textBox9.Text !=""
            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1)
                {
                try
                {
                    baglanti.Open();
                    OleDbCommand güncelle = new OleDbCommand("update hastalar set hastaadi='" + textBox2.Text + "', hastasoyadi='" + textBox3.Text + "', dogumtarihi='" + dateTimePicker1.Text + "', dogumyeri='"+textBox5.Text+"',cinsiyet='"+comboBox1.Text+"',medenihali='"+comboBox2.Text+"',kangurubu='"+comboBox3.Text+"',alerji='"+textBox6.Text+"',hastagiris='"+dateTimePicker2.Text+"',hastacikis='"+dateTimePicker4.Text+"',adres='"+textBox9.Text+"',tel='"+maskedTextBox1.Text+"',doktor='"+comboBox4.Text+"', bolum='"+comboBox5.Text+"' where tc='" + textBox1.Text + "'", baglanti);
                    güncelle.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kullanıcı Kaydı Başarıyla Güncelledi..", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                catch (Exception hatamesaj)
                {

                    MessageBox.Show(hatamesaj.Message);
                    baglanti.Close();
                }

            }
            else
            {
                MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden geçiriniz", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();


           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Arama Kodları
            bool kayitarama = false;
            if (textBox1.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("Select *from hastalar where tc like '" + textBox1.Text + "%'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    textBox1.Text = kayitokuma.GetValue(0).ToString();
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    dateTimePicker1.Text = kayitokuma.GetValue(3).ToString();
                    textBox5.Text = kayitokuma.GetValue(4).ToString();
                    comboBox1.Text = kayitokuma.GetValue(5).ToString();
                    comboBox2.Text = kayitokuma.GetValue(6).ToString();
                    comboBox3.Text = kayitokuma.GetValue(7).ToString();
                    textBox6.Text = kayitokuma.GetValue(8).ToString();
                    dateTimePicker2.Text = kayitokuma.GetValue(9).ToString();
                  //  dateTimePicker4.Text = kayitokuma.GetValue(10).ToString();
                    textBox9.Text = kayitokuma.GetValue(11).ToString();
                    maskedTextBox1.Text = kayitokuma.GetValue(12).ToString();
                    comboBox4.Text = kayitokuma.GetValue(13).ToString();
                    comboBox5.Text = kayitokuma.GetValue(14).ToString();

                    break;
                }
                if (kayitarama == false)
                {
                    MessageBox.Show("Aranan Kayıt Bulunamadı...", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Tc Kimlik Numarasını 11 Haneli Olarak Giriniz", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {

            //Silme Kodları

            if (textBox1.Text.Length == 11)
            {
                bool kayitarama = false;
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("select *from hastalar where tc='" + textBox1.Text + "'", baglanti);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitarama = true;
                    OleDbCommand sil = new OleDbCommand("Delete *from hastalar where tc='" + textBox1.Text + "'", baglanti);
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarıyla silindi", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    goster();
                    break;
                }
                if (kayitarama == false)
                {
                    MessageBox.Show("Silinecek Kayıt Bulunamadı", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }

            }
            else
            {
                MessageBox.Show("Tc kİmlik No 11 Karakterli Olarak Yazınız.", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Hastaislemleri_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 11; // Textbox 1 deki kararkter sayısı 11 olacak.
           
            toolTip1.SetToolTip(this.textBox1, "Tc Kimlik No 11 Karakter Olmalıdır...");
            
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //HASTA EKLEME KODLARI

            bool kayitkontrol = false;
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("select *from hastalar where tc='" + textBox1.Text + "'", baglanti);
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
                                      
                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1)
                {
                    try
                    {
                        baglanti.Open();
                        OleDbCommand ekle = new OleDbCommand("Insert Into hastalar Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Text + "','" + textBox5.Text + "','"+comboBox1.Text+"','"+comboBox2.Text+"','"+comboBox3.Text+"','" + textBox6.Text + "','"+dateTimePicker2.Text+"','"+dateTimePicker4+"','"+textBox9.Text+"','" + maskedTextBox1.Text + "','" + comboBox4.Text + "','"+comboBox5.Text+"')", baglanti);
                        ekle.ExecuteNonQuery();
                        baglanti.Close();
                        goster();
                        MessageBox.Show("Yeni Kullanıcı Kaydı Başarıyla Oluşturuldu..", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden geçiriniz", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Girilen Tc Kimlik Numarası Daha Önceden Kayıtlıdır..", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            baglanti.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void hasta_ekle_Click(object sender, EventArgs e)
        {
            Form2 form2sec = new Form2();
            form2sec.Show();
            this.Hide();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //HASTA EKLEME KODLARI

            bool kayitkontrol = false;
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("select *from hastalar where tc='" + textBox1.Text + "'", baglanti);
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

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1)
                {
                    try
                    {
                        baglanti.Open();
                        OleDbCommand ekle = new OleDbCommand("Insert Into hastalar Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + textBox6.Text + "','" + dateTimePicker2.Text + "','" + dateTimePicker4 + "','" + textBox9.Text + "','" + maskedTextBox1.Text + "','" + comboBox4.Text + "','" + comboBox5.Text + "')", baglanti);
                        ekle.ExecuteNonQuery();
                        baglanti.Close();
                        goster();
                        MessageBox.Show("Yeni Kullanıcı Kaydı Başarıyla Oluşturuldu..", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden geçiriniz", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Girilen Tc Kimlik Numarası Daha Önceden Kayıtlıdır..", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            baglanti.Close();
        }
    }
}
