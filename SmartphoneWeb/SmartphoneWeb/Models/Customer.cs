using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartphoneWeb.Models;

[Index("PhoneNumber", Name = "UQ__Customer__A1936A6B60D0DA5E", IsUnique = true)]
[Index("Email", Name = "UQ__Customer__AB6E6164E12D3BDA", IsUnique = true)]
public partial class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("first_name")]
    [StringLength(255)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(255)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("customers_password")]
    [StringLength(50)]
    [Unicode(false)]
    public string CustomersPassword { get; set; } = null!;

    [Column("customers_address")]
    [StringLength(255)]
    public string? CustomersAddress { get; set; }

    [Column("phone_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("customer_date", TypeName = "datetime")]
    public DateTime? CustomerDate { get; set; }

    [InverseProperty("Customer")]
    public virtual Cart? Cart { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
