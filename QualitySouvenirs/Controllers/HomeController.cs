using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QualitySouvenirs.Models;
using QualitySouvenirs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QualitySouvenirs.Controllers
{
    public class HomeController : Controller
    {
        private readonly QualitySouvenirsContext _context;

        public HomeController(QualitySouvenirsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ProductViewModel pVM = new ProductViewModel { Products = await _context.Products.ToListAsync(), Categories = await _context.Categories.ToListAsync() };
            return View(pVM);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Thinking about why I made this site?";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Queries or Feedback? Here's how to reach us!";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}