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
            return connection.Query<Item>("SELECT * FROM Item WHERE Id = {=id}", new { id }).FirstOrDefault();
        }

        public List<Item> List()
        {
            using var connection = new MySqlConnection(connectionString);
            return connection.Query<Item>("SELECT * FROM Item").ToList();
        }

        public void Add(Item item)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("INSERT INTO Item (Name, Description, Image, CategoryName, Price, Amount) VALUES (@Name, @Description, @Image, @CategoryName, @Price, @Amount)", item);
        }

        public void Update(Item item)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("UPDATE Item SET Name=@Name, Description=@Description, Image=@Image, CategoryName=@CategoryName, Price=@Price, Amount=@Amount WHERE Id = @Id", item);
        }

        public void Delete(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Execute("DELETE FROM Item WHERE Name=@name", new { id });
        }

    }
}
