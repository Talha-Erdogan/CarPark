using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.API.Business.Interfaces;
using CarPark.API.Common;
using CarPark.API.Common.Enums;
using CarPark.API.Models;
using CarPark.API.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
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

        private readonly IPersonnelService _personnelService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IProfileDetailService _profileDetailService;

        public AuthenticationController(IPersonnelService personnelService, IAuthenticationService authenticationService, IProfileDetailService profileDetailService)
        {
            _personnelService = personnelService;
            _authenticationService = authenticationService;
            _profileDetailService = profileDetailService;
        }

        [Route("Token")]
        [HttpPost]
        public IActionResult Token([FromBody] TokenRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            Return<TokenResponseModel> responseModel = new Return<TokenResponseModel>() { DisplayLanguage = displayLanguage };
            // parameter validations
            if (requestModel == null || string.IsNullOrEmpty(requestModel.UserName) || string.IsNullOrEmpty(requestModel.Password))
            {
                responseModel.Status = ResultStatusCodeStatic.BadRequest;
                responseModel.Message = "Parametreler boş olmamalıdır.";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "400";
                error.Message = "Parametreler boş olmamalıdır.";
                error.Code = 400;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                return BadRequest(responseModel);
            }

            var userLoginResponse = _authenticationService.Login(requestModel.UserName, requestModel.Password, displayLanguage);
            if (userLoginResponse.IsValid)
            {
                responseModel.Data = new TokenResponseModel();
                responseModel.Data.Id = userLoginResponse.Personnel.Id;
                responseModel.Data.TC = userLoginResponse.Personnel.TC;
                responseModel.Data.Name = userLoginResponse.Personnel.Name;
                responseModel.Data.LastName = userLoginResponse.Personnel.LastName;
                responseModel.Data.Phone = userLoginResponse.Personnel.Phone;
                responseModel.Data.Address = userLoginResponse.Personnel.Address;
                responseModel.Data.UserName = userLoginResponse.Personnel.UserName;
                responseModel.Data.Password = "-";
                responseModel.Data.TokenExpirePeriod = 60;

                // kullanıcının authcode bilgileri elde edilir
                var userAuthCodeListAsString = _profileDetailService.GetAllAuthCodeByPersonnelIdAsConcatenateString(userLoginResponse.Personnel.Id);

                //jwt token eklenmesi işlevi
                responseModel.Data.UserToken = TokenHelper.CreateToken(userLoginResponse.Personnel, userAuthCodeListAsString); //userToken.Token;

                responseModel.Status = ResultStatusCodeStatic.Success;
                responseModel.Message = "Success";
                responseModel.Success = true;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = ResultStatusCodeStatic.InternalServerError;
                responseModel.Message = userLoginResponse.ErrorMessage;
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = userLoginResponse.ErrorMessage;
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                responseModel.Data = null; //hata oluştugundan dolayı Data null olarak dönülür.
                return BadRequest(responseModel);
            }
        }


    }
}
