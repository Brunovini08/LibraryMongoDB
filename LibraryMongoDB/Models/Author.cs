using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace LibraryMongoDB.Models
{
    public class Author
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public int Index {  get; private set; }
        public Author(string name, string country)
        {
            Name = name;
            Country = country;
        }

        public Author(string id, string name, string country, int index)
        {
            Id = id;
            Name = name;
            Country = country;
            Index = index;
        }
    }
}
