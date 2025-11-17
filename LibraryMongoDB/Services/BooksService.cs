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

        public async Task<Book> GetBookByIndex(int index)
        {
            return await _booksCollection.Find(book => book.Index ==  index).FirstOrDefaultAsync();
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

        public async Task UpdateBook(string id, Book updatedBook)
        {
            await _booksCollection.ReplaceOneAsync(book => book.Id == id, updatedBook);
        }

        public async Task DeleteBook(string id)
        {
            await _booksCollection.DeleteOneAsync(book => book.Id == id);
        }
    }
}
