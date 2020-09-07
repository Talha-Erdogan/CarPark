using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common;
using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models.ProfileDetail;
using CarPark.Web.Models.ProfileDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarPark.Web.Controllers
{
    public class ProfileDetailController : Controller
    {
        private readonly IProfileDetailService _profileDetailService;
        private readonly IProfileService _profileService;

        public ProfileDetailController(IProfileDetailService profileDetailService, IProfileService profileService)
        {
            _profileDetailService = profileDetailService;
            _profileService = profileService;
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILEDETAIL_BATCHEDIT)]
        public ActionResult BatchEdit()
        {
            BatchEditViewModel model = new BatchEditViewModel();
            model.ProfileSelectList = GetProfileSelectList();
            model.AuthList = new List<AuthCheckViewModel>();
            model.AuthWhichIsNotIncludeList = new List<AuthCheckViewModel>();
            return View(model);
        }

        [HttpPost]
        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILEDETAIL_BATCHEDIT)]
        public ActionResult BatchEdit(BatchEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.ProfileId.HasValue)
                {
                    model.AuthList = GetAllAuthByProfileId(model.ProfileId.Value);
                    model.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded(model.ProfileId.Value);
                }
                else
                {
                    model.AuthList = new List<AuthCheckViewModel>();
                    model.AuthWhichIsNotIncludeList = new List<AuthCheckViewModel>();
                }
                return View(model);
            }

            if (model.SubmitType == "Add")
            {
                if (model.AuthWhichIsNotIncludeList != null)
                {
                    ModelState.Clear();
                    List<AuthCheckViewModel> record = model.AuthWhichIsNotIncludeList.Where(x => x.Checked == true).ToList();
                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                            AddRequestModel profileDetail = new AddRequestModel();
                            profileDetail.AuthId = item.Id;
                            profileDetail.ProfileId = model.ProfileId.Value;
                            var result = _profileDetailService.Add(profileDetail).Result;
                        }
                    }
                }
            }
            if (model.SubmitType == "Delete")
            {
                if (model.AuthList != null)
                {
                    ModelState.Clear();
                    List<AuthCheckViewModel> record = model.AuthList.Where(x => x.Checked == true).ToList();
                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                            var apiResponseModel = _profileDetailService.Delete(model.ProfileId.Value, item.Id).Result;
                            if (apiResponseModel.Status == ResultStatusCodeStatic.Success)
                            {
                                //not error
                            }
                            else
                            {
                                BatchEditViewModel batchEditViewModel = new BatchEditViewModel();

                                batchEditViewModel.ProfileSelectList = GetProfileSelectList();


                                batchEditViewModel.AuthList = GetAllAuthByProfileId( model.ProfileId.Value);
                                batchEditViewModel.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded( model.ProfileId.Value);
                                ViewBag.ErrorMessage = apiResponseModel.Message;
                                return View(batchEditViewModel);
                            }
                        }
                    }
                }
            }


            model.ProfileSelectList = GetProfileSelectList();
            if (model.ProfileId.HasValue)
            {
                model.AuthList = GetAllAuthByProfileId(model.ProfileId.Value);
                model.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded( model.ProfileId.Value);
            }
            else
            {
                model.AuthList = new List<AuthCheckViewModel>();
                model.AuthWhichIsNotIncludeList = new List<AuthCheckViewModel>();
            }
            return View(model);
        }

        [NonAction]
        private List<AuthCheckViewModel> GetAllAuthByProfileId(int profileId)
        {
            //profile ait yetki kayıtları listelenir
            List<AuthCheckViewModel> resultList = new List<AuthCheckViewModel>();
            resultList = _profileDetailService.GetAllAuthByProfileId(profileId).Result.Data.Select(a => new AuthCheckViewModel() { Id = a.Id, Name = a.Name, Checked = false, Code = a.Code }).ToList();
            return resultList;
        }

        [NonAction]
        private List<AuthCheckViewModel> GetAllAuthByProfileIdWhichIsNotIncluded(int profileId)
        {
            //profile ait olmayan yetki kayıtları listelenir
            List<AuthCheckViewModel> resultList = new List<AuthCheckViewModel>();
            resultList = _profileDetailService.GetAllAuthByProfileIdWhichIsNotIncluded(profileId).Result.Data.Select(a => new AuthCheckViewModel() { Id = a.Id, Name = a.Name, Checked = false, Code = a.Code }).ToList();
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetProfileSelectList()
        {
            // aktif profil kayıtları listelenir
            List<SelectListItem> resultList = new List<SelectListItem>();
            resultList = _profileService.GetAll().Result.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }
    }
}
