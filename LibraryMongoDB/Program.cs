using LibraryMongoDB.Data;
using LibraryMongoDB.Helpers;
using LibraryMongoDB.Models;
using LibraryMongoDB.Presentation;
using LibraryMongoDB.Services;

async Task RunAsync()
{

    var context = new DataContext("mongodb+srv://brunocapitadev_db_user:TPTQS628vuxuugfR@interacaomongo.cua1ndw.mongodb.net/");

    var booksCollection = context.GetCollection<Book>("Books");
    var authorsCollection = context.GetCollection<Author>("Authors");

    var booksService = new BooksService(booksCollection);
    var authorsService = new AuthorsService(authorsCollection, booksCollection);

    var bookUI = new BookUI(booksService, authorsService);
    var authorUI = new AuthorUI(booksService, authorsService);

    bool finish = false;
    do
    {
        Menu.ShowMainMenu();
        int option = InputHelper.ReadInt("Digite a opção do menu que deseja: ", "Digite um valor númerico!");
        switch (option)
        {
            case 1:
                {
                    bool finishBook = false;
                    do
                    {
                        Menu.ShowBooksMenu();
                        int optionBook = InputHelper.ReadInt("Digite a opção do menu que deseja: ", "Digite um valor númerico!");

                        switch (optionBook)
                        {
                            case 1:
                                await bookUI.ListBooks();
                                Console.ReadKey();
                                break;
                            case 2:
                                await bookUI.InsertBook();
                                Console.ReadKey();
                                break;
                            case 3:
                                await bookUI.UpdateBook();
                                Console.ReadKey();
                                break;
                            case 4:
                                await bookUI.DeleteBook();
                                Console.ReadKey();
                                break;
                            case 0:
                                finishBook = true;
                                break;
                            default:
                                Console.WriteLine("Opção inválida, digite novamente");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                        }
                    } while (!finishBook);
                }
                break;
            case 2:
                {
                    bool finishAuthor = false;
                    do
                    {
                        Menu.ShowAuthorsMenu();
                        int optionAuthor = InputHelper.ReadInt("Digite a opção do menu que deseja: ", "Digite um valor númerico!");

                        switch (optionAuthor)
                        {
                            case 1:
                                await authorUI.ListAuthors();
                                Console.ReadKey();
                                break;
                            case 2:
                                await authorUI.InsertAuthor();
                                Console.ReadKey();
                                break;
                            case 3:
                                await authorUI.UpdateAuthor();
                                Console.ReadKey();
                                break;
                            case 4:
                                await authorUI.DeleteAuthor();
                                Console.ReadKey();
                                break;
                            case 0:
                                finishAuthor = true;
                                break;
                            default:
                                Console.WriteLine("Opção inválida, digite novamente");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                        }
                    } while (!finishAuthor);
                }
                break;
            case 0:
                finish = true;
                break;
            default:
                Console.WriteLine("Opção inválida, digite novamente");
                Console.ReadKey();
                Console.Clear();
                break;
        }
    } while (!finish);
}

await RunAsync();