using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common;
using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models.Car;
using CarPark.Web.Filters;
using CarPark.Web.Models.Car;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_CAR_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            model.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            model.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _carService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection,model.Filter.Filter_Plate).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_CAR_LIST)]
        [HttpPost]
        public ActionResult List(ListViewModel model)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (model.Filter == null)
            {
                model.Filter = new ListFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }

            model.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            model.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;

            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _carService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Plate).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_CAR_ADD)]
        public ActionResult Add()
        {
            Models.Car.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_CAR_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Car.AddViewModel modell)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error.";
                return View(modell);
            }

            Business.Models.Car.AddRequestModel car = new Business.Models.Car.AddRequestModel();
            car.Plate = modell.Plate;
            car.Brand = modell.Brand;
            car.Model = modell.Model;
            var apiResponseModel = _carService.Add(car).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(CarController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message != null ? apiResponseModel.Message : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                return View(modell);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_CAR_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Car.AddViewModel model = new AddViewModel();
            var apiResponseModel = _carService.GetById(id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var car = apiResponseModel.Data;
            if (car == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = car.Id;
            model.Plate = car.Plate;
            model.Brand = car.Brand;
            model.Model = car.Model;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_CAR_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Car.AddViewModel modell)
        {
            if (!ModelState.IsValid)
            {
                return View(modell);
            }

            var apiResponseModel = _carService.GetById(modell.Id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(modell);
            }

            var car = apiResponseModel.Data;

            if (car == null)
            {
                return View("_ErrorNotExist");
            }

            AddRequestModel editModel = new AddRequestModel();
            editModel.Id = car.Id;
            editModel.Plate = modell.Plate;
            editModel.Brand = modell.Brand;
            editModel.Model = modell.Model;

           
                var apiEditResponseModel = _carService.Edit(modell.Id, editModel).Result;
                if (apiEditResponseModel.Status != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiEditResponseModel.Message != null ? apiEditResponseModel.Message : "Not Edited";
                    return View(modell);
                }
           
            return RedirectToAction(nameof(CarController.List));
        }
    }
}
