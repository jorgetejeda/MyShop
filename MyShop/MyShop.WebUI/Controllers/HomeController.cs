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

        public ActionResult Index()
        {
            List<Product> listProduct = product.Collection().ToList();
            return View(listProduct);
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