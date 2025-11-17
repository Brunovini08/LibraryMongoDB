using LibraryMongoDB.Data;
using LibraryMongoDB.Helpers;
using LibraryMongoDB.Models;
using LibraryMongoDB.Presentation;
using LibraryMongoDB.Services;

var context = new DataContext("mongodb+srv://brunocapitadev_db_user:TPTQS628vuxuugfR@interacaomongo.cua1ndw.mongodb.net/");

var booksCollection = context.GetCollection<Book>("Books");
var authorsCollection = context.GetCollection<Author>("Authors");

var booksService = new BooksService(booksCollection);
var authorsService = new AuthorsService(authorsCollection);

var bookUI = new BookUI(booksService, authorsService);


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
                            bookUI.ListBooks();
                            Console.ReadKey();
                            break;
                        case 2:
                            bookUI.InsertBook();
                            Console.ReadKey();
                            break;
                        case 3:
                            bookUI.UpdateBook();
                            Console.ReadKey();
                            break;
                        case 4:
                            bookUI.DeleteBook();
                            Console.ReadKey();
                            break;
                        case 0:
                            finishBook = true;
                            break;
                    }
                } while (!finishBook);
            }
            break;
        case 2:
            {
                Menu.ShowAuthorsMenu();
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