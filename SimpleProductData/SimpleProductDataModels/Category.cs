using System.ComponentModel.DataAnnotations;

namespace SimpleProductDataModels;

public class Category
{
    public int Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(250)]
    public string Description { get; set; }

    public virtual List<Product> Products { get; set; } = new List<Product>();
}
