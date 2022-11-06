using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    internal class ItemRepository : IItemRepository
    {
        private readonly string connectionString = "Data Source=catalog.db";

        public Item Get(string name)
        {
            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Item>("SELECT * FROM Item WHERE Name = {=name}", new { name }).FirstOrDefault();
        }

        public List<Item> List()
        {
            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Item>("SELECT * FROM Item").ToList();
        }

        public void Add(Item item)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Execute("INSERT INTO Item (Name, Description, Image, CategoryName, Price, Amount) VALUES (@Name, @Description, @Image, @CategoryName, @Price, @Amount)", item);
        }

        public void Update(Item item)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Execute("UPDATE Item SET Description=@Description, Image=@Image, CategoryName=@CategoryName, Price=@Price, Amount=@Amount WHERE Name = @Name", item);
        }

        public void Delete(string name)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Execute("DELETE FROM Item WHERE Name=@name", new { name = name });
        }

    }
}
