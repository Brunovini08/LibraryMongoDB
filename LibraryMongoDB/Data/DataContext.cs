using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database;
        public DataContext(string uri)
        {
            var client = new MongoClient(uri);
            _database = client.GetDatabase("LibraryDB");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

    }
}
