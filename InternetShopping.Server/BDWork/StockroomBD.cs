using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using ShopLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestShop
{
    public class StockroomBD
    {
        private const string CONNECTION_STRING = @"Server=Tpshka;DataBase=Shop;Trusted_Connection=True;";
        public int Create(int productId, string Address, int Amount)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var product = new ProductBD().SearchById(productId);
                if (product != null)
                {
                    return db.GetTable<Stockroom>()
                        .Value(p => p.ProductId, product.Id)
                        .Value(p => p.Address, Address)
                        .Value(p => p.Amount, Amount)
                        .Insert();
                }
                else return -1;
            }
        }

        public List<Stockroom> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Stockroom>().LoadWith(request => request.product).ToList();
            }
        }

        public Stockroom? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Stockroom>().LoadWith(request => request.product)
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();            
            }
        }

        public Stockroom? SearchByProduct(int ProductId)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Stockroom>().LoadWith(request => request.product)
                    .Where(p => p.product.Id == ProductId)
                    .FirstOrDefault();
            }
        }

        public Stockroom? SearchByAddress(string Address)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Stockroom>()
                    .Where(p => p.Address == Address)
                    .FirstOrDefault();
            }
        }

        public Stockroom? SearchByAmount(int Amount)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Stockroom>()
                    .Where(p => p.Amount == Amount)
                    .FirstOrDefault();
            }
        }

        public int UpdateAmount(int Id, int ProductId, string Address, int Amount)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var product = new ProductBD().SearchById(ProductId);
                if (product != null)
                    return db.GetTable<Stockroom>()
                        .Where(c => c.Id == Id)
                        .Set(c => c.ProductId, product.Id)
                        .Set(c => c.Address, Address)
                        .Set(c => c.Amount, Amount)
                        .Update();
                else return -1;
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Stockroom>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
