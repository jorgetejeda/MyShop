using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Core.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,100)]
        [DataType(DataType.Currency)]
        public decimal Range { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}
