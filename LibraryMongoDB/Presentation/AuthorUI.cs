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

        public async Task InsertAuthor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INSERINDO AUTOR NO SISTEMA");
            Console.ResetColor();
            Console.WriteLine();
            string name = InputHelper.ReadString("Digite o nome do autor que deseja cadastrar: ", "Digite o nome do autor!");
            Console.WriteLine();
            string country = InputHelper.ReadString("Digite o país do autor que deseja cadastrar: ", "Digite o país do autor!");
            int lastIndex = await _authorsService.GetLastAuthorIndex(); 
            var author = new Author(name, country, lastIndex + 1);
                try
                {
                    await _authorsService.CreateAuthor(author);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Autor cadastrado com sucesso!");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public async Task ListAuthors()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTA DOS AUTORES");
            Console.ResetColor();
            Console.WriteLine();
            var authors = await _authorsService.GetAllAuthors();
            if (authors.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Não existe autores cadastrados no sistema");
                Console.ResetColor();
            }
            else
            {
                foreach (var author in authors)
                {
                    Console.WriteLine(author.ToString());
                }
            }
        }

        public async Task ListAuthor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LISTANDO UM AUTOR POR NÚMERO DE IDENTIFIÇÃO: ");
            Console.ResetColor();
            int index = InputHelper.ReadInt("Digite o número de identificação do Livro: ", "Digite o ID Numérico");
            var author = await _authorsService.GetAuthorByIndex(index);
            if(author == null)
            {
                Console.WriteLine("Autor não existe!");

            } else
            {
                Console.WriteLine(author.ToString());
            }
        }

        public async Task UpdateAuthor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("MODIFICANDO AUTOR");
            Console.ResetColor();
            Console.WriteLine("Caso não queira não queira alterar nenhuma informação, aperte ENTER");
            Console.WriteLine();
            var authors = await _authorsService.GetAllAuthors();
            if (authors.Count == 0)
            {
                Console.WriteLine("Não existe autores cadastrados no sistema");
            }
            else
            {
                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.Index} - {author.Name}");
                }

                int index = InputHelper.ReadInt("Digite a identificação do autor para ser modificado: ", "Digite o identificador!");
                var authorSearch = await _authorsService.GetAuthorByIndex(index);
                if (authorSearch != null)
                {
                    string name = InputHelper.ReadString("Digite o nome do autor que deseja alterar: ", "Digite o nome do autor!", authorSearch.Name);
                    Console.WriteLine();
                    string country = InputHelper.ReadString("Digite o país do autor que deseja alterar: ", "Digite o país do autor!", authorSearch.Country);
                    Author author = new Author(authorSearch.Id, name, country, authorSearch.Index);

                    try
                    {
                        await _authorsService.UpdateAuthor(authorSearch.Id, author);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Autor alterado com sucesso!");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Autor não existe, tente novamente!");
                    Console.ResetColor();

                }
            }
        }

        public async Task DeleteAuthor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("DELETANDO AUTOR DO SISTEMA");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("LISTA DOS AUTORES");
            Console.WriteLine();
            var authors = await _authorsService.GetAllAuthors();
            if (authors.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Não existe autores cadastrados no sistema");
                Console.ResetColor();
            }
            else
            {
                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.Index} - {author.Name}");
                }
                int indexBook = InputHelper.ReadInt("Digite a identificação do autor para ser deletado: ", "Digite o identificador!");
                var authorSearch = await _authorsService.GetAuthorByIndex(indexBook);
                if (authorSearch != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Ao deletar o autor, todos os livros que tiverem vínculo com o mesmo serão deletados");
                    Console.ResetColor();
                    string confirm = InputHelper.ReadString("Deseja realmente deletar o autor (SIM - NAO): ", "Digite o valor Correto, por favor (SIM - NAO)", "nao");
                    if(confirm.ToLower() == "sim")
                    {
                        try
                        {
                            await _authorsService.DeleteAuthor(authorSearch.Id);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Autor deletado com sucesso!");
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    } else if(confirm.ToLower() == "nao") { }
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O Index digitado não existe! Tente Novamente");
                    Console.ResetColor();
                }
            }
        }
    }
}
