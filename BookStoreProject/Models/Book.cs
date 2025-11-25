using System;
using System.Collections.Generic;

namespace BookStoreProject.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Language { get; set; }

    public int? Price { get; set; }

    public int? AuthorId { get; set; }

    public string? ReleaseDate { get; set; }

    public string? Genre { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
