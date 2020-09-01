using CarPark.API.Common;
using CarPark.API.Common.Enums;
using CarPark.API.Models;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Filters
{
    public class TokenAuthorizeFilter : ActionFilterAttribute
    {
        private readonly string[] _authCodeList;

        public TokenAuthorizeFilter(params string[] authCodeList)
        {
            _authCodeList = authCodeList;
        }

        public override void OnActionExecuted(ActionExecutedContext actionContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

            string displayLanguage = "tr";

            if (actionContext.HttpContext.Request.Headers.ContainsKey("DisplayLanguage"))
            {
                displayLanguage = actionContext.HttpContext.Request.Headers["DisplayLanguage"].FirstOrDefault();

                displayLanguage = displayLanguage.ToLowerInvariant();
            }

            //istekteki -> Authorization bilgisinin alınması
            var authToken = "";
            if (actionContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                authToken = actionContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                authToken = authToken.Replace("Bearer ", "");
            }

            //token bilgisi yoksa
            if (string.IsNullOrEmpty(authToken))
            {
                BaseResponseModel responseModel = new BaseResponseModel();
                responseModel.Status = ResultStatusCodeStatic.Unauthorized;
                responseModel.Message = "Token paramater not found.";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = "Token paramater not found.";
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
               
                actionContext.Result = new UnauthorizedObjectResult(responseModel);
                return;
            }

            // jwt token imza kontrolu yapılır
            // jwt token geçerlilik kontrolu yapılır
            string jwtSecret = ConfigHelper.Jwt_Secret;
            bool isTokenSignatureError = false;
            bool isTokenParseError = false;
            bool isTokenExpireError = false;
            string userAuthCodeListAsString = "";
            string employeeId = "";
            try
            {
                var payload = new JwtBuilder()
                     .WithAlgorithm(new HMACSHA256Algorithm())
                     .WithSecret(jwtSecret)
                     .MustVerifySignature()
                     .Decode<IDictionary<string, object>>(authToken);

                var expiredDateAsSeconds = Convert.ToInt64(payload["expireAsUnixSeconds"].ToString());
                userAuthCodeListAsString = payload["authCodeListAsString"].ToString();
                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > expiredDateAsSeconds)
                {
                    isTokenExpireError = true;
                }

                // çalışan bilgisi HttpContext.User alanına atanıyor
                employeeId = payload["ID"].ToString();
                actionContext.HttpContext.User =
                    new System.Security.Claims.ClaimsPrincipal(new System.Security.Principal.GenericIdentity(employeeId));
            }
            catch (TokenExpiredException tokenExpiredException)
            {
                //Console.WriteLine("Token has expired");

            }
            catch (SignatureVerificationException signatureVerificationException)
            {
                //Console.WriteLine("Token has invalid signature");
                isTokenSignatureError = true;
            }
            catch (Exception ex)
            {
                isTokenParseError = true;
            }

            if (isTokenSignatureError)
            {
                BaseResponseModel responseModel = new BaseResponseModel();
                responseModel.Status = ResultStatusCodeStatic.Unauthorized;
                responseModel.Message = "Token signature error occured.";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = "Token signature error occured.";
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                actionContext.Result = new UnauthorizedObjectResult(responseModel);

                return;
            }
            else if (isTokenParseError)
            {
                BaseResponseModel responseModel = new BaseResponseModel();
                responseModel.Status = ResultStatusCodeStatic.Unauthorized;
                responseModel.Message = "Token parse error occured.";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = "Token parse error occured.";
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);

                actionContext.Result = new UnauthorizedObjectResult(responseModel);

                return;
            }
            else if (isTokenExpireError)
            {
                BaseResponseModel responseModel = new BaseResponseModel();
                responseModel.Status = ResultStatusCodeStatic.Unauthorized;
                responseModel.Message = "Token expire error occured.";
                responseModel.Success = false;
                ReturnError error = new ReturnError();
                error.Key = "500";
                error.Message = "Token expire error occured.";
                error.Code = 500;
                responseModel.Errors = new List<ReturnError>();
                responseModel.Errors.Add(error);
                actionContext.Result = new UnauthorizedObjectResult(responseModel);

                return;
            }

            // auth yetki kontolleri yapma
            // jwt token üzerinden auth code bilgileri alınır
            List<string> userAuthCodeList = userAuthCodeListAsString.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

            if (_authCodeList != null && _authCodeList.Count() > 0)
            {
                if (userAuthCodeList == null || userAuthCodeList.Count == 0 || (!userAuthCodeList.Where(r => _authCodeList.Contains(r)).Any()))
                {
                    BaseResponseModel responseModel = new BaseResponseModel();
                    responseModel.Status = ResultStatusCodeStatic.Unauthorized;
                    responseModel.Message = "User not authorized.";
                    responseModel.Success = false;
                    ReturnError error = new ReturnError();
                    error.Key = "500";
                    error.Message = "User not authorized.";
                    error.Code = 500;
                    responseModel.Errors = new List<ReturnError>();
                    responseModel.Errors.Add(error);
                    actionContext.Result = new UnauthorizedObjectResult(responseModel);

                    return;
                }

            }


        }
    }
}
