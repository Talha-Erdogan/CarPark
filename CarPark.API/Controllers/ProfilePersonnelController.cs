using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.API.Business.Interfaces;
using CarPark.API.Business.Models;
using CarPark.API.Common.Enums;
using CarPark.API.Filters;
using CarPark.API.Models;
using CarPark.API.Models.ProfilePersonnel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilePersonnelController : ControllerBase
    {

        #region Http Status Code
        //200-> OK
        //201-> Created(Tabloya veri eklendiğinde)
        //204-> No Content(veri silindikten sonra dönülebilir.)
        //400-> Bad Request(ekleme de ve ya guncelleme de modele uygun veri gönderilmemiş ise döndürülebilir.)
        //401-> Unauthorized 
        //404-> Not Found(istenilen veri yoksa kullanılabilir veya guncelleme yapılacak veri var ise ve parametre ile gönderilen Id 'ye ait veri yoksa kullanılabilir.)
        //500-> Internal Server Error(Genel sorun ve hata mesajı olarak kullanılabilir.)
        #endregion

        private readonly IProfilePersonnelService _profilePersonnelService;
        public ProfilePersonnelController(IProfilePersonnelService profilePersonnelService)
        {
            _profilePersonnelService = profilePersonnelService;
        }

        [Route("GetAllPersonnelPaginatedWithDetail")]
        [HttpGet]
        //[TokenAuthorizeFilter]
        public Return<PaginatedList<Data.Entity.Personnel>> GetAllPersonnelPaginatedWithDetail([FromQuery] GetAllPersonnelPaginatedRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<PaginatedList<Data.Entity.Personnel>>() { DisplayLanguage = displayLanguage };
            try
            {
                var searchFilter = new Business.Models.ProfilePersonnel.ProfilePersonnelSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_ProfileId = requestModel.ProfileId;
                searchFilter.Filter_Personnel_Name = requestModel.Personnel_Name;
                searchFilter.Filter_Personnel_LastName = requestModel.Personnel_LastName;
                responseModel.Data = _profilePersonnelService.GetAllPersonnelPaginatedWithDetailBySearchFilter(searchFilter);
                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                responseModel.Message = "An error occurred";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = ex.Message;
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
            }
            return responseModel;
        }

        [Route("GetAllPersonnelWhichIsNotIncludedPaginatedWithDetail")]
        [HttpGet]
        [TokenAuthorizeFilter]
        public Return<PaginatedList<Data.Entity.Personnel>> GetAllPersonnelWhichIsNotIncludedPaginatedWithDetail([FromQuery] GetAllPersonnelWhichIsNotIncludedPaginatedRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<PaginatedList<Data.Entity.Personnel>>() { DisplayLanguage = displayLanguage };
            try
            {
                var searchFilter = new Business.Models.ProfilePersonnel.ProfilePersonnelSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_ProfileId = requestModel.ProfileId;
                searchFilter.Filter_Personnel_Name = requestModel.Personnel_Name;
                searchFilter.Filter_Personnel_LastName = requestModel.Personnel_LastName;
                responseModel.Data = _profilePersonnelService.GetAllPersonnelWhichIsNotIncludedPaginatedWithDetailBySearchFilter(searchFilter);

                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                responseModel.Message = "An error occurred";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = ex.Message;
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
            }
            return responseModel;
        }

        [Route("GetAllProfileByCurrentUser")]
        [HttpGet]
        //[TokenAuthorizeFilter]
        public Return<List<Data.Entity.Profile>> GetAllProfileByCurrentUser([FromQuery] GetAllProfileByCurrentUserRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<List<Data.Entity.Profile>>();
            responseModel.DisplayLanguage = displayLanguage;

            if (requestModel.PersonnelId <= 0)
            {
                responseModel.Status = ResultStatusCodeStatic.BadRequest;
                responseModel.Message = "Personnel Id Must Be Entered";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = "Personnel Id Must Be Entered";
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                return responseModel;
            }
            try
            {
                responseModel.Data = _profilePersonnelService.GetAllProfileByCurrentUser(requestModel.PersonnelId);
                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                responseModel.Message = "An error occurred";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = ex.Message;
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
            }

            return responseModel;
        }

        [Route("")]
        [HttpPost]
        [TokenAuthorizeFilter]
        public Return<Data.Entity.ProfilePersonnel> Add([FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<Data.Entity.ProfilePersonnel>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                var record = new Data.Entity.ProfilePersonnel();
                record.PersonnelId = requestModel.PersonnelId;
                record.ProfileId = requestModel.ProfileId;
                var dbResult = _profilePersonnelService.Add(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.Status = ResultStatusCodeStatic.Success;
                    responseModel.Message = "Success";
                    responseModel.Success = true;
                }
                else
                {
                    responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                    responseModel.Message = "Could Not Be Saved";
                    responseModel.Success = false;
                    ReturnError error = new ReturnError();
                    error.Key = "500";
                    error.Message = "Could Not Be Saved";
                    error.Code = 500;
                    responseModel.Errors = new List<ReturnError>();
                    responseModel.Errors.Add(error);
                    responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                }
            }
            catch (Exception ex)
            {
                responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                responseModel.Message = "An error occurred";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = ex.Message;
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
            }
            return responseModel;
        }

        [Route("")]
        [HttpDelete]
        [TokenAuthorizeFilter]
        public Return<int> DeleteByProfileIdAndAuthId([FromQuery] DeleteByProfileIdAndPersonnelIdRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<int>() { DisplayLanguage = displayLanguage };
            try
            {
                responseModel.Data = _profilePersonnelService.DeleteByProfileIdAndPersonnelId(requestModel.ProfileId, requestModel.PersonnelId);
                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                responseModel.Message = "An error occurred";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = ex.Message;
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = 0; //hata oluştugundan dolayı Data 0 olarak dönülür.
            }
            return responseModel;
        }
    }
}
