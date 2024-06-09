using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace crud.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Qty { get; set; }

        public string Remarks { get; set; }

        [Required]
        [DisplayName("Category")]
        public string Category { get; set; } 

        [Required]
        [DisplayName("Product Type")]
        public string ProductType { get; set; }

        [DisplayName("Manufacture Date")]
        [DataType(DataType.Date)]
        public DateTime? ManufactureDate { get; set; }

        [DisplayName("In Stock")]
        public bool InStock { get; set; } 
    }
}
