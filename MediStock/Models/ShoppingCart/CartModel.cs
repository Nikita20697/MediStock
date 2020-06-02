using DAL.Domains;
using DAL.Domains.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediStockWeb.Models.ShoppingCart
{
    public class CartModel
    {
        public CartModel()
        {
            orderItems = new List<OrderItemNewModel>();
        }

        public string ShoppingCartId { get; set; }

        public List<OrderItemNewModel> orderItems { get; set; }
        public Order Order { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
