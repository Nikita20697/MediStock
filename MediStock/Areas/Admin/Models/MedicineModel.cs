﻿using DAL.Domains;
using System;
using System.Collections.Generic;

namespace MediStockWeb.Areas.Admin.Models
{
    public partial class MedicineModel 
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public Guid ProductGUID { get; set; }
        public decimal Price { get; set; }
        public int Manufacturer { get; set; }
        public string Description { get; set; }
        //public string Image { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Stock Stock { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<CategoryModel> AllCategories { get; set; }
    }
}
