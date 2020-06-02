using DAL.Data;
using MediStockWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediStockWeb.Views.ViewComponents
{
    public class MedicineProductViewComponent : ViewComponent
    {
        #region Fields
        private readonly MediStockContext _context;

        #endregion

        #region Ctor
        public MedicineProductViewComponent(MediStockContext context)
        {
            _context = context;
        }
        #endregion

        public IViewComponentResult Invoke()
        {
            var medicine = _context.Medicines.ToList();
            var homelist = new List<HomeModel>();
            foreach (var item in medicine)
            {
                var model = new HomeModel
                {
                    MedicineId = item.Id,
                    ImageUrl = "/images/" + item.PictureStr,
                    Name = item.Name,
                    Price = item.Price

                };
                homelist.Add(model);
            }
            return View(homelist);
        }
    }
}

