using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models.LocationMove;
using CarPark.Web.Models.LocationMove;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarPark.Web.Controllers
{
    public class LocationMoveController : Controller
    {
        private readonly ILocationMoveService _locationMoveService;
        private readonly ICarService _carService;

        public LocationMoveController(ILocationMoveService locationMoveService, ICarService carService )
        {
            _locationMoveService = locationMoveService;
            _carService = carService;
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATIONMOVE_LIST)]
        public IActionResult List()
        {
            List<LocationWithDetail> model = new List<LocationWithDetail>();
            var apiResponseModel = _locationMoveService.GetAllWithDetail().Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                model = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATIONMOVE_LIST)]
        public ActionResult Add(int locationId)
        {
            if (locationId <= 0)
            {
                return View("_ErrorNotExist");
            }
            Models.LocationMove.AddViewModel model = new AddViewModel();
            var apiResponseModel = _locationMoveService.GetLocationByLocationIdWithDetail(locationId).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                //not error
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var roomWithDetail = apiResponseModel.Data;
            if (roomWithDetail.IsEmpty == false)
            {
                return View("_ErrorNotExist");
            }
            model.LocationId = roomWithDetail.LocationId;

            //select list
            model.CarSelectList = GetCarSelectList();
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATIONMOVE_LIST)]
        [HttpPost]
        public ActionResult Add(Models.LocationMove.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CarSelectList = GetCarSelectList();
                return View(model);
            }
            var apiResponseModel = _locationMoveService.GetLocationByLocationIdWithDetail(model.LocationId).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                //not error
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }
            var locationMove = apiResponseModel.Data;
            if (locationMove.IsEmpty == false)
            {
                return View("_ErrorNotExist");
            }

            AddRequestModel record = new AddRequestModel();
            record.CarId = model.CarId;
            record.LocationId = locationMove.LocationId;
            record.EntryDate = DateTime.Now;
            var apiResult = _locationMoveService.Add(record).Result;
            if (apiResult.Status == ResultStatusCodeStatic.Success)
            {
                //not error
            }
            else
            {
                ViewBag.ErrorMessage = apiResult.Message;
                return View(model);
            }
            var result = apiResult.Data;
            if (result.Id > 0)
            {
                return RedirectToAction(nameof(LocationMoveController.List));
            }
            else
            {
                ViewBag.ErrorMessage = "Not Saved";
                model.CarSelectList = GetCarSelectList();
                return View(model);
            }
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATIONMOVE_LIST)]
        public ActionResult Edit(int locationId)
        {
            if (locationId <= 0)
            {
                return View("_ErrorNotExist");
            }
            Models.LocationMove.AddViewModel model = new AddViewModel();
            var locationWithDetail = _locationMoveService.GetLocationByLocationIdWithDetail(locationId).Result.Data;
            if (locationWithDetail.IsEmpty == true)
            {
                return View("_ErrorNotExist");
            }

            var locationMoveResponce = _locationMoveService.GetById(locationWithDetail.LocationMove_ID).Result.Data;
            if (locationMoveResponce == null)
            {
                return View("_ErrorNotExist");
            }
            model.CarId = locationMoveResponce.CarId;
            model.LocationId = locationMoveResponce.LocationId;
            model.EntryDate = locationMoveResponce.EntryDate;
            model.ExitDate = locationMoveResponce.ExitDate;
            model.Id = locationMoveResponce.Id;
            model.Location_Name = locationWithDetail.LocationName;

            model.CarSelectList = GetCarSelectList();
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_LOCATIONMOVE_LIST)]
        [HttpPost]
        public ActionResult Edit(Models.LocationMove.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CarSelectList = GetCarSelectList();
                return View(model);
            }

            var locationMove = _locationMoveService.GetById(model.Id).Result.Data;
            if (locationMove.ExitDate != null)
            {
                return View("_ErrorNotExist");
            }

            AddRequestModel addRequestModel = new AddRequestModel();
            addRequestModel.Id = locationMove.Id;
            addRequestModel.CarId = locationMove.CarId;
            addRequestModel.LocationId = locationMove.LocationId;
            addRequestModel.EntryDate = locationMove.EntryDate;
            addRequestModel.ExitDate = DateTime.Now;

            var result = _locationMoveService.Edit(model.Id, addRequestModel).Result;
            if (result.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = "Not Edited";
                model.CarSelectList = GetCarSelectList();

                return View(model);
            }
            return RedirectToAction(nameof(LocationMoveController.List));
        }




        [NonAction]
        private List<SelectListItem> GetCarSelectList()
        {
            //müşteri kayıtları listelenir
            List<SelectListItem> resultList = new List<SelectListItem>();
            resultList = _locationMoveService.GetAllCarWhichIsNotLocationMove().Result.Data.OrderBy(r => r.Plate).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Plate }).ToList();
            return resultList;
        }
    }
}
