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

        public async Task InsertBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INSERINDO LIVRO NO SISTEMA");
            Console.ResetColor();
            Console.WriteLine();
            string title = InputHelper.ReadString("Digite o título do livro que deseja cadastrar: ", "Digite o título do livro!");
            Console.WriteLine();
            int year = InputHelper.ReadInt("Digite o ano de lançamento do livro que deseja cadastrar: ", "Digite o ano de lançamento do livro!");

            var authors = await _authorsService.GetAllAuthors();
            if (authors.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Você não pode adicionar nenhum livro - Nenhum Autor Cadastrado");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Por favor, Adicione um autor para adicionar um livro!");
                Console.ResetColor();
            } else
            {
                Author author;
                do
                {
                    Console.Clear();
                    await _authorsService.ShowAuthors();
                    int index = InputHelper.ReadInt("Digite o número de identificação do Autor para vincular ao Livro: ", "Digite o número de identificação do autor!");
                    author = await _authorsService.GetAuthorByIndex(index);
                } while (author is null);


                Book book = new Book(title, author.Index, year, await _booksService.GetLastBookIndex() + 1);

                try
                {
                    await _booksService.CreateBook(book);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Livro cadastrado com sucesso!");
                    Console.ResetColor();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


        }

        public async Task ListBooks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTA DOS LIVROS");
            Console.ResetColor();
            Console.WriteLine();
            var books = await _booksService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("Não existe livros cadastrados no sistema");
            } else
            {
                foreach (var book in books)
                {
                    var author = await _authorsService.GetAuthorByIndex(book.AuthorId);
                    Console.WriteLine($"Título: {book.Title}\nAno de Publicação: {book.Year}\nAutor: {author.Name}\n");
                }
            }
        }

        public async Task ListBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTANDO UM LIVRO POR NÚMERO DE IDENTIFIÇÃO: ");
            Console.ResetColor();
            int index = InputHelper.ReadInt("Digite o número de identificação do Livro: ", "Digite o ID Numérico");

            var book = await _booksService.GetBookByIndex(index);
            if (book == null)
            {
                Console.WriteLine("O Livro não existe!");
            } else
            {
                Console.WriteLine(book.ToString());
            }
        }

        public async Task UpdateBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("MODIFICANDO LIVRO DO SISTEMA");
            Console.ResetColor();
            Console.WriteLine("Caso não queira não queira alterar nenhuma informação, aperte ENTER");
            Console.WriteLine();
            var books = await _booksService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("Não existe livros cadastrados no sistema");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Index} - {book.Title}");
                }

                int index = InputHelper.ReadInt("Digite a identificação do livro para ser modificado: ", "Digite o identificador!");
                var bookSearch = await _booksService.GetBookByIndex(index);
                if (bookSearch != null) {
                    string title = InputHelper.ReadString("Digite o título do livro que deseja alterar: ", "Digite o título do livro!", bookSearch.Title);
                    Console.WriteLine();
                    int year = InputHelper.ReadInt("Digite o ano de lançamento do livro que deseja alterar: ", "Digite o ano de lançamento do livro!", bookSearch.Year);

                    var authors = await _authorsService.GetAllAuthors();
                    if (authors.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Você não pode modificar nenhum livro - Nenhum Autor Cadastrado");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Por favor, Adicione um autor para adicionar um livro!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Author author;
                        do
                        {
                            Console.Clear();
                            await _authorsService.ShowAuthors();
                            int indexAuthor = InputHelper.ReadInt("Digite o número de identificação do Autor para vincular ao Livro que deseja alterar: ", "Digite o número de identificação do autor!", bookSearch.AuthorId);
                            author = await _authorsService.GetAuthorByIndex(indexAuthor);
                        } while (author is null);

                        Book book = new Book(bookSearch.Id, author.Index, title, year, bookSearch.Index);

                        try
                        {
                            await _booksService.UpdateBook(book.Id, book);
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Livro alterado com sucesso!");
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("O Livro não existe!");
                }
            }

        }

        public async Task DeleteBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("DELETANDO LIVRO DO SISTEMA");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("LISTA DOS LIVROS");
            Console.WriteLine();
            var books = await _booksService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("Não existe livros cadastrados no sistema");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Index} - {book.Title}");
                }

                int indexBook = InputHelper.ReadInt("Digite a identificação do livro para ser deletado:", "Digite o identificador!");
                var bookSearch = await _booksService.GetBookByIndex(indexBook);
                if(bookSearch != null)
                {
                    try
                    {
                        await _booksService.DeleteBook(bookSearch.Id);
                        Console.WriteLine("Livro deletado com sucesso!");
                    } 
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } else
                {
                    Console.WriteLine("O Index digitado não existe! Tente Novamente");
                }
            }
        }
    }
}
