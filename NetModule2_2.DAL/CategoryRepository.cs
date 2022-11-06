using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly string connectionString = "Data Source=catalog.db";

        public Category Get(string name)
        {
            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Category>("SELECT * FROM Category WHERE Name = {=name}", new { name = name }).Single();
        }

        public List<Category> List()
        {
            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Category>("SELECT * FROM Category").ToList();
        }

        public void Add(Category category)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Execute("INSERT INTO Category (Name, Image, ParentCategoryName) VALUES (@Name, @Image, @ParentCategoryName)", category);
        }

        public void Update(Category category)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Execute("UPDATE Category SET ParentCategoryname=@ParentCategoryName, Image=@Image WHERE Name = @Name", category);
        }

        public void Delete(string name)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Execute("DELETE FROM Category WHERE Name=@name", new { name = name });
        }
    }
}
