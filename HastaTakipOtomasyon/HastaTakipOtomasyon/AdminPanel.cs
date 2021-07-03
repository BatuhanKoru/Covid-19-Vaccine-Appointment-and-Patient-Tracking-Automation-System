using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaTakipOtomasyon
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hastaislemleri ac = new Hastaislemleri();
            ac.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoktorIslemleri ac = new DoktorIslemleri();
            ac.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kullanici ac = new kullanici();
            ac.Show();
        }

        private void buttın6_Click(object sender, EventArgs e)
        {
            //CovidRandevu ac = new CovidRandevu();
            //ac.Show();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimleri\\" + Form1.tcno + ".jpg");
            }
            catch
            {

                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimleri\\resimyok.jpeg");
            }
            //label3.ForeColor = Color.DarkRed;
            label3.Text = Form1.adi + " " + Form1.soyadi;
            label5.Text = Form1.kuladi;
            label8.Text = Form1.tel;
            label9.Text = Form1.eposta;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //odeme ac = new odeme();
            //ac.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
