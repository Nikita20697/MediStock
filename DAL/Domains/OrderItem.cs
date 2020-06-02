using DAL.Domains.Base;

namespace DAL.Domains
{
    public partial class OrderItem : BaseEntity
    {
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
