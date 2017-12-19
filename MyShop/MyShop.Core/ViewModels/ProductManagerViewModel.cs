using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Product Product { get; set; }
        //Sera un Ienumerable porque sera una dato fijo el cual vamos a poner en un dropdown list
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
