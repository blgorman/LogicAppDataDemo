using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProductDataModels;

public class Product
{
    public int Id { get; set; }
    [StringLength(32)]
    public string SKU { get; set; }
    [StringLength(50)]
    public string Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}