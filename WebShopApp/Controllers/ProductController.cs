﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NuGet.Versioning;

using WebShopApp.Core.Contracts;
using WebShopApp.Core.Services;
using WebShopApp.Models.Brand;
using WebShopApp.Models.Category;
using WebShopApp.Models.Product;

namespace WebShopApp.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductService _productService;
        public readonly IBrandService _brandService;
        public readonly ICategoryService _categoryService;


        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            _brandService = brandService;
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: ProductController
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductIndexVM> products = _productService.GetProducts(searchStringBrandName, searchStringCategoryName)
                .Select(product => new ProductIndexVM
                {
                    Id = product.Id,
                    Name = product.ProductName,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.CategoryName,
                    Picture = product.Picture,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Discount = product.Discount
                }).ToList();
            return this.View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Brands = _brandService.GetBrands().Select(x => new BrandPairVM()
            {
                Id = x.Id,
                Name = x.BrandName
            }).ToList();

            product.Categories = _categoryService.GetCategories().Select(x => new CategoryPairVM()
            {
                Id = x.Id,
                Name = x.CategoryName
            }).ToList();
            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var createdId = _productService.Create(product.ProductName, product.BrandId, product.CategoryId, product.Picture, product.Quantity, product.Price, product.Discount);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
