using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Models.ShoppingCartViewModels;
using Microsoft.AspNetCore.Mvc;

namespace QualityBags.ViewComponents
{
    public class ShoppingCartViewModelViewComponent : ViewComponent
    {
        private readonly QualitySouvenirsContext _context;

        public ShoppingCartViewModelViewComponent(QualitySouvenirsContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(ReturnCurrentCartViewModel());
        }

        public ShoppingCartViewModel ReturnCurrentCartViewModel()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(_context),
                GrandTotal = cart.GetTotal(_context),
                TotalCount = cart.GetCount(_context)
            };

            return viewModel;
        }
    }
}