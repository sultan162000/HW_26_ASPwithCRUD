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
        public ViewResult Index()
        {
            return View();
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
                return View("Index");
            }
            return View();
            
        }

        public async Task<ViewResult> ShowDate()
        {
            var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
            if (list.Count() == 0)
            {
                return View("Index");
            }
            return View(list);
        }

        [HttpGet]
        public async Task<ViewResult> Remove(int? id)
        {
            if (id == 0)
            {
                return View("Index");
            }
            var product = await db.Products.SingleAsync(m => m.ProductId == id);
            db.Remove(product);
            await db.SaveChangesAsync();
            var list = await db.Products.OrderBy(p => p).Include(p => p.Category).ToListAsync();
            if (list.Count() == 0)
            {
                return View("Index");
            }
            return View("ShowDate",list);
        }


    }
}
