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
    public partial class HastaEkle : Form
    {
        readonly OleDbConnection ekle = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=hasta.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=hasta.accdb");
        

        public HastaEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayitkontrol = false;
            baglan.Open();
            
            {
               
            }
            baglan.Close();

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

                //Kullanıcı Adı Kontrol
                if (textBox4.Text.Length < 2 || textBox4.Text == "")
                    label4.ForeColor = Color.Red;
                else
                    label4.ForeColor = Color.White;

                

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox8.Text != "" /*&& textBox5.Text == textBox8.Text !=""*/)                
                {
                    try
                    {
                        //baglan.Open();
                        //OleDbCommand ekle = new OleDbCommand("Insert Into kullanici Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + /*MaskedTextBox*/"','" + comboBox1.Text + "')",baglan);
                        //ekle.ExecuteNonQuery()
                        //baglan.Close();
                        //MessageBox.Show("Yeni Kullanıcı Kaydı Başarıyla Oluşturuldu..", "Hasta Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    catch (Exception hatamesaj)
                    {

                        //MessageBox.Show(hatamesaj.Message);
                        //baglan.Close();
                    }
                }
                else
                {
                   
                }

            }
            else
            {
            

            }

            baglan.Close();
            
            string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hasta.accdb";
            OleDbConnection baglanti = new OleDbConnection(vtyolu);
            baglanti.Open();
            string ekle = "insert into hastalar(tc,hastaadi,hastasoyadi,dogumtarihi,dogumyeri,cinsiyet,medenihali,kangurubu,alerji,hastagiris,hastacikis,adres,tel,doktor,bolum) values (@tc,@hastaadi,@hastasoyadi,@dogumtarihi,@dogumyeri,@cinsiyet,@medenihali,@kangurubu,@alerji,@hastagiris,@hastacikis,@adres,@tel,@doktor,@bolum)";
            OleDbCommand komut = new OleDbCommand(ekle, baglanti);
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@hastaadi", textBox2.Text);
            komut.Parameters.AddWithValue("@hastasoyadi", textBox3.Text);
            komut.Parameters.AddWithValue("@dogumtarihi", textBox4.Text);
            komut.Parameters.AddWithValue("@dogumyeri", textBox5.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
            komut.Parameters.AddWithValue("@medenihali", comboBox2.Text);
            komut.Parameters.AddWithValue("@kangurubu", comboBox3.Text);
            komut.Parameters.AddWithValue("@alerji", textBox6.Text);
            komut.Parameters.AddWithValue("@hastagiris", textBox7.Text);
            komut.Parameters.AddWithValue("@hastacikis", textBox8.Text);
            komut.Parameters.AddWithValue("@adres", textBox9.Text);
            komut.Parameters.AddWithValue("@tel", textBox10.Text);
            komut.Parameters.AddWithValue("@doktor", comboBox4.Text);
            komut.Parameters.AddWithValue("@bolum", comboBox5.Text);
            //komut.ExecuteNonQuery();
            MessageBox.Show("Hasta Başarıyla Kayededildi..");
        }

        private void HastaEkle_Load(object sender, EventArgs e)
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
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }
