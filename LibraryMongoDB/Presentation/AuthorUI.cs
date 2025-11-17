using LibraryMongoDB.Helpers;
using LibraryMongoDB.Models;
using LibraryMongoDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Presentation
{
    public class AuthorUI
    {
        public readonly BooksService _booksService;
        public readonly AuthorsService _authorsService;

        public AuthorUI(BooksService bookService, AuthorsService authorsService)
        {
            _booksService = bookService;
            _authorsService = authorsService;
        }

        public async void InsertAuthor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INSERINDO AUTOR NO SISTEMA");
            Console.ResetColor();
            Console.WriteLine();
            string name = InputHelper.ReadString("Digite o nome do autor que deseja cadastrar: ", "Digite o nome do autor!");
            Console.WriteLine();
            string country = InputHelper.ReadString("Digite o país do autor que deseja cadastrar: ", "Digite o país do autor!");

            var author = new Author(name, country);
                try
                {
                    await _authorsService.CreateAuthor(author);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Autor cadastrado com sucesso!");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public async void ListAuthors()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTA DOS AUTORES");
            Console.ResetColor();
            Console.WriteLine();
            var authors = await _authorsService.GetAllAuthors();
            if (authors.Count == 0)
            {
                Console.WriteLine("Não existe autores cadastrados no sistema");
                Console.ReadKey();
            }
            else
            {
                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.Index} - {author.Name}");
                }
                Console.ReadKey();
            }
        }

        public async void ListAuthor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTANDO UM AUTOR POR NÚMERO DE IDENTIFIÇÃO: ");
            Console.ResetColor();
            int index = InputHelper.ReadInt("Digite o número de identificação do Livro: ", "Digite o ID Numérico");
            var author = _authorsService.GetAuthorByIndex(index);
            if(author == null)
            {
                Console.WriteLine("Autor não existe!");
                Console.ReadKey();
            } else
            {
                Console.WriteLine(author.ToString());
                Console.ReadKey();
            }
        }

        public async void UpdateAuthor()
        {

        }

        public async void DeleteAuthor()
        {

        }
    }
}
