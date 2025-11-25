using BookStoreProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject
{
    public class DbService
    {
        private readonly BookStoreContext _context;

        public DbService(BookStoreContext context)
        {
            _context = context;
        }

        // --- READ METODER ---

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Store>> GetAllStores()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

     
        public async Task<List<Inventory>> GetStoreInventories(int storeId)
        {
            return await _context.Inventorys
                .Where(i => i.StoreId == storeId)
                .Include(i => i.Book)
                .ToListAsync();
        }

       
        public async Task AddToBookStore(int storeId, int bookID, int amount)
        {
            var item = new Inventory
            {
                StoreId = storeId,
                BookId = bookID,
                Amount = amount,
            };

            _context.Inventorys.Add(item);

            await _context.SaveChangesAsync();
        }

      
        public async Task<bool> RemoveFromInventory(int storeId, int bookId)
        {
  
            var item = await _context.Inventorys
                .FirstOrDefaultAsync(i => i.StoreId == storeId && i.BookId == bookId);

            if (item == null)
            {
                return false;
            }

            _context.Inventorys.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}