using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.API.Business.Interfaces;
using CarPark.API.Business.Models;
using CarPark.API.Business.Models.Auth;
using CarPark.API.Common;
using CarPark.API.Common.Enums;
using CarPark.API.Common.Model;
using CarPark.API.Data.Entity;
using CarPark.API.Models;
using CarPark.API.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
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
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("")]
        [HttpGet]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
        public IActionResult GetAllPaginatedWithDetail([FromQuery] GetAllPaginatedRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<PaginatedList<Auth>>();
            responseModel.DisplayLanguage = displayLanguage;

            try
            {
                var searchFilter = new AuthSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Code = requestModel.Code;
                searchFilter.Filter_Name = requestModel.Name;
                responseModel.Data = _authService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);

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

        [Route("{Id}")]
        [HttpGet]
        //[TokenAuthorizeFilter]
        public IActionResult GetById(int id, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<Auth>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                responseModel.Data = _authService.GetById(id);
                if (responseModel.Data ==null)
                {
                    responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                    responseModel.Message = "Record Not Found.";
                    responseModel.Success = false;
                    ReturnError error = new ReturnError();
                    error.Key = "404";
                    error.Message = "Record Not Found.";
                    error.Code = 404;
                    responseModel.Errors = new List<ReturnError>();
                    responseModel.Errors.Add(error);
                    responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                    return NotFound(responseModel);
                }
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


        [HttpPost]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        public IActionResult Add([FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<Auth>();
            responseModel.DisplayLanguage = displayLanguage;

            //TokenModel tokenModel = TokenHelper.DecodeTokenFromRequestHeader(Request);
            //var employeeId = tokenModel.Id;


            //sil bunu
           var employeeId = 1;
            try
            {
                var record = new Auth();
                record.Code = requestModel.Code;
                record.Name = requestModel.Name;
                record.CreatedBy = employeeId;
                record.CreatedDateTime = DateTime.Now;

                record.IsDeleted = false;

                var dbResult = _authService.Add(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.Status = ResultStatusCodeStatic.Success;
                    responseModel.Message = "Success";
                    responseModel.Success = true;
                    return Ok(responseModel);
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
                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
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
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("{Id}")]
        [HttpPut]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        public IActionResult Edit(int id, [FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<Auth>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                var record = _authService.GetById(id);
                record.Code = requestModel.Code;
                record.Name = requestModel.Name;
                var dbResult = _authService.Update(record);
                if (dbResult > 0)
                {
                    responseModel.Data = record;
                    responseModel.Status = ResultStatusCodeStatic.Success;
                    responseModel.Message = "Success";
                    responseModel.Success = true;
                    return Ok(responseModel);
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
                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
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
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("{Id}")]
        [HttpDelete]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_DELETE)]
        public IActionResult Delete(int id, [FromHeader] string displayLanguage)
        {
            var responseModel = new Return<Auth>();
            responseModel.DisplayLanguage = displayLanguage;

            //user bilgilerinden filitre parametreleri eklenir.
            TokenModel tokenModel = TokenHelper.DecodeTokenFromRequestHeader(Request);
            var employeeId = tokenModel.Id;

            try
            {
                var record = _authService.GetById(id);
                record.IsDeleted = true;
                record.DeletedDateTime = DateTime.Now;
                record.DeletedBy = employeeId;
                var dbResult = _authService.Update(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // 'isDeleted= true' yapılan -> entity bilgisi geri gönderiliyor
                    responseModel.Status = ResultStatusCodeStatic.Success;
                    responseModel.Message = "Success";
                    responseModel.Success = true;
                    return Ok(responseModel);
                }
                else
                {
                    responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                    responseModel.Message = "Could Not Be Deleted";
                    responseModel.Success = false;
                    ReturnError error = new ReturnError();
                    error.Key = "500";
                    error.Message = "Could Not Be Deleted";
                    error.Code = 500;
                    responseModel.Errors = new List<ReturnError>();
                    responseModel.Errors.Add(error);
                    responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
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
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }
    }
}
