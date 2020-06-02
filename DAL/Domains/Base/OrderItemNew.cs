using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domains.Base
{
    public partial class OrderItemNew : BaseEntity
    {
        public int MedicineId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
    }
    public partial class OrderItemNewModel : BaseEntity
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineImageUrl { get; set; }
        public string MedicinePrice { get; set; }
        public int Quantity { get; set; }
        public string Total { get; set; }
        public string CartId { get; set; }
    }
}
