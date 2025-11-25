using System;
using System.Collections.Generic;

namespace BookStoreProject.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? StoreId { get; set; }

    public int? BookId { get; set; }

    public int? Amount { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Store? Store { get; set; }
}
