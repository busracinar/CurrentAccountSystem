using CariHesapOtomasyonu.Entity;
using CariHesapOtomasyonu.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesapOtomasyonu.DAL
{
    static class Common
    {
        public static (Customer, bool) CustomerCUD(Customer customer, EntityState entityState)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                c.Entry(customer).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (customer, true);
                }
                else
                {
                    return (customer, false);
                }
            }
        }
        public static (Product, bool) ProductCUD(Product product, EntityState entityState)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                c.Entry(product).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (product, true);
                }
                else
                {
                    return (product, false);
                }
            }
        }
        public static (Category, bool) CategoryCUD(Category category, EntityState entityState)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                c.Entry(category).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (category, true);
                }
                else
                {
                    return (category, false);
                }
            }
        }
        public static (User, bool) UserCUD(User user, EntityState entityState)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                c.Entry(user).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (user, true);
                }
                else
                {
                    return (user, false);
                }
            }
        }
        public static List<Customer> GetCustomerList()
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Customer.ToList();
            }
        }
        public static List<Product> GetProductList()
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Product.ToList();
            }
        }
        public static List<Category> GetCategoryList()
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Category.ToList();
            }
        }
        public static List<Sale> GetSaleList()
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Sale.ToList();
            }
        }
        public static User GetUserByName(string userName)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.User.Where(x => x.UserName == userName).FirstOrDefault();
            }
        }
        public static User GetUserById(int userId)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.User.Find(userId);
            }
        }
        public static Product GetProductById(int productId)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Product.Find(productId);
            }
        }
        public static Customer GetCustomerById(int customerId)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Customer.Find(customerId);
            }
        }
        public static Category GetCategoryById(int categoryId)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Category.Find(categoryId);
            }
        }
        public static List<Product> GetProductByCategoryId(int categoryId)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                return c.Product.Where(x => x.CategoryId == categoryId).ToList();
            }
        }
        public static (Sale, bool) SaleAdd(Sale sale)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                var a = c.Sale.Add(sale);
                if (c.SaveChanges() > 0)
                {
                    return (a, true);
                }
                else
                {
                    return (a, false);
                }
            }
        }
        public static List<SaleModel> GetSaleModelList()
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                List<Sale> sales = GetSaleList();
                List<SaleModel> saleModels = new List<SaleModel>();
                foreach (var item in sales)
                {
                    SaleModel saleModel = new SaleModel();
                    saleModel.SaleId = item.SaleId;
                    saleModel.Count = item.Count;
                    saleModel.Date = item.Date;
                    saleModel.Product = GetProductById((int)item.ProductId);
                    saleModel.Custemer = GetCustomerById((int)item.CustemerId);
                    saleModels.Add(saleModel);
                }
                return saleModels;
            }
        }
        public static List<SaleModel> GetSaleModelByCustomerName(string customerName)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                List<SaleModel> saleModelsHelper = new List<SaleModel>();
                List<SaleModel> saleModels = GetSaleModelList();
                foreach (var item in saleModels)
                {
                    if (item.Custemer.Name.ToLower().Contains(customerName.ToLower()))
                    {
                        saleModelsHelper.Add(item);
                    }
                }
                return saleModelsHelper;
            }
        }
        public static List<SaleModel> GetSaleModelByProductName(string productName)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                List<SaleModel> saleModelsHelper = new List<SaleModel>();
                List<SaleModel> saleModels = GetSaleModelList();
                foreach (var item in saleModels)
                {
                    if (item.Product.Name.ToLower().Contains(productName.ToLower()))
                    {
                        saleModelsHelper.Add(item);
                    }
                }
                return saleModelsHelper;
            }
        }
        public static List<SaleModel> GetSaleModelByCategoryName(string categoryName)
        {
            using (CariHesapDbEntities c = new CariHesapDbEntities())
            {
                List<SaleModel> saleModelsHelper = new List<SaleModel>();
                List<SaleModel> saleModels = GetSaleModelList();
                foreach (var item in saleModels)
                {
                    Category category = GetCategoryById((int)item.Product.CategoryId);
                    if (category.Name.ToLower().Contains(categoryName.ToLower()))
                    {
                        saleModelsHelper.Add(item);
                    }
                }
                return saleModelsHelper;
            }
        }
    }
}
