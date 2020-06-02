using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Services;
using DAL.Data;
using DAL.Domains;
using MediStockWeb.Models.ShoppingCart;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MediStockWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMedicineService _medicineService;
        private readonly MediStockContext _context;
        private readonly ShoppingCart _shoppingCart;
        public CartController(
        IWebHostEnvironment environment,
        MediStockContext context,
        IMedicineService medicineService,
            ShoppingCart shoppingCart
            )
        {
            _env = environment;
            _context = context;
            _medicineService = medicineService;
            _shoppingCart = shoppingCart;
        }
        
        public ViewResult ShoppingCart()
        {
            CartModel cartModel = new CartModel();
            
            var items = _shoppingCart.GetShoppingCartItemNew();
            cartModel.orderItems = items;
            return View(cartModel);

        }
        public ActionResult RemoveFromCart(int id)
        {
            var selectedMedicine = _context.Medicines.FirstOrDefault(p => p.Id == id);
            if (selectedMedicine != null)
            {
                _shoppingCart.RemoveFromCart(selectedMedicine);
            }

            return RedirectToAction("ShoppingCart");

        }
        public ActionResult AddToCart(int id)
        {
            var selectedMedicine = _context.Medicines.FirstOrDefault(p => p.Id == id);
            if (selectedMedicine != null)
            {
                _shoppingCart.AddToCart(selectedMedicine);
            }

            return RedirectToAction("ShoppingCart");
        }
    }
}