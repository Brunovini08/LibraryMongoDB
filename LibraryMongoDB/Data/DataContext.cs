using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database;
        private MongoClient _client;
        public DataContext(string uri)
        {
             _client = new MongoClient(uri);
            _database = _client.GetDatabase("LibraryDB");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

    }
}
