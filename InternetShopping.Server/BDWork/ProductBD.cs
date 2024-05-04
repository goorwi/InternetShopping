using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using ShopLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace TestShop
{
    public class ProductBD
    {
        private const string CONNECTION_STRING = @"Server=Tpshka;DataBase=Shop;Trusted_Connection=True;";

        public int Create(string Title, string categoryTitle, string Producer, string supplierTitle, string Content, int Price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var category = new CategoryBD().SearchByTitle(categoryTitle);
                var supplier = new SupplierBD().SearchByTitle(supplierTitle);
                if (category != null && supplier != null)
                {
                    return db.GetTable<Product>()
                        .Value(p => p.Title, Title)
                        .Value(p => p.CategoryId, category.Id)
                        .Value(p => p.Producer, Producer)
                        .Value(p => p.SupplierId, supplier.Id)
                        .Value(p => p.Content, Content)
                        .Value(p => p.Price, Price)
                        .Insert();
                }
                else
                    return 0;
            }
        }

        public List<Product> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>().LoadWith(request => request.category).LoadWith(request => request.supplier).ToList();
            }
        }

        public int UpdateProduct(int Id, string Title, string CategoryTitle, string Producer, string SupplierTitle, string Content, int Price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var category = new CategoryBD().SearchByTitle(CategoryTitle);
                var supplier = new SupplierBD().SearchByTitle(SupplierTitle);
                if (category != null && supplier != null)
                    return db.GetTable<Product>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.Title, Title)
                        .Set(p => p.CategoryId, category.Id)
                        .Set(p => p.Producer, Producer)
                        .Set(p => p.SupplierId, supplier.Id)
                        .Set(p => p.Content, Content)
                        .Set(p => p.Price, Price)
                        .Update();
                else return -1;
            }
        }

        public Product? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByTitle(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Title == Title)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByCategory(string CategoryTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>().LoadWith(request => request.category)
                    .Where(p => p.category.Title == CategoryTitle)
                    .FirstOrDefault();
            }
        }
        
        public Product? SearchByProducer(string Producer)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Producer == Producer)
                    .FirstOrDefault();
            }
        }
        
        public Product? SearchBySupplier(string SupplierTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>().LoadWith(request => request.supplier)
                    .Where(p => p.supplier.Title == SupplierTitle)
                    .FirstOrDefault();
            }
        }
        
        public Product? SearchByPrice(int Price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Price == Price)
                    .FirstOrDefault();
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
