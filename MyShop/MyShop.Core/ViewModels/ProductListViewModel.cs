using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class ProductListViewModel
    {
        //Una lista de los productos los cuales no vamos alterar
        //La cual le enviaremos  a la vista
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<ProductCategory> ProductCagetory { get; set; }
    }
}
