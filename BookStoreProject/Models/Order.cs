using System;
using System.Collections.Generic;

namespace BookStoreProject.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? OrderNumber { get; set; }

    public DateTime? Date { get; set; }

    public int? StoreId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Store? Store { get; set; }
}
