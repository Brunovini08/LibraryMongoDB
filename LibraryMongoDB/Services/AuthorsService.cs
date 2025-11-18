using LibraryMongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Services
{
    public class AuthorsService
    {
        private readonly IMongoCollection<Author> _authors;
        private readonly IMongoCollection<Book> _books;
        public AuthorsService(IMongoCollection<Author> authorsCollection, IMongoCollection<Book> booksCollection)
        {
            _authors = authorsCollection;
            _books = booksCollection;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authors.Find(_ => true).ToListAsync();
        }

        public async Task<Author> GetAuthorById(string id)
        {
            return await _authors.Find(author => author.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Author> GetAuthorByIndex(int index)
        {
            return await _authors.Find(author => author.Index == index).FirstOrDefaultAsync();
        }

        public async Task<int> GetLastAuthorIndex()
        {
            var author = await _authors
                .Find(FilterDefinition<Author>.Empty)
                .SortByDescending(author => author.Index)
                .FirstOrDefaultAsync();

            if (author == null) return 0;
            else if (author.Index == 0) return 0;
            else return author.Index;
        }

        public async Task CreateAuthor(Author newAuthor)
        {
            await _authors.InsertOneAsync(newAuthor);
        }

        public async Task UpdateAuthor(string id, Author updatedAuthor)
        {
            await _authors.ReplaceOneAsync(author => author.Id == id, updatedAuthor);
        }

        public async Task DeleteAuthor(string id)
        {
            var author = await this.GetAuthorById(id);
            if (author == null) return;
            else
            {
                try
                {
                    await _books.DeleteManyAsync(b => b.AuthorId == author.Index);
                    await _authors.DeleteOneAsync(at => at.Id == id);
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task ShowAuthors()
        {
            List<Author> authors = await GetAllAuthors();
            if (authors.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Autores Cadastrados no Sistema ({authors.Count})");
                Console.ResetColor();
                foreach (Author author in authors)
                {
                    Console.WriteLine(author.ToString());
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Não existe Autores");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
