using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using ShopLibrary;

namespace TestShop
{
    public class CustomerBD
    {
        private const string CONNECTION_STRING = @"Server=Tpshka;DataBase=Shop;Trusted_Connection=True;";

        public int Create(string Name, string Address, string Phone, string Email, string Password, bool IsAdmin)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Value(p => p.Name, Name)
                    .Value(p => p.Address, Address)
                    .Value(p => p.Email, Email)
                    .Value(p => p.Phone, Phone)
                    .Value(p => p.Password, Password)
                    .Value(p => p.IsAdmin, false)
                    .Insert();
            }
        }

        public List<Customer> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>().ToList();
            }
        }

        public Customer? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Customer? SearchByName(string Name)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(p => p.Name == Name)
                    .FirstOrDefault();
            }
        }

        public Customer? SearchByAddress(string Address)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(p => p.Address == Address)
                    .FirstOrDefault();
            }
        }

        public Customer? SearchByPhone(string Phone)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(p => p.Phone == Phone)
                    .FirstOrDefault();
            }
        }

        public Customer? SearchByEmail(string Email)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(p => p.Email == Email)
                    .FirstOrDefault();
            }
        }

        public int Update(int Id, string Name, string Address, string Email, string Phone, string Password, bool IsAdmin)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == Id)
                    .Set(c => c.Name, Name)
                    .Set(c => c.Address, Address)
                    .Set(c => c.Email, Email)
                    .Set(c => c.Phone, Phone)
                    .Set(c => c.Password, Password)
                    .Set(c => c.IsAdmin, IsAdmin)
                    .Update();
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
