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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
            ViewBag.Cate = await db.Category.ToListAsync();
            return View(list);
        }


        [HttpPost]
        public async Task<IActionResult> Index(string fil)
        {
            ViewBag.Cate = await db.Category.ToListAsync();
            
            if (fil == "All")
            {
                var AfiltProduct =  db.Products.OrderBy(m=>m).Include(m => m.Category).ToList();
                return View(AfiltProduct);
            }
            var filtProduct = db.Products.Include(m => m.Category).Where(m => m.Category.List.ToString() == fil).ToList();
            
            return View(filtProduct);

        }

        [HttpGet]
        public  async Task<IActionResult> Create()
        {
            ViewBag.Cate = await db.Category.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product,int id)
        {
            var Category = await db.Category.SingleAsync(m => m.CategoryId == id);
            var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
            product.Category = Category;
            ViewBag.Cate = await db.Category.ToListAsync();
            if (ModelState.IsValid)
            {
                await db.AddAsync(product);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Index",list);
            }
            return RedirectToAction("Index",list);
            
        }
        

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await db.Products.SingleAsync(m => m.ProductId == id);
            db.Remove(product);
            await db.SaveChangesAsync();
            var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
            ViewBag.Cate = await db.Category.ToListAsync();
            return RedirectToAction("Index",list);
        }
        
        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            var product = await db.Products.Include(m => m.Category).SingleAsync(m => m.ProductId == id);
            ViewBag.Cates = await db.Category.ToListAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p, int id)
        {
            var newp = await db.Products.SingleAsync(m => m.ProductId == p.ProductId);
            var c = await db.Category.SingleAsync(m => m.CategoryId == id);
            newp.Name = p.Name;
            newp.Price = p.Price;
            newp.Category = c;
            db.Update(newp);
            await db.SaveChangesAsync();


            var list = await db.Products.OrderBy(m => m).Include(m => m.Category).ToListAsync();
            ViewBag.Cate = await db.Category.ToListAsync();
            return RedirectToAction("Index",list);
        }

    }
}
