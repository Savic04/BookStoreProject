using System;
using System.Threading.Tasks;
using BookStoreProject.Models;

namespace BookStoreProject
{
    public class Program
    {
        static async Task Main(string[] args)
        { 
            BookStoreContext context = new BookStoreContext();
            DbService db = new DbService(context);

            MainMenu menu = new MainMenu(db);

            bool running = true;
            while (running)
            {

                running = await menu.LobbyMenuAsync();
            }
            Console.WriteLine("Application closed. Goodbye!");
        }
    }
}
