using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common;
using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models.Personnel;
using CarPark.Web.Models.Personnel;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Web.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly IPersonnelService _personnelService ;
        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }


        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            model.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            model.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _personnelService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Name,model.Filter.Filter_LastName).Result;
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

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_LIST)]
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

            var apiResponseModel = _personnelService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Name, model.Filter.Filter_LastName).Result;
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

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_ADD)]
        public ActionResult Add()
        {
            Models.Personnel.AddViewModel model = new AddViewModel();
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Personnel.AddViewModel modell)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error.";
                return View(modell);
            }

            Business.Models.Personnel.AddRequestModel personnel = new Business.Models.Personnel.AddRequestModel();
            personnel.TC = modell.TC;
            personnel.Name = modell.Name;
            personnel.LastName = modell.LastName;
            personnel.Phone = modell.Phone;
            personnel.Address = modell.Address;
            personnel.UserName = modell.UserName;
            personnel.Password = modell.Password;
            var apiResponseModel = _personnelService.Add(personnel).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(PersonnelController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message != null ? apiResponseModel.Message : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                return View(modell);
            }
        }

        // [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Personnel.AddViewModel model = new AddViewModel();
            var apiResponseModel = _personnelService.GetById(id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var personnel = apiResponseModel.Data;
            if (personnel == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = personnel.Id;
            model.TC = personnel.TC;
            model.Name = personnel.Name;
            model.LastName = personnel.LastName;
            model.Phone = personnel.Phone;
            model.Address = personnel.Address;
            model.UserName = personnel.UserName;
            model.Password = personnel.Password;

            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Personnel.AddViewModel modell)
        {
            if (!ModelState.IsValid)
            {
                return View(modell);
            }

            var apiResponseModel = _personnelService.GetById(modell.Id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(modell);
            }

            var personnel = apiResponseModel.Data;

            if (personnel == null)
            {
                return View("_ErrorNotExist");
            }

            AddRequestModel editModel = new AddRequestModel();
            editModel.Id = personnel.Id;
            editModel.TC = modell.TC;
            editModel.Name = modell.Name;
            editModel.LastName = modell.LastName;
            editModel.Phone = modell.Phone;
            editModel.Address = modell.Address;
            editModel.UserName = modell.UserName;
            editModel.Password = modell.Password;

            var apiEditResponseModel = _personnelService.Edit(modell.Id, editModel).Result;
            if (apiEditResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.Message != null ? apiEditResponseModel.Message : "Not Edited";
                return View(modell);
            }

            return RedirectToAction(nameof(PersonnelController.List));
        }
    }
}
