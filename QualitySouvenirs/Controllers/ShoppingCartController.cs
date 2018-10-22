using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace QualitySouvenirs.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Member")]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Return the view
            return View(cart);
        }

        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the item from the database
            var addItem = _context.Products
                .Single(p => p.ItemID == id);
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addItem, _context);
            // Go back to the main store page for more shopping
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            int itemCount = cart.RemoveFromCart(id, _context);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult EmptyCart()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.EmptyCart(_context);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}