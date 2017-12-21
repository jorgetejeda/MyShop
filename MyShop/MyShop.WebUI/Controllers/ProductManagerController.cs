using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();

            InMemoryRepository<Product>.lkl();
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

        //agregamos la propiedad que nos permite recibir imagenes "HttpPostedFileBase" mas el name del campo en el formulario
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    //Concatenamos el nombre de la imagen junto al Id
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    //GUardamos la imagen en nuestro archivo Content/ProductImages
                    file.SaveAs(Server.MapPath("//Content//ProductImage//" + product.Image));
                }

                context.Insert(product);
                return RedirectToAction("Index");
            }


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
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    //Concatenamos el nombre de la imagen junto al Id
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    //GUardamos la imagen en nuestro archivo Content/ProductImages
                    file.SaveAs(Server.MapPath("//Content//ProductImage//" + productToEdit.Image));
                }

                productToEdit.Name = product.Name;
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
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