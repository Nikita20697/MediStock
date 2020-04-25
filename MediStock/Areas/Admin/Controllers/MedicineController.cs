﻿using BAL.Services;
using DAL.Data;
using DAL.Domains;
using MediStockWeb.Areas.Admin.Controllers.Base;
using MediStockWeb.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X.PagedList;

namespace MediStockWeb.Areas.Admin.Controllers
{
    public class MedicineController : BaseController
    {
        #region Fields

        private readonly IMedicineService _medicineService;
        #endregion

        #region Ctor

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        #endregion

        #region Methods

        public IActionResult List(int? page)
        {
            //get all medicine data.
            var medicineData = _medicineService.GetAllMedicines();
            var medicineList = new List<MedicineModel>();
            foreach (var item in medicineData)
            {
                var model = new MedicineModel();
                model.MedicineId = item.Id;
                model.Name = item.Name;
                model.SKU = item.SKU;
                model.ProductGUID = item.ProductGUID;
                model.Price = item.Price;
                model.Manufacturer = item.Manufacturer;
                model.ExpiryDate = item.ExpiryDate;
                model.IsActive = item.IsActive;
                model.IsDeleted = false;
                medicineList.Add(model);
            }
            var pageNumber = page ?? 1;
            var pageSize = 3;
            return View(medicineList.ToPagedList(pageNumber, pageSize));
        }

        //public IActionResult List(MedicineModel searchModel)
        //{
        //    return View(searchModel);
        //}

        public IActionResult Create()
        {
            var model = new MedicineModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MedicineModel model)
        {
            Medicine obj = new Medicine();
           
                // Model Prepration
                Medicine objMedicineModel = new Medicine
                {
                    Name = model.Name,
                    SKU = model.SKU,
                    ProductGUID = model.ProductGUID,
                    Price = model.Price,
                    Manufacturer = model.Manufacturer,
                    ManufacturingDate = model.ManufacturingDate,
                    Description = model.Description,
                    ExpiryDate = model.ExpiryDate,
                    IsActive = model.IsActive,
                    IsDeleted = false,
                    Stock = model.Stock,
                    Pictures = model.Pictures,
                    //CategoryMedicine = model.AllCategories
                };
            var medicines = _medicineService.InsertMedicine(objMedicineModel);
            obj = medicines;
            if (obj == null)
            {
                return RedirectToAction("Create", "AdminUser", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("List", new { id = objMedicineModel.Id });
            }

        }

        public IActionResult Edit(int id)
        {
            var medicineModel = _medicineService.GetMedicineById(id);

            MedicineModel model = new MedicineModel
            {
                MedicineId = medicineModel.Id,
                Name = medicineModel.Name,
                SKU = medicineModel.SKU,
                ProductGUID = medicineModel.ProductGUID,
                Price = medicineModel.Price,
                Manufacturer = medicineModel.Manufacturer,
                ManufacturingDate = medicineModel.ManufacturingDate,
                Description = medicineModel.Description,
                ExpiryDate = medicineModel.ExpiryDate,
                IsActive = medicineModel.IsActive,
                IsDeleted = false
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MedicineModel model)
        {
            Medicine medicineModel = new Medicine
            {
                Id = model.MedicineId,
                Name = model.Name,
                SKU = model.SKU,
                ProductGUID = model.ProductGUID,
                Price = model.Price,
                Manufacturer = model.Manufacturer,
                ManufacturingDate = model.ManufacturingDate,
                Description = model.Description,
                //Image = model.Image,
                ExpiryDate = model.ExpiryDate,
                IsActive = model.IsActive,
                IsDeleted = false
            };
            var medicineModels = _medicineService.UpdateMedicine(medicineModel);
            if (medicineModels == null)
            {
                // Failed
                ViewBag.AddCategoryStatus = "Failed";
                return RedirectToAction("Edit");
            }
            else
            {
                // Success
                ViewBag.AddUserStatus = "Success";
                return RedirectToAction("List");
            }
        }

        public IActionResult Delete(int id)
        {
            //_adminMedicineService.DeleteMedicine(id);
            //return RedirectToAction("List");

            var medicineData = _medicineService.DeleteMedicine(id);

            if (medicineData == null)
            {
                //Failed
                ViewBag.AddUserStatus = "Failed";
                return RedirectToAction("Create");
            }
            else
            {
                // Success
                ViewBag.AddUserStatus = "Success";
                return RedirectToAction("List");
            }
        }

        public ActionResult SearchMedicine(string searchString)
        {
            // Get particular user data
            var medicineByName = _medicineService.GetMedicineByName(searchString);
            var medicineNameList = new List<MedicineModel>();
            foreach (var item in medicineByName)
            {
                var lst = new MedicineModel();
                lst.MedicineId = item.Id;
                lst.Name = item.Name;
                lst.SKU = item.SKU;
                lst.ProductGUID = item.ProductGUID;
                lst.Manufacturer = item.Manufacturer;
                lst.ManufacturingDate = item.ManufacturingDate;
                lst.ExpiryDate = item.ExpiryDate;
                medicineNameList.Add(lst);
            }

            if (medicineNameList == null)
            {
                return View("List", medicineNameList);
            }
            else
            {
                return View("List", medicineNameList);
            }
        }
        #endregion
    }
}