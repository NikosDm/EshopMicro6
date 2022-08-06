using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Web.Models;

public class ProductDTO
{
    public ProductDTO() 
    {
        Count = 1;
    }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }

    [Range(1, 100)]
    public int Count { get; set; }
}
