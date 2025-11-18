using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace LibraryMongoDB.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public int Index { get; private set; }
        public string Title { get; private set; }
        public int AuthorId { get; private set; }
        public int Year { get; set; }
        public Book(string id, int authorIndex, string title, int year, int index)
        {
            Id = id;
            Title = title;
            AuthorId = authorIndex;
            Year = year;
            Index = index;
        }

        public Book(string title, int authorIndex, int year, int index)
        {
            Title = title;
            AuthorId = authorIndex;
            Year = year;
            Index = index;
        }

        public override string ToString()
        {
            return $"Identificação: {Index}\nTítulo: {this.Title}\nAno de Publicação: {this.Year}\n";
        }
    }
}
