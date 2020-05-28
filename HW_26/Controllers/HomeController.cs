using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HW_26.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HW_26.Controllers
{



    public class HomeController : Controller
    {
        private readonly DateDb db;

        public HomeController(DateDb context)
        {
            this.db = context;
        }
        public async Task<ViewResult> Index()
        {
            var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public  async Task<IActionResult> Create()
        {
            ViewBag.Cate = await db.Category.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Create(Product product,int id)
        {
            var Category = await db.Category.SingleAsync(m => m.CategoryId == id);
            
            product.Category = Category;
            if (ModelState.IsValid)
            {
                await db.AddAsync(product);
                await db.SaveChangesAsync();
                var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
                return View("Index",list);
            }
            return View();
            
        }
        

        [HttpGet]
        public async Task<ViewResult> Remove(int id)
        {
            var product = await db.Products.SingleAsync(m => m.ProductId == id);
            db.Remove(product);
            await db.SaveChangesAsync();
            var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
            return View("Index",list);
        }
        
        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            var product = await db.Products.Include(m => m.Category).SingleAsync(m => m.ProductId == id);
            ViewBag.Cate = await db.Category.ToListAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<ViewResult> Edit(Product p, int id)
        {
            var newp = db.Products.Single(m => m.ProductId == p.ProductId);
            var c =  db.Category.Single(m => m.CategoryId == id);
            newp.Name = p.Name;
            newp.Price = p.Price;
            newp.Category = c;
            db.Update(newp);
            await db.SaveChangesAsync();


            var list = await db.Products.OrderBy(m => m).Include(m => m.Category).ToListAsync();
            return View("Index",list);
        }

    }
}
