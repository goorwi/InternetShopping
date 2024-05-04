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
    public class OrderItemBD
    {
        private const string CONNECTION_STRING = @"Server=Tpshka;DataBase=Shop;Trusted_Connection=True;";

        public int Create(int ProductId, int Amount, int OrderId, int StockroomId) 
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var product = new ProductBD().SearchById(ProductId);
                var order = new OrderBD().SearchById(OrderId);
                var stockroom = new StockroomBD().SearchById(StockroomId);
                if (product != null && order != null && stockroom != null)
                    return db.GetTable<OrderItem>()
                        .Value(p => p.ProductId, product.Id)
                        .Value(p => p.Amount, Amount)
                        .Value(p => p.Price, product.Price)
                        .Value(p => p.OrderId, order.Id)
                        .Value(p => p.StockroomId, stockroom.Id)
                        .Insert();
                else 
                    return -1;
            }
        }

        public List<OrderItem> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product).LoadWith(request => request.order).LoadWith(request => request.stockroom).ToList();
            }
        }

        public OrderItem? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product).LoadWith(request => request.order).LoadWith(request => request.stockroom)
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public OrderItem? SearchByProduct(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product).LoadWith(request => request.order).LoadWith(request => request.stockroom)
                    .Where(p => p.product.Id == Id)
                    .FirstOrDefault();
            }
        }

        public OrderItem? SearchByAmount(int Amount)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product).LoadWith(request => request.order).LoadWith(request => request.stockroom)
                    .Where(p => p.Amount == Amount)
                    .FirstOrDefault();
            }
        }

        public OrderItem? SearchByOrder(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product).LoadWith(request => request.order).LoadWith(request => request.stockroom)
                    .Where(p => p.order.Id == Id)
                    .FirstOrDefault();
            }
        }

        public OrderItem? SearchByStockroom(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product).LoadWith(request => request.order).LoadWith(request => request.stockroom)
                    .Where(p => p.stockroom.Id == Id)
                    .FirstOrDefault();
            }
        }

        public int Update(int Id, int ProductId, int Amount, int OrderId, int StockroomId)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var product = new ProductBD().SearchById(ProductId);
                var order = new OrderBD().SearchById(OrderId);
                var stockroom = new StockroomBD().SearchById(StockroomId);
                if (product != null && order != null && stockroom != null)
                    return db.GetTable<OrderItem>()
                        .Where(c => c.Id == Id)
                        .Set(c => c.ProductId, ProductId)
                        .Set(c => c.ProductId, ProductId)
                        .Set(c => c.ProductId, ProductId)
                        .Set(c => c.ProductId, ProductId)
                        .Update();
                return -1;
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
