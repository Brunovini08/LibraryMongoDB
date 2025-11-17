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
    public class BookUI
    {
        public readonly BooksService _booksService;
        public readonly AuthorsService _authorsService;
        public BookUI(BooksService bookService, AuthorsService authorsService) 
        {
            _booksService = bookService;
            _authorsService = authorsService;
        }

        public async void InsertBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INSERINDO LIVRO NO SISTEMA");
            Console.ResetColor();
            Console.WriteLine();
            string title = InputHelper.ReadString("Digite o título do livro que deseja cadastrar: ", "Digite o título do livro!");
            Console.WriteLine();
            int year = InputHelper.ReadInt("Digite o ano de lançamento do livro que deseja cadastrar: ", "Digite o ano de lançamento do livro!");

            var authors =  await _authorsService.GetAllAuthors();
            if(authors.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Você não pode adicionar nenhum livro - Nenhum Autor Cadastrado");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Por favor, Adicione um autor para adicionar um livro!");
                Console.ResetColor();
                Console.ReadKey();
            } else
            {
                Author author;
                do
                {
                    Console.Clear();
                    _authorsService.ShowAuthors();
                    int index = InputHelper.ReadInt("Digite o número de identificação do Autor para vincular ao Livro: ", "Digite o número de identificação do autor!");
                    author = await _authorsService.GetAuthorByIndex(index);
                } while (author is null);


                Book book = new Book(title, author.Index, year, _booksService.GetLastBookIndex() + 1);

                try
                {
                    await _booksService.CreateBook(book);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Livro cadastrado com sucesso!");
                    Console.ResetColor();
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

          
        }

        public async void ListBooks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTA DOS LIVROS");
            Console.ResetColor();
            Console.WriteLine();
            var books = await _booksService.GetAllBooks();
            if(books.Count == 0)
            {
                Console.WriteLine("Não existe livros cadastrados no sistema");
                Console.ReadKey();
            } else
            {
                foreach(var book in books)
                {
                    Console.WriteLine(book.Title );
                }
                Console.ReadKey();
            }
        }

        public async void ListBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTANDO UM LIVRO POR NÚMERO DE IDENTIFIÇÃO: ");
            int index = InputHelper.ReadInt("Digite o número de identificação do Livro: ", "Digite o ID Numérico");
        }

        public async void UpdateBook()
        {

        }

        public async void DeleteBook()
        {

        }
    }
}
