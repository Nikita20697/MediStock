using DAL.Data;
using DAL.Domains;
using DAL.Domains.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Services
{
    public class ShoppingCart
    {
        private readonly MediStockContext _context;
        public ShoppingCart(MediStockContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<MediStockContext>();
            string cartId = session.GetString("CartID") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new Cart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Medicine medicine)
        {
            var shoppingCartItem = _context.OrderItemsNew.SingleOrDefault(s => s.MedicineId == medicine.Id);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new OrderItemNew
                {
                    CartId = ShoppingCartId,
                    MedicineId = medicine.Id,
                    Price = Convert.ToInt32(medicine.Price),
                    Quantity = 1,
                };
                _context.OrderItemsNew.Add(shoppingCartItem);
                _context.SaveChanges();
            }
           
        }
        public void RemoveFromCart(Medicine medicine)
        {
            var shoppingCartItem = _context.OrderItemsNew.SingleOrDefault(s => s.MedicineId == medicine.Id);
            _context.OrderItemsNew.Remove(shoppingCartItem);
            _context.SaveChanges();

        }
     

        public List<OrderItemNewModel> GetShoppingCartItemNew()
        {
            List<OrderItemNewModel> models = new List<OrderItemNewModel>();
            foreach (var item in _context.OrderItemsNew.ToList())
            {
                var medicin = _context.Medicines.SingleOrDefault(s => s.Id == item.MedicineId);
                var OrderItemNewModel = new OrderItemNewModel()
                {
                    MedicineId = medicin.Id,
                    MedicineName = medicin.Name,
                    MedicineImageUrl = "/images/" + medicin.PictureStr,
                    MedicinePrice = Convert.ToDecimal(medicin.Price).ToString(),
                    Quantity = item.Quantity,
                    Total = Convert.ToDecimal(item.Quantity * Convert.ToDecimal(medicin.Price)).ToString()
                };
                models.Add(OrderItemNewModel);
            }
            return models;
            //return OrderItems ??
            //    (OrderItems = _context.OrderItemsNew.ToList());
            //return _context.OrderItemsNew.ToList();

        }
        public void ClearCart()
        {
            var cartItems = _context.OrderItems.Where(cart => cart.CartId == ShoppingCartId);
            _context.OrderItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
            var total = _context.OrderItems.Where(c => c.CartId == ShoppingCartId)
                .Select(c => c.Medicine.Price * c.Price).Sum();
            return total;
        }
    }
}
