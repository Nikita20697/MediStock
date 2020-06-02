using DAL.Data;
using DAL.Domains.Base;
using System.Collections.Generic;

namespace DAL.Domains
{
    public partial class Cart : BaseEntity
    {
        public Cart(MediStockContext context)
        {
        }

        public string ShoppingCartId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Order Order { get; set; }
    }

}
