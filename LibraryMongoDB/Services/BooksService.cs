using LibraryMongoDB.Data;
using LibraryMongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BooksService(IMongoCollection<Book> booksCollection)
        {
            _booksCollection = booksCollection;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _booksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _booksCollection.Find(book => book.Index == id).FirstOrDefaultAsync();
        }

        public int GetLastBookIndex()
        {
            return _booksCollection
                .Find(FilterDefinition<Book>.Empty)
                .SortByDescending(book => book.Index)
                .First().Index;
        }

        public async Task CreateBook(Book newBook)
        {
            try
            {
                await _booksCollection.InsertOneAsync(newBook);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateBook(int id, Book updatedBook)
        {
            await _booksCollection.ReplaceOneAsync(book => book.Index == id, updatedBook);
        }

        public async Task DeleteBook(int id)
        {
            await _booksCollection.DeleteOneAsync(book => book.Index == id);
        }
    }
}
