using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common;
using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models.Profile;
using CarPark.Web.Filters;
using CarPark.Web.Models.Profile;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        
        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();
            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            model.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            model.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _profileService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Code, model.Filter.Filter_Name).Result;
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_LIST)]
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
            var apiResponseModel = _profileService.GetAllPaginatedWithDetailBySearchFilter(model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection, model.Filter.Filter_Code, model.Filter.Filter_Name).Result;
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_ADD)]
        public ActionResult Add()
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           Business.Models.Profile.AddRequestModel profile = new Business.Models.Profile.AddRequestModel();
            profile.Code = model.Code;
            profile.Name = model.Name;
            var apiResponseModel = _profileService.Add(profile).Result;
            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(ProfileController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.Message != null ? apiResponseModel.Message : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            var apiResponseModel = _profileService.GetById(id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var profile = apiResponseModel.Data;
            if (profile == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = profile.Id;
            model.Code = profile.Code;
            model.Name = profile.Name;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var apiResponseModel = _profileService.GetById(model.Id).Result;
            if (apiResponseModel.Status != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.Message;
                return View(model);
            }

            var profile = apiResponseModel.Data;

            if (profile == null)
            {
                return View("_ErrorNotExist");
            }

            AddRequestModel editModel = new AddRequestModel();
            editModel.Id = profile.Id;
            editModel.Code = model.Code;
            editModel.Name = model.Name;

            if (model.SubmitType == "Edit")
            {
                var apiEditResponseModel = _profileService.Edit(model.Id, editModel).Result;
                if (apiEditResponseModel.Status != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiEditResponseModel.Message != null ? apiEditResponseModel.Message : "Not Edited";
                    return View(model);
                }
            }
            if (model.SubmitType == "Delete")
            {
                var apiDeleteResponseModel = _profileService.Delete(profile.Id).Result;
                if (apiDeleteResponseModel.Status != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiDeleteResponseModel.Message != null ? apiDeleteResponseModel.Message : "Not Deleted";
                    return View(model);
                }
            }
            return RedirectToAction(nameof(ProfileController.List));
        }
    }
}
