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

        public AuthorsService(IMongoCollection<Author> authorsCollection)
        {
            _authors = authorsCollection;
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
            await _authors.DeleteOneAsync(author => author.Id == id);
        }

        public async void ShowAuthors()
        {
            List<Author> authors = await GetAllAuthors();
            if (authors.Count > 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Autores Cadastrados no Sistema ({authors.Count})");
                Console.ResetColor();
                Console.ReadKey();
                foreach (Author author in authors)
                {
                    Console.WriteLine($"{author.Index} - {author.Name}");
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
