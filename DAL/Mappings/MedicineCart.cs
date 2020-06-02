using DAL.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Mappings
{
   public partial class MedicineCart
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
