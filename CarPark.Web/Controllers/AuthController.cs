using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common;
using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models.Auth;
using CarPark.Web.Filters;
using CarPark.Web.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            model.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            model.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Code, model.Filter.Filter_Name).Result;
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
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

            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Code, model.Filter.Filter_Name).Result;
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        public ActionResult Add()
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Business.Models.Auth.AddRequestModel auth = new Business.Models.Auth.AddRequestModel();
            auth.Code = model.Code;
            auth.Name = model.Name;
            var apiResponseModel = _authService.Add(auth).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(AuthController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message != null ? apiResponseModel.Message : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            var apiResponseModel = _authService.GetById( id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var auth = apiResponseModel.Data;
            if (auth == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = auth.Id;
            model.Code = auth.Code;
            model.Name = auth.Name;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiResponseModel = _authService.GetById( model.Id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var auth = apiResponseModel.Data;

            if (auth == null)
            {
                return View("_ErrorNotExist");
            }
           
            AddRequestModel editModel = new AddRequestModel();
            editModel.Id = auth.Id;
            editModel.Code = model.Code;
            editModel.Name = model.Name;

            if (model.SubmitType == "Edit")
            {
                var apiEditResponseModel = _authService.Edit( model.Id, editModel).Result;
                if (apiEditResponseModel.Status != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiEditResponseModel.Message != null ? apiEditResponseModel.Message : "Not Edited";
                    return View(model);
                }
            }
            if (model.SubmitType == "Delete")
            {
                var apiDeleteResponseModel = _authService.Delete(auth.Id).Result;
                if (apiDeleteResponseModel.Status != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiDeleteResponseModel.Message != null ? apiDeleteResponseModel.Message : "Not Deleted";
                    return View(model);
                }
            }
            return RedirectToAction(nameof(AuthController.List));
        }
    }
}
