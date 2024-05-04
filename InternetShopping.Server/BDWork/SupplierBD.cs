using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using ShopLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestShop
{
    public class SupplierBD
    {
        private const string CONNECTION_STRING = @"Server=Tpshka;DataBase=Shop;Trusted_Connection=True;";

        public int Create(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                if (SearchByTitle(Title) == null)
                    return db.GetTable<Supplier>()
                        .Value(p => p.Title, Title)
                        .Insert();
                else
                    return -1;
            }
        }

        public List<Supplier> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>().ToList();
            }
        }

        public Supplier? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Supplier? SearchByTitle(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(p => p.Title == Title)
                    .FirstOrDefault();
            }
        }

        public int UpdateTitle(int Id, string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.Id == Id)
                    .Set(c => c.Title, Title)
                    .Update();
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
