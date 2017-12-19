using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        
        InMemoryRepository<ProductCategory> contextCategory;

        public ProductCategoryManagerController()
        {
            contextCategory = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> category = contextCategory.Collection().ToList();
            return View(category);
        }

        public ActionResult Create()
        {
            ProductCategory category = new ProductCategory();
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }
            else
            {
                contextCategory.Insert(c);
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {
            ProductCategory categoryEdit = contextCategory.Find(Id);
            if (categoryEdit != null)
            {
                return View(categoryEdit);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }
            else
            {
                ProductCategory categoryEdit = contextCategory.Find(c.Id);
                if (categoryEdit != null)
                {
                    categoryEdit.Category = c.Category;
                    contextCategory.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory categoryToDelete = contextCategory.Find(Id);
            if (categoryToDelete != null)
            {
                return View(categoryToDelete);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCategory(string Id)
        {
            ProductCategory categoryToDelete = contextCategory.Find(Id);
            if (categoryToDelete != null)
            {
                contextCategory.Delete(categoryToDelete.Id);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}