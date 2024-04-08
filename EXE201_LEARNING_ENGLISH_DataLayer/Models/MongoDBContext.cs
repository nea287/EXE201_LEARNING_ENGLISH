using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }


        // Thêm các thuộc tính DbSet cho các bảng (collections) trong cơ sở dữ liệu
        public IMongoCollection<User> Users => _database.GetCollection<User>("User");
        public IMongoCollection<Cart> Carts => _database.GetCollection<Cart>("Cart");
    }
}
