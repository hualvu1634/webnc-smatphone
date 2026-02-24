using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartphoneWeb.Models;

[Index("UserId", Name = "UQ__Carts__CD65CB84F6BA1264", IsUnique = true)]
public partial class Cart
{
    [Key]
    [Column("cart_id")]
    public int CartId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("UserId")]
    [InverseProperty("Cart")]
    public virtual User? User { get; set; }
}