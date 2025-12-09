using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreProject.Models;

namespace BookStoreProject
{
    public class MainMenu
    {
        private readonly DbService _dbService;

        public MainMenu(DbService dbService)
        {
            _dbService = dbService;
        }

        // MainMenu loop
        public async Task<bool> LobbyMenuAsync()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("=        BOOKSTORE ADMIN TOOLS       =");
            Console.WriteLine("==================================================");
            Console.WriteLine("[1]. List all books in the Avalible");
            Console.WriteLine("[2]. Show inventory for a specific store");
            Console.WriteLine("[3]. Add book to store inventory");
            Console.WriteLine("[4]. Remove book from store inventory");
            Console.WriteLine("[5]. List all Stores");
            Console.WriteLine("[6]. Exit application");
            Console.WriteLine("==================================================");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    await ListAllBooks();
                    break;
                case "2":
                    await ShowInentory();
                    break;
                case "3":
                   
                    await AddBookToStoreInventoryFlow();
                    break;
                case "4":
                    
                    await RemoveBookFromStoreInventoryFlow();
                    break;
                case "5":
                    await ListAllStores();
                    break;
                case "6":
                    Console.WriteLine("Exiting application");
                    return false;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return true;
        }

        async Task ListAllBooks()
        {
            Console.WriteLine("--- BOOK CATALOG ---");
            var books = await _dbService.GetAllBooks();
            var authors = await _dbService.GetAllAuthors();
            Console.WriteLine("            ID | Title | Author           ");
            Console.WriteLine("------------------------------------------");
            foreach (var book in books)
            {
                var chosenAuthor = authors.FirstOrDefault(a => a.AuthorId == book.AuthorId);
                string authorName;
                if (chosenAuthor != null)
                {
                    string firstName = chosenAuthor.FirstName;
                    string lastName = chosenAuthor.LastName;

                    authorName = firstName + " " + lastName;
                    Console.WriteLine($"{book.AuthorId} | {book.Title} | {authorName} | Amount of copies: ");
                }
            }
            Console.ReadKey();
        }
        async Task ListAllStores()
        {
            Console.WriteLine("--- ALL BOOKSTORES ---");
            var stores = await _dbService.GetAllStores();

                foreach (var store in stores)
                {
                    Console.WriteLine($"[ID: {store.StoreId}] Name: {store.StoreName}, City: {store.City}");
                }
           
        }

        public async Task ShowInentory() 
        {
            Console.WriteLine("--- VIEW STORE INVENTORY ---");

            var storeId = await SelectStoreId();
            if (storeId == 0) return;

            var inventory = await _dbService.GetStoreInventories(storeId);
            var stores = await _dbService.GetAllStores();
            var selectedStore = stores.FirstOrDefault(s => s.StoreId == storeId);
            string storeName = selectedStore?.StoreName ?? $"Store ID {storeId}";
          
            Console.WriteLine($"\nINVENTORY FOR {storeName}");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("-Book ID | Title | Amount in Stock-");
            Console.WriteLine("-----------------------------------");

             foreach (var item in inventory)
             {
                 string title = item.Book?.Title ?? "Title Missing";
                 Console.WriteLine($"{item.BookId} | {title} | {item.Amount}");
             }
   
        }

  

        public async Task AddBookToStoreInventoryFlow()
        {
            Console.WriteLine("--- ADD BOOK TO STORE INVENTORY ---");

            var storeId = await SelectStoreId();
            if (storeId == 0) return;


            var bookId = await SelectBookId();
            if (bookId == 0) return;

            Console.Write("Enter Amount to add: ");
            if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            try
            {
                await _dbService.AddToBookStore(storeId, bookId, amount);
                Console.WriteLine($"\n Successfully added {amount} copies of Book ID {bookId} to Store ID {storeId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n An error occurred while adding to inventory: {ex.Message}");
            }
        }


        public async Task RemoveBookFromStoreInventoryFlow()
        {
            Console.WriteLine("--- REMOVE BOOK FROM STORE INVENTORY ---");

            var storeId = await SelectStoreId();
            if (storeId == 0) return;

            Console.Write("Enter Book ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid Book ID.");
                return;
            }

            try
            {
                bool success = await _dbService.RemoveFromInventory(storeId, bookId);

                if (success)
                {
                    Console.WriteLine($"\n Successfully deleted item Book ID {bookId} from Store ID {storeId}.");
                }
                else
                {
                    Console.WriteLine($"\n Item Book ID {bookId} not found in Store ID {storeId} inventory.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n An error occurred while removing from inventory: {ex.Message}");
            }
        }


        private async Task<int> SelectStoreId()
        {
            await ListAllStores();
            Console.Write("\nEnter Store ID: ");
            if (!int.TryParse(Console.ReadLine(), out int storeId))
            {
                Console.WriteLine("Invalid Store ID.");
                return 0;
            }
            return storeId;
        }

        private async Task<int> SelectBookId()
        {
            await ListAllBooks();
            Console.Write("\nEnter Book ID to select: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid Book ID.");
                return 0;
            }
            return bookId;
        }
    }
}