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
    public class OrderBD
    {
        private const string CONNECTION_STRING = @"Server=Tpshka;DataBase=Shop;Trusted_Connection=True;";

        public int Create(string customerName, int Cost, string OrderStatus, string OrderDate)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(customerName);
                if (customer != null)
                {
                    return db.GetTable<Order>()
                        .Value(p => p.CustomerId, customer.Id)
                        .Value(p => p.Cost, Cost)
                        .Value(p => p.OrderStatus, OrderStatus)
                        .Value(p => p.OrderDate, OrderDate)
                        .Insert();
                }
                else
                    return -1;
            }
        }

        public List<Order> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>().LoadWith(request => request.customer).ToList();
            }
        }

        public Order? SearchBy(int customerId, int cost, string orderStatus)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>().LoadWith(request => request.customer)
                    .Where(p => p.customer.Id == customerId)
                    .Where(p => p.Cost == cost)
                    .Where(p => p.OrderStatus == orderStatus)
                    .FirstOrDefault();
            }
        }

        public Order? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Order? SearchByCustomer(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.customer.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Order? SearchByCost(int Cost)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Cost == Cost)
                    .FirstOrDefault();
            }
        }

        public Order? SearchByOrderStatus(string OrderStatus)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.OrderStatus == OrderStatus)
                    .FirstOrDefault();
            }
        }

        public Order? SearchByOrderDate(string OrderDate)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.OrderDate == OrderDate)
                    .FirstOrDefault();
            }
        }

        public int UpdateOrder(int Id, string CustomerName, int Cost, string OrderStatus, string OrderDate)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(CustomerName);
                if (customer != null)
                {
                    return db.GetTable<Order>()
                        .Where(c => c.Id == Id)
                        .Set(c => c.CustomerId, customer.Id)
                        .Set(c => c.Cost, Cost)
                        .Set(c => c.OrderStatus, OrderStatus)
                        .Set(c => c.OrderDate, OrderDate)
                        .Update();
                }
                else return -1;
            }
        }

        public void UpdateOrderCost(int Id, int Cost)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                db.GetTable<Order>()
                    .Where(c => c.Id == Id)
                    .Set(c => c.Cost, c => c.Cost + Cost)
                    .Update();
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
