using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW_26.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_26.Controllers
{
    public class ClientController : Controller
    {
        private readonly DateDb db;
        public ClientController(DateDb context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(string category)
        {
            var list = await db.Products.Where(m=> category==null||m.Category.List.ToString()==category)
                .OrderBy(m => m).Include(m => m.Category).ToListAsync();
            ViewBag.Li = await db.Category.ToListAsync();
            ViewBag.Pro = await db.Products.Include(m => m.Category).ToListAsync();
            return View(list);
        }
    }
}