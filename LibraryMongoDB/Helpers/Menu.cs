using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Helpers
{
    public class Menu
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Controle de Livros");
            Console.WriteLine("2. Controle de Autores");
            Console.WriteLine("0. Sair");
        }

        public static void ShowBooksMenu()
        {
            Console.Clear();
            Console.WriteLine("Controle de Livros");
            Console.WriteLine("1. Listar todos os livros");
            Console.WriteLine("2. Adicionar um novo livro");
            Console.WriteLine("3. Atualizar um livro existente");
            Console.WriteLine("4. Deletar um livro");
            Console.WriteLine("0. Voltar ao menu principal");
        }

        public static void ShowAuthorsMenu()
        {
            Console.Clear();
            Console.WriteLine("Controle de Autores");
            Console.WriteLine("1. Listar todos os autores");
            Console.WriteLine("2. Adicionar um novo autor");
            Console.WriteLine("3. Atualizar um autor existente");
            Console.WriteLine("4. Deletar um autor");
            Console.WriteLine("0. Voltar ao menu principal");
        }
    }
}
