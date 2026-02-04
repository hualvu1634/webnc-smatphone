using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartphoneWeb.Models;

[Index("CustomerId", Name = "UQ__Carts__CD65CB84F6BA1264", IsUnique = true)]
public partial class Cart
{
    [Key]
    [Column("cart_id")]
    public int CartId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Cart")]
    public virtual Customer? Customer { get; set; }
}
