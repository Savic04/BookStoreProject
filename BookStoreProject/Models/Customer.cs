using System;
using System.Collections.Generic;

namespace BookStoreProject.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Adress { get; set; }

    public DateTime? Date { get; set; }

    public int? OrderNumber { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
