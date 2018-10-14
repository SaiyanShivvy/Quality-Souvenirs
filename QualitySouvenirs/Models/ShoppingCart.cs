using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;

namespace QualitySouvenirs.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartID { get; set; }
        public const string CartSessionKey = "shoppingCartID";

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }

        public void AddToCart(Product product, QualitySouvenirsContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartID == ShoppingCartID && c.Product.ItemID == product.ItemID);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Product = product,
                    CartID = ShoppingCartID,
                    ItemCount = 1,
                    DateCreated = DateTime.Now
                };
                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.ItemCount++;
            }
            db.SaveChanges();
        }

        public int RemoveFromCart(int id, QualitySouvenirsContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.CartID == ShoppingCartID && cart.Product.ItemID == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.ItemCount > 1)
                {
                    cartItem.ItemCount--;
                    itemCount = cartItem.ItemCount;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart(QualitySouvenirsContext db)
        {
            var cartItems = db.CartItems.Where(cart => cart.CartID == ShoppingCartID);
            foreach (var cartItem in cartItems)
            {
                db.CartItems.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<CartItem> GetCartItems(QualitySouvenirsContext db)
        {
            List<CartItem> cartItems = db.CartItems.Include(ci => ci.Product).ThenInclude(b => b.Category).Where(ci => ci.CartID == ShoppingCartID).ToList();

            return cartItems;
        }

        public int GetCount(QualitySouvenirsContext db)
        {
            int? count =
                (from cartItems in db.CartItems where cartItems.CartID == ShoppingCartID select (int?)cartItems.ItemCount).Sum();
            return count ?? 0;
        }

        public decimal GetTotal(QualitySouvenirsContext db)
        {
            decimal? total = (from cartItems in db.CartItems
                              where cartItems.CartID == ShoppingCartID
                              select (int?)cartItems.ItemCount * cartItems.Product.Price).Sum();
            return total ?? decimal.Zero;
        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                Guid tempCartId = Guid.NewGuid();
                context.Session.SetString(CartSessionKey, tempCartId.ToString());
            }
            return context.Session.GetString(CartSessionKey).ToString();
        }
    }
}