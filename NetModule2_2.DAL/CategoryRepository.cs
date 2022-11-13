using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly string connectionString = "server=localhost;port=3306;database=catalog;uid=root;password=123456";

        public Category Get(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            return connection.Query<Category>("SELECT * FROM Category WHERE Id = {=id}", new { id }).Single();
        }

        public List<Category> List()
        {
            using var connection = new MySqlConnection(connectionString);
            return connection.Query<Category>("SELECT * FROM Category").ToList();
        }

        public void Add(Category category)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("INSERT INTO Category (Name, Image, ParentCategoryId) VALUES (@Name, @Image, @ParentCategoryId)", category);
        }

        public void Update(Category category)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("UPDATE Category SET Name = @Name, ParentCategoryId=@ParentCategoryId, Image=@Image WHERE Id = @Id", category);
        }

        public void Delete(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("DELETE FROM Category WHERE Name=@name", new { id });
        }
    }
}
