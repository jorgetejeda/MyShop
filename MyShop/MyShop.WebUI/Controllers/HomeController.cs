using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IRepository<Product> product;
        IRepository<ProductCategory> productCategory;

        public HomeController()
        {
            product = new InMemoryRepository<Product>();
            productCategory = new InMemoryRepository<ProductCategory>();
        }

        public ActionResult Index(string Category = null)
        {
            //Declaramos nuestra variable de productos ya que en ella guardaremos los productos que correspondan a esa categoria
            List<Product> listProduct;
            //Traemos todas las categorias que existen
            List<ProductCategory> Categories = productCategory.Collection().ToList();
            if (Category == null)
            {
                //Si la categoria esta vacia mandamos la lista completa
                listProduct = product.Collection().ToList();
            }
            else
            {
                //Si la lista contiene algo entonces buscamos los productos que tengan esa categoria
                listProduct = product.Collection().Where(c => c.Category == Category).ToList();
            }
            //Instanciamos nuestra clase el cual tiene nuestra lista de productos y categorias
            ProductListViewModel model = new ProductListViewModel();
            model.Product = listProduct;
            model.ProductCagetory = Categories;
            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product productDetails = product.Find(Id);
            if (productDetails == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productDetails);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}