using Dapper;
using MySql.Data.MySqlClient;

namespace NetModule2_2.DAL
{
    internal class ItemRepository : IItemRepository
    {
        private readonly string connectionString = "server=localhost;port=3306;database=catalog;uid=root;password=123456";

        public Item Get(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            return connection.Query<Item>("SELECT * FROM Item WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<Item> List(int? categoryId)
        {
            using var connection = new MySqlConnection(connectionString);
            return connection.Query<Item>("SELECT * FROM Item WHERE @categoryId IS NULL OR CategoryId = @categoryId ORDER BY Name", new { categoryId }).ToList();
        }

        public int Add(Item item)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("INSERT INTO Item (Name, Description, Image, CategoryId, Price, Amount) VALUES (@Name, @Description, @Image, @CategoryId, @Price, @Amount)", item);
            return connection.Query<int>("SELECT * FROM Item WHERE Name = @Name", item).Single();
        }

        public void Update(Item item)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("UPDATE Item SET Name=@Name, Description=@Description, Image=@Image, CategoryId=@CategoryId, Price=@Price, Amount=@Amount WHERE Id = @Id", item);
        }

        public void Delete(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("DELETE FROM Item WHERE Id=@id", new { id });
        }

        public void DeleteCategory(int categoryId)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("DELETE FROM Item WHERE CategoryId=@categoryId", new { categoryId });
        }
    }
}
