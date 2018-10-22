using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.Include(i => i.User).AsNoTracking().ToListAsync());
        }

        // GET: Orders/Create
        [Authorize(Roles = "Member")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Create([Bind("City,Country,FirstName,LastName,Phone,PostalCode,State")] Order order)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
                List<CartItem> items = cart.GetCartItems(_context);
                List<OrderItem> details = new List<OrderItem>();
                foreach (CartItem item in items)
                {
                    OrderItem detail = CreateOrderDetailForThisItem(item);
                    detail.Order = order;
                    details.Add(detail);
                    _context.Add(detail);
                }

                order.User = user;
                order.Date = DateTime.Today;
                order.GrandTotal = ShoppingCart.GetCart(this.HttpContext).GetGrandTotal(_context);
                order.OrderItems = details;
                _context.SaveChanges();

                return RedirectToAction("Purchased", new RouteValueDictionary(
                new { action = "Purchased", id = order.OrderID }));
            }

            return View(order);
        }

        private OrderItem CreateOrderDetailForThisItem(CartItem item)
        {
            OrderItem detail = new OrderItem();

            detail.Quantity = item.ItemCount;
            detail.Product = item.Product;
            detail.OrderItemPrice = item.Product.Price;

            return detail;
        }

        public async Task<IActionResult> Purchased(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderItems.Where(detail => detail.Order.OrderID == order.OrderID).Include(detail => detail.Product).ToList();

            order.OrderItems = details;
            ShoppingCart.GetCart(this.HttpContext).EmptyCart(_context);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(i => i.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderItems.Where(detail => detail.Order.OrderID == order.OrderID).Include(detail => detail.Product).ToList();
            order.OrderItems = details;

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}