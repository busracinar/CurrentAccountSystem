using CariHesapOtomasyonu.DAL;
using CariHesapOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariHesapOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = Common.GetUserByName(textBox1.Text.Trim());
            if (user==null)
            {
                label4.Text = "Kullanıcı adı yanlış.";
            }
            else
            {
                if (user.Password==textBox2.Text)
                {
                    Form2 f = new Form2(user);
                    this.Hide();
                    f.Show();
                }
                else
                {
                    label4.Text = "Şifre yanlış.";
                }
            }
        }
    }
}
