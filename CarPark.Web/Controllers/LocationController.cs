using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common;
using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models.Location;
using CarPark.Web.Models.Location;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATION_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            model.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            model.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _locationService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Name).Result;
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

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATION_LIST)]
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

            var apiResponseModel = _locationService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Name).Result;
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

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATION_ADD)]
        public ActionResult Add()
        {
            Models.Location.AddViewModel model = new AddViewModel();
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATION_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Location.AddViewModel modell)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error.";
                return View(modell);
            }

            Business.Models.Location.AddRequestModel location = new Business.Models.Location.AddRequestModel();
            location.Name = modell.Name;
            
            var apiResponseModel = _locationService.Add(location).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(LocationController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message != null ? apiResponseModel.Message : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                return View(modell);
            }
        }

        // [AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATION_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Location.AddViewModel model = new AddViewModel();
            var apiResponseModel = _locationService.GetById(id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var location = apiResponseModel.Data;
            if (location == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = location.Id;
            model.Name = location.Name;
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATION_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Location.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiResponseModel = _locationService.GetById(model.Id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var location = apiResponseModel.Data;

            if (location == null)
            {
                return View("_ErrorNotExist");
            }

            AddRequestModel editModel = new AddRequestModel();
            editModel.Id = location.Id;
            editModel.Name = model.Name;

            var apiEditResponseModel = _locationService.Edit(model.Id, editModel).Result;
            if (apiEditResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.Message != null ? apiEditResponseModel.Message : "Not Edited";
                return View(model);
            }

            return RedirectToAction(nameof(LocationController.List));
        }
    }
}
