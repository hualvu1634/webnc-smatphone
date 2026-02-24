using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartphoneWeb.Models;

[Index("ProductName", Name = "UQ__Products__2B5A6A5FAFF73DB0", IsUnique = true)]
public partial class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("product_name")]
    [StringLength(50)]
    public string ProductName { get; set; } = null!;

    [Column("descriptions")]
    [StringLength(255)]
    public string? Descriptions { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("image_url", TypeName = "nvarchar(max)")] 
    public string? ImageUrl { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    [Column("product_date", TypeName = "datetime")]
    public DateTime? ProductDate { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
