using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.API.Business.Interfaces;
using CarPark.API.Common.Enums;
using CarPark.API.Filters;
using CarPark.API.Models;
using CarPark.API.Models.ProfileDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfileDetailController : ControllerBase
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

        private readonly IProfileDetailService _profileDetailService;
        public ProfileDetailController(IProfileDetailService profileDetailService)
        {
            _profileDetailService = profileDetailService;
        }

        [Route("GetAllAuthByCurrentUser")]
        [HttpGet]
      //[TokenAuthorizeFilter]
        public IActionResult GetAllAuthByCurrentUser([FromQuery] GetAllByCurrentUserRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<List<Data.Entity.Auth>>();
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
                return BadRequest(responseModel);
            }
            try
            {
                responseModel.Data = _profileDetailService.GetAllAuthByCurrentUser(requestModel.PersonnelId);
                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
                return Ok(responseModel);
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
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("GetAllAuthByProfileId")]
        [HttpGet]
        [TokenAuthorizeFilter]
        public IActionResult GetAllAuthByProfileId([FromQuery] GetAllAuthByProfileIdRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<List<Data.Entity.Auth>>() { DisplayLanguage = displayLanguage };
            if (requestModel.ProfileId <= 0)
            {
                responseModel.Status = ResultStatusCodeStatic.BadRequest;
                responseModel.Message = "Profile Id Must Be Entered";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = "Profile Id Must Be Entered";
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                return BadRequest(responseModel);
            }
            try
            {
                responseModel.Data = _profileDetailService.GetAllAuthByProfileId(requestModel.ProfileId);

                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
                return Ok(responseModel);
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
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("GetAllAuthByProfileIdWhichIsNotIncluded")]
        [HttpGet]
        [TokenAuthorizeFilter]
        public IActionResult GetAllAuthByProfileIdWhichIsNotIncluded([FromQuery] GetAllAuthByProfileIdWhichIsNotIncludedRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<List<Data.Entity.Auth>>() { DisplayLanguage = displayLanguage };
            if (requestModel.ProfileId <= 0)
            {
                responseModel.Status = ResultStatusCodeStatic.BadRequest;
                responseModel.Message = "Profile Id Must Be Entered";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = "Profile Id Must Be Entered";
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                return BadRequest(responseModel);
            }
            try
            {
                responseModel.Data = _profileDetailService.GetAllAuthByProfileIdWhichIsNotIncluded(requestModel.ProfileId);
                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
                return Ok(responseModel);
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
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("")]
        [HttpPost]
        [TokenAuthorizeFilter]
        public Return<Data.Entity.ProfileDetail> Add([FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<Data.Entity.ProfileDetail>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                var record = new Data.Entity.ProfileDetail();
                record.AuthId = requestModel.AuthId;
                record.ProfileId = requestModel.ProfileId;

                var dbResult = _profileDetailService.Add(record);

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
        public Return<int> DeleteByProfileIdAndAuthId([FromQuery] DeleteByProfileIdAndAuthIdRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<int>() { DisplayLanguage = displayLanguage };
            try
            {
                responseModel.Data = _profileDetailService.DeleteByProfileIdAndAuthId(requestModel.ProfileId, requestModel.AuthId);
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
