using CariHesapOtomasyonu.DAL;
using CariHesapOtomasyonu.Entity;
using CariHesapOtomasyonu.Model;
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
    public partial class Form2 : Form
    {
        string messageBoxHeader = "Bilgilendirme";
        User user = new User();
        public Form2(User user)
        {
            InitializeComponent();
            RefreshTheDatabase();
            this.user = user;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.Name = textBox1.Text;
            c.Surname = textBox2.Text;
            c.Phone = textBox3.Text;
            c.Address = textBox4.Text;
            c.UserId = user.UserId;
            c.IsDeleted = false;
            var a = Common.CustomerCUD(c, System.Data.Entity.EntityState.Added);
            if (a.Item2)
            {
                RefreshTheDatabase();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                MessageBox.Show("Müşteri eklendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Müşteri eklenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void RefreshTheDatabase()
        {
            dataGridView1.Rows.Clear();
            List<Customer> customers = Common.GetCustomerList();
            foreach (var item in customers)
            {
                if (!item.IsDeleted)
                {
                    dataGridView1.Rows.Add(item.Name, item.Surname, item.Phone, item.Address, item.CustomerId);
                }
            }
            dataGridView2.Rows.Clear();
            List<Product> products = Common.GetProductList();
            foreach (var item in products)
            {
                Category category = Common.GetCategoryById((int)item.CategoryId);
                if (!item.IsDeleted)
                {
                    dataGridView2.Rows.Add(item.Name, category.Name, item.ArrivalPrice, item.SalePrice, item.AvailableStock, item.Description, item.ProductId);
                }
            }
            dataGridView3.Rows.Clear();
            List<Category> categories = Common.GetCategoryList();
            foreach (var item in categories)
            {
                if (!item.IsDeleted)
                {
                    dataGridView3.Rows.Add(item.Name, item.Description, item.CategoryId);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Customer c = Common.GetCustomerById(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value));
            textBox8.Text = c.Name;
            textBox7.Text = c.Surname;
            textBox6.Text = c.Phone;
            textBox5.Text = c.Address;
            label29.Text = c.CustomerId.ToString();
            groupBox4.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Customer c = Common.GetCustomerById(Convert.ToInt32(label29.Text));
            c.Name = textBox8.Text;
            c.Surname = textBox7.Text;
            c.Phone = textBox6.Text;
            c.Address = textBox5.Text;
            var a = Common.CustomerCUD(c, System.Data.Entity.EntityState.Modified);
            if (a.Item2)
            {
                RefreshTheDatabase();
                textBox8.Clear();
                textBox7.Clear();
                textBox6.Clear();
                textBox5.Clear();
                label29.Text = null;
                groupBox4.Enabled = false;
                MessageBox.Show("Müşteri güncellendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Müşteri güncellenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Index.ToString()))
            {
                MessageBox.Show("Herhangi bir müşteri seçmediniz. Silmek için önce müşteri seçiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var a = MessageBox.Show("Seçilen müşteriyi silmek istediğinizden emin misiniz?", messageBoxHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (a == DialogResult.Yes)
                {
                    var b = Common.CustomerCUD(Common.GetCustomerById(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)), System.Data.Entity.EntityState.Deleted);
                    if (b.Item2)
                    {
                        RefreshTheDatabase();
                        MessageBox.Show("Müşteri silindi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Müşteri silinemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<Category> categories = Common.GetCategoryList();
            string[] categoryNames = new string[categories.Count];
            string[] categoryIds = new string[categories.Count];
            for (int i = 0; i < categoryNames.Length; i++)
            {
                categoryNames[i] = categories[i].Name;
                categoryIds[i] = categories[i].CategoryId.ToString();
            }
            label1.Text = $"Hoş Geldiniz {user.UserName}";
            comboBox1.Items.AddRange(categoryNames);
            comboBox2.Items.AddRange(categoryNames);
            comboBox3.Items.AddRange(categoryNames);
            comboBox4.Items.AddRange(categoryIds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                List<Category> categories = Common.GetCategoryList();
                Product p = new Product();
                p.Name = textBox16.Text;
                p.ArrivalPrice = Convert.ToInt32(textBox15.Text);
                p.SalePrice = Convert.ToInt32(textBox14.Text);
                p.InitialStock = Convert.ToInt32(textBox13.Text);
                p.AvailableStock = Convert.ToInt32(textBox13.Text);
                p.Description = textBox17.Text;
                p.IsDeleted = false;
                foreach (var item in categories)
                {
                    if (item.Name.ToLower() == comboBox1.Text.ToLower())
                    {
                        p.CategoryId = item.CategoryId;
                        break;
                    }
                }
                var a = Common.ProductCUD(p, System.Data.Entity.EntityState.Added);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox16.Clear();
                    textBox15.Clear();
                    textBox14.Clear();
                    textBox13.Clear();
                    textBox17.Clear();
                    comboBox1.Text = null;
                    MessageBox.Show("Ürün eklendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ürün eklenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (FormatException exc)
            {
                MessageBox.Show("Yeni ürün bilgileri hatalı. Kontrol edip tekrar deneyiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Product p = Common.GetProductById(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value));
            Category c = Common.GetCategoryById((int)p.CategoryId);
            textBox18.Text = p.Name;
            comboBox2.Text = c.Name;
            textBox12.Text = p.ArrivalPrice.ToString();
            textBox11.Text = p.SalePrice.ToString();
            textBox10.Text = p.AvailableStock.ToString();
            textBox9.Text = p.Description;
            label30.Text = p.ProductId.ToString();
            groupBox5.Enabled = true;
            comboBox2.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                List<Category> categories = Common.GetCategoryList();
                Product p = Common.GetProductById(Convert.ToInt32(label30.Text));
                p.Name = textBox18.Text;
                p.ArrivalPrice = Convert.ToInt32(textBox12.Text);
                p.SalePrice = Convert.ToInt32(textBox11.Text);
                p.Description = textBox9.Text;
                p.IsDeleted = false;
                foreach (var item in categories)
                {
                    if (item.Name.ToLower() == comboBox2.Text.ToLower())
                    {
                        p.CategoryId = item.CategoryId;
                        break;
                    }
                }
                var a = Common.ProductCUD(p, System.Data.Entity.EntityState.Modified);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox18.Clear();
                    textBox12.Clear();
                    textBox11.Clear();
                    textBox10.Clear();
                    textBox9.Clear();
                    comboBox2.Text = null;
                    groupBox5.Enabled = false;
                    MessageBox.Show("Ürün güncellendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ürün güncellenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (FormatException exc)
            {
                MessageBox.Show("Ürün bilgileri hatalı. Kontrol edip tekrar deneyiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dataGridView2.CurrentRow.Index.ToString()))
            {
                MessageBox.Show("Herhangi bir ürün seçmediniz. Silmek için önce ürün seçiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var a = MessageBox.Show("Seçilen ürünü silmek istediğinizden emin misiniz?", messageBoxHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (a == DialogResult.Yes)
                {
                    var b = Common.ProductCUD(Common.GetProductById(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value)), System.Data.Entity.EntityState.Deleted);
                    if (b.Item2)
                    {
                        RefreshTheDatabase();
                        MessageBox.Show("Ürün silindi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ürün silinemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Category c = Common.GetCategoryById(Convert.ToInt32(dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[2].Value));
            textBox22.Text = c.Name;
            textBox21.Text = c.Description;
            label31.Text = c.CategoryId.ToString();
            groupBox12.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.Name = textBox19.Text;
            c.Description = textBox20.Text;
            c.IsDeleted = false;
            var a = Common.CategoryCUD(c, System.Data.Entity.EntityState.Added);
            if (a.Item2)
            {
                RefreshTheDatabase();
                textBox19.Clear();
                textBox20.Clear();
                MessageBox.Show("Kategori eklendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kategori eklenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Category c = Common.GetCategoryById(Convert.ToInt32(label31.Text));
            c.Name = textBox22.Text;
            c.Description = textBox21.Text;
            var a = Common.CategoryCUD(c, System.Data.Entity.EntityState.Modified);
            if (a.Item2)
            {
                RefreshTheDatabase();
                textBox22.Clear();
                textBox21.Clear();
                label31.Text = null;
                groupBox12.Enabled = false;
                MessageBox.Show("Kategori güncellendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kategori güncellenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dataGridView3.CurrentRow.Index.ToString()))
            {
                MessageBox.Show("Herhangi bir kategori seçmediniz. Silmek için önce kategori seçiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var a = MessageBox.Show("Seçilen kategoriyi silmek istediğinizden emin misiniz?", messageBoxHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (a == DialogResult.Yes)
                {
                    var b = Common.CategoryCUD(Common.GetCategoryById(Convert.ToInt32(dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[2].Value)), System.Data.Entity.EntityState.Deleted);
                    if (b.Item2)
                    {
                        RefreshTheDatabase();
                        MessageBox.Show("Kategori silindi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kategori silinemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox23.Text == user.Password)
            {
                if (textBox24.Text == textBox24.Text)
                {
                    user.Password = textBox24.Text;
                    var a = Common.UserCUD(user, System.Data.Entity.EntityState.Modified);
                    if (a.Item2)
                    {
                        textBox23.Clear();
                        textBox24.Clear();
                        textBox25.Clear();
                        label32.Text = null;
                        MessageBox.Show("Kullanıcı şifreniz değiştirildi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı şifreniz değiştirilemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    label32.Text = "Girdiğiniz yeni şifreler uyuşmuyor. ";
                }
            }
            else
            {
                label32.Text = "Geçerli şifreyi yanlış girdiniz. ";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView5.Rows.Clear();
            dataGridView5.Visible = true;
            label37.Visible = true;
            comboBox7.Items.Clear();
            comboBox5.Items.Clear();
            List<Product> products = Common.GetProductByCategoryId(Convert.ToInt32(comboBox4.Items[comboBox3.SelectedIndex]));
            foreach (var item in products)
            {
                if (!item.IsDeleted)
                {
                    dataGridView5.Rows.Add(item.Name, item.SalePrice, item.Description, item.ProductId);
                }
            }
            List<Customer> customers = Common.GetCustomerList();
            foreach (var item in customers)
            {
                if (!item.IsDeleted)
                {
                    comboBox7.Items.Add(item.CustomerId);
                    comboBox5.Items.Add(item.Name + " " + item.Surname);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = Common.GetProductById(Convert.ToInt32(dataGridView5.Rows[dataGridView5.CurrentRow.Index].Cells[3].Value));
                if (product.AvailableStock == 0)
                {
                    MessageBox.Show($"Stokda hiç {product.Name} bulunmamakta.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else if (product.AvailableStock < Convert.ToInt32(textBox27.Text))
                {
                    MessageBox.Show($"Stokda sadece {product.AvailableStock} adet {product.Name} bulunmakta. Lütfen adedi düşürüp tekrar deneyiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Sale sale = new Sale();
                    sale.ProductId = Convert.ToInt32(dataGridView5.Rows[dataGridView5.CurrentRow.Index].Cells[3].Value);
                    sale.Count = Convert.ToInt32(textBox27.Text);
                    sale.CustemerId = Convert.ToInt32(comboBox7.Items[comboBox5.SelectedIndex]);
                    sale.Date = DateTime.Now;
                    var a = Common.SaleAdd(sale);
                    if (a.Item2)
                    {
                        MessageBox.Show("Satış eklendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox26.Clear();
                        textBox27.Clear();
                        comboBox5.Text = null;
                        product.AvailableStock -= sale.Count;
                        var b = Common.ProductCUD(product, System.Data.Entity.EntityState.Modified);
                        if (!b.Item2)
                        {
                            MessageBox.Show("Stok güncellenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        RefreshTheDatabase();
                    }
                    else
                    {
                        MessageBox.Show("Satış eklenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (FormatException exc)
            {
                MessageBox.Show("Satış bilgileri hatalı. Kontrol edip tekrar deneyiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Lütfen hangi bölgede arama yapacağınızı seçiniz.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SearchByRadioButton();
            }
        }

        public void SearchByRadioButton()
        {
            List<SaleModel> saleModels = new List<SaleModel>();
            if (radioButton1.Checked)
            {
                saleModels = Common.GetSaleModelByCustomerName(textBox28.Text);

            }
            else if (radioButton2.Checked)
            {
                saleModels = Common.GetSaleModelByProductName(textBox28.Text);
            }
            else if (radioButton3.Checked)
            {
                saleModels = Common.GetSaleModelByCategoryName(textBox28.Text);
            }
            dataGridView4.Rows.Clear();
            DateTime dateTime1 = new DateTime((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value);
            DateTime dateTime2 = new DateTime((int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value);
            foreach (var item in saleModels)
            {
                if (item.Date >= dateTime1 && item.Date <= dateTime2)
                {
                    Category category = Common.GetCategoryById((int)item.Product.CategoryId);
                    dataGridView4.Rows.Add(item.Custemer.Name + " " + item.Custemer.Surname, category.Name, item.Product.Name, item.Count, item.Product.SalePrice, item.Date);
                }
            }
        }

        public void Search()
        {
            List<SaleModel> saleModels = Common.GetSaleModelList();
            dataGridView4.Rows.Clear();
            DateTime dateTime1 = new DateTime((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value);
            DateTime dateTime2 = new DateTime((int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value);
            foreach (var item in saleModels)
            {
                if (item.Date >= dateTime1 && item.Date <= dateTime2)
                {
                    Category category = Common.GetCategoryById((int)item.Product.CategoryId);
                    dataGridView4.Rows.Add(item.Custemer.Name + " " + item.Custemer.Surname, category.Name, item.Product.Name, item.Count, item.Product.SalePrice, item.Date);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SearchByRadioButton();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SearchByRadioButton();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SearchByRadioButton();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
            double girdi = 0.0;
            double cikti = 0.0;
            List<SaleModel> saleModels = Common.GetSaleModelList();
            List<Product> products = Common.GetProductList();
            foreach (var item in products)
            {
                cikti += item.InitialStock * item.ArrivalPrice;
                girdi += (item.InitialStock - item.AvailableStock) * item.SalePrice;
            }
            label45.Text = $"KAR - ZARAR DURUMU : {girdi - cikti}";
        }

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            panel1.Visible = true;
            textBox26.Text = dataGridView5.Rows[dataGridView5.CurrentRow.Index].Cells[0].Value.ToString();
        }
    }
}
