using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaTakipOtomasyon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter da;

        // Kişileri listelemek için metot oluşturuyoruz.

        void KişiListele()
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=hasta.accdb");
            baglanti.Open();
            da = new OleDbDataAdapter("Select *From hastalar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }

        //For yüklendiğinde metodu çağırıyoruz.
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "Insert into hastalar (tc,hastaadi,hastasoyadi,dogumtarihi,dogumyeri,cinsiyet,medenihali,kangurubu,alerji,hastagiris,hastacikis,adres,tel,doktor,bolum) values (@tc,@hastaadi,@hastasoyadi,@dogumtarihi,@dogumyeri,@cinsiyet,@medenihali,@kangurubu,@alerji,@hastagiris,@hastacikis,@adres,@tel,@doktor,@bolum)";
            komut = new OleDbCommand(sorgu, baglanti);
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
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KişiListele();
        }

        //Silme işlemi
        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete From hastalar Where Numara=@tc";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells[0].Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KişiListele();
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
