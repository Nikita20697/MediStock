﻿using DAL.Domains.Base;
using System;

namespace DAL.Domains
{
    public partial class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public int OrderStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ShoppingCartId { get; set; }
        public Cart ShoppingCart { get; set; }

    }
}
