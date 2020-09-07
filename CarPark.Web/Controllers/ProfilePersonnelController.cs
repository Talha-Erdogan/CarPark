using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Enums;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.ProfilePersonnel;
using CarPark.Web.Filters;
using CarPark.Web.Models.ProfilePersonnel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarPark.Web.Controllers
{
    public class ProfilePersonnelController : Controller
    {
        private readonly IProfilePersonnelService _profilePersonnelService;
        private readonly IProfileService _profileService;
        private readonly IPersonnelService _personnelService;

        public ProfilePersonnelController(IProfilePersonnelService profilePersonnelService, IProfileService profileService, IPersonnelService personnelService)
        {
            _profilePersonnelService = profilePersonnelService;
            _profileService = profileService;
            _personnelService = personnelService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILEPERSONNEL_BATCHEDIT)]
        public ActionResult BatchEdit()
        {
            BatchEditViewModel model = new BatchEditViewModel();
            model.ProfileSelectList = GetProfileSelectList();
            model.PersonnelList = new DefinedPersonnelListViewModel();
            model.PersonnelList.Filter = new DefinedPersonnelListFilterViewModel();
            model.PersonnelList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
            model.PersonnelList.CurrentPage = 1;
            model.PersonnelList.PageSize = 10;


            model.PersonnelWhichIsNotIncludeList = new UndefinedPersonnelListViewModel();
            model.PersonnelWhichIsNotIncludeList.Filter = new UndefinedPersonnelListFilterViewModel();
            model.PersonnelWhichIsNotIncludeList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
            model.PersonnelWhichIsNotIncludeList.CurrentPage = 1;
            model.PersonnelWhichIsNotIncludeList.PageSize = 10;

            return View(model);
        }

        [HttpPost]
        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILEPERSONNEL_BATCHEDIT)]
        public ActionResult BatchEdit(BatchEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ProfileSelectList = GetProfileSelectList();

                if (model.ProfileId.HasValue)
                {
                    if (model.PersonnelList.Filter == null)
                    {
                        model.PersonnelList.Filter = new DefinedPersonnelListFilterViewModel();
                    }
                    if (model.PersonnelWhichIsNotIncludeList.Filter == null)
                    {
                        model.PersonnelWhichIsNotIncludeList.Filter = new UndefinedPersonnelListFilterViewModel();
                    }

                    if (!model.PersonnelList.CurrentPage.HasValue)
                    {
                        model.PersonnelList.CurrentPage = 1;
                    }

                    if (!model.PersonnelList.PageSize.HasValue)
                    {
                        model.PersonnelList.PageSize = 10;
                    }

                    if (!model.PersonnelWhichIsNotIncludeList.CurrentPage.HasValue)
                    {
                        model.PersonnelWhichIsNotIncludeList.CurrentPage = 1;
                    }

                    if (!model.PersonnelWhichIsNotIncludeList.PageSize.HasValue)
                    {
                        model.PersonnelWhichIsNotIncludeList.PageSize = 10;
                    }
                    
                    model.PersonnelList.DataList = GetAllEmployeeByProfileId(model.ProfileId.Value,model.PersonnelList.Filter.Filter_Personnel_Name,model.PersonnelList.Filter.Filter_Personnel_LastName,model.PersonnelList.CurrentPage.Value,model.PersonnelList.PageSize.Value,model.PersonnelList.SortOn,model.PersonnelList.SortDirection);
                    model.PersonnelWhichIsNotIncludeList.DataList = GetAllEmployeeByProfileIdWhichIsNotIncluded(model.ProfileId.Value, model.PersonnelWhichIsNotIncludeList.Filter.Filter_Personnel_Name, model.PersonnelWhichIsNotIncludeList.Filter.Filter_Personnel_LastName, model.PersonnelWhichIsNotIncludeList.CurrentPage.Value, model.PersonnelWhichIsNotIncludeList.PageSize.Value, model.PersonnelWhichIsNotIncludeList.SortOn, model.PersonnelWhichIsNotIncludeList.SortDirection);
                }
                else
                {
                    model.PersonnelList = new DefinedPersonnelListViewModel();
                    model.PersonnelList.Filter = new DefinedPersonnelListFilterViewModel();
                    model.PersonnelList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
                    model.PersonnelList.CurrentPage = 1;
                    model.PersonnelList.PageSize = 10;

                    model.PersonnelWhichIsNotIncludeList = new UndefinedPersonnelListViewModel();
                    model.PersonnelWhichIsNotIncludeList.Filter = new UndefinedPersonnelListFilterViewModel();
                    model.PersonnelWhichIsNotIncludeList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
                    model.PersonnelWhichIsNotIncludeList.CurrentPage = 1;
                    model.PersonnelWhichIsNotIncludeList.PageSize = 10;
                }
                return View(model);
            }

            if (model.SubmitType == "Add")
            {
                if (model.PersonnelWhichIsNotIncludeList.DataList != null && model.ProfileId.HasValue)
                {
                    ModelState.Clear();
                    List<PersonnelCheckViewModel> records = model.PersonnelWhichIsNotIncludeList.DataList.Items.Where(x => x.Checked == true).ToList();
                    if (records != null)
                    {
                        foreach (var item in records)
                        {
                            AddRequestModel profilePersonnel = new AddRequestModel();
                            profilePersonnel.PersonnelId = item.Id;
                            profilePersonnel.ProfileId = model.ProfileId.Value;
                            _profilePersonnelService.Add(profilePersonnel);
                        }
                    }
                }
            }
            if (model.SubmitType == "Delete")
            {
                if (model.PersonnelList.DataList != null && model.ProfileId.HasValue)
                {
                    ModelState.Clear();
                    List<PersonnelCheckViewModel> record = model.PersonnelList.DataList.Items.Where(x => x.Checked == true).ToList();
                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                            var apiResponseModel = _profilePersonnelService.Delete(model.ProfileId.Value, item.Id);
                        }
                    }
                }
            }

            model.ProfileSelectList = GetProfileSelectList();
            if (model.ProfileId.HasValue)
            {
                if (model.PersonnelList.Filter == null)
                {
                    model.PersonnelList.Filter = new DefinedPersonnelListFilterViewModel();
                }
                if (model.PersonnelWhichIsNotIncludeList.Filter == null)
                {
                    model.PersonnelWhichIsNotIncludeList.Filter = new UndefinedPersonnelListFilterViewModel();
                }

                if (!model.PersonnelList.CurrentPage.HasValue)
                {
                    model.PersonnelList.CurrentPage = 1;
                }

                if (!model.PersonnelList.PageSize.HasValue)
                {
                    model.PersonnelList.PageSize = 10;
                }

                if (!model.PersonnelWhichIsNotIncludeList.CurrentPage.HasValue)
                {
                    model.PersonnelWhichIsNotIncludeList.CurrentPage = 1;
                }

                if (!model.PersonnelWhichIsNotIncludeList.PageSize.HasValue)
                {
                    model.PersonnelWhichIsNotIncludeList.PageSize = 10;
                }

                model.PersonnelList.DataList = GetAllEmployeeByProfileId(model.ProfileId.Value, model.PersonnelList.Filter.Filter_Personnel_Name, model.PersonnelList.Filter.Filter_Personnel_LastName, model.PersonnelList.CurrentPage.Value, model.PersonnelList.PageSize.Value, model.PersonnelList.SortOn, model.PersonnelList.SortDirection);
                model.PersonnelWhichIsNotIncludeList.DataList = GetAllEmployeeByProfileIdWhichIsNotIncluded(model.ProfileId.Value, model.PersonnelWhichIsNotIncludeList.Filter.Filter_Personnel_Name, model.PersonnelWhichIsNotIncludeList.Filter.Filter_Personnel_LastName, model.PersonnelWhichIsNotIncludeList.CurrentPage.Value, model.PersonnelWhichIsNotIncludeList.PageSize.Value, model.PersonnelWhichIsNotIncludeList.SortOn, model.PersonnelWhichIsNotIncludeList.SortDirection);

                if (model.PersonnelList.DataList == null || model.PersonnelList.DataList.Items == null)
                {
                    model.PersonnelList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
                }
                if (model.PersonnelWhichIsNotIncludeList.DataList == null || model.PersonnelWhichIsNotIncludeList.DataList.Items == null)
                {
                    model.PersonnelWhichIsNotIncludeList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
                }
            }
            else
            {
                model.PersonnelList = new DefinedPersonnelListViewModel();
                model.PersonnelList.Filter = new DefinedPersonnelListFilterViewModel();
                model.PersonnelList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
                model.PersonnelList.CurrentPage = 1;
                model.PersonnelList.PageSize = 10;

                model.PersonnelWhichIsNotIncludeList = new UndefinedPersonnelListViewModel();
                model.PersonnelWhichIsNotIncludeList.Filter = new UndefinedPersonnelListFilterViewModel();
                model.PersonnelWhichIsNotIncludeList.DataList = new Business.Models.PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
                model.PersonnelWhichIsNotIncludeList.CurrentPage = 1;
                model.PersonnelWhichIsNotIncludeList.PageSize = 10;
            }

            return View(model);
        }

        [NonAction]
        private PaginatedList<PersonnelCheckViewModel> GetAllEmployeeByProfileId(int profileId, string personnelName, string personnelLastName,int currentPage,int pageSize,string sortOn,string sortDirection)
        {
            //profile ait kullanıcı kayıtları listelenir
            var resultList = _profilePersonnelService.GetAllPersonnelPaginatedWithDetail(profileId, personnelName, personnelLastName, currentPage,pageSize, sortOn, sortDirection).Result.Data;
            if (resultList != null && resultList.Items != null)
            {
                var employeeCheckList = resultList.Items.Select(x => new PersonnelCheckViewModel()
                {
                    Checked = false,
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                }).ToList();
                PaginatedList<PersonnelCheckViewModel> result = new PaginatedList<PersonnelCheckViewModel>(employeeCheckList, resultList.TotalCount, resultList.CurrentPage, resultList.PageSize, resultList.SortOn, resultList.SortDirection);
                return result;
            }
            else
            {
                return new PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
            }
        }

        [NonAction]
        private PaginatedList<PersonnelCheckViewModel> GetAllEmployeeByProfileIdWhichIsNotIncluded(int profileId, string personnelName, string personnelLastName, int currentPage, int pageSize, string sortOn, string sortDirection)
        {
            //profile ait olmayan yetki kayıtları listelenir
            var resultList = _profilePersonnelService.GetAllPersonnelWhichIsNotIncludedPaginatedWithDetail(profileId, personnelName, personnelLastName, currentPage, pageSize, sortOn, sortDirection).Result.Data;
            if (resultList != null && resultList.Items != null)
            {
                var employeeCheckList = resultList.Items.Select(x => new PersonnelCheckViewModel()
                {
                    Checked = false,
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                }).ToList();
                PaginatedList<PersonnelCheckViewModel> result = new PaginatedList<PersonnelCheckViewModel>(employeeCheckList, resultList.TotalCount, resultList.CurrentPage, resultList.PageSize, resultList.SortOn, resultList.SortDirection);
                return result;
            }
            else
            {
                return new PaginatedList<PersonnelCheckViewModel>(new List<PersonnelCheckViewModel>(), 0, 1, 10, "", "");
            }
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
