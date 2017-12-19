using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategories;


        public ProductManagerController()
        {
            context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> product = context.Collection().ToList();
            return View(product);
        }

        public ActionResult Create()
        {
            //Cargeremos nuestra nueva identidad
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product(); //Cargamos nuestra entidad de producto a nuesta nueva identidad
            viewModel.ProductCategories = productCategories.Collection(); //Buscamos todas las categorias que existen
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            context.Insert(product);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit != null)
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = productToEdit;
                viewModel.ProductCategories = productCategories.Collection().ToList();
                return View(viewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            Product productToEdit = context.Find(product.Id);
            if (productToEdit != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Name = product.Name;
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Range = product.Range;
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete != null)
            {
                return View(productToDelete);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteProduct(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete != null)
            {
                context.Delete(productToDelete.Id);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}