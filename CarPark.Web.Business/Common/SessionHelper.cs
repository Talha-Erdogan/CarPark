using CarPark.Web.Business.Common.Enums;
using CarPark.Web.Business.Common.Extensions;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Common
{
    public static class SessionHelper
    {

        // A Better Approach To Access HttpContext Outside A Controller In .Net Core 2.1
        // https://www.c-sharpcorner.com/article/a-better-approach-to-access-httpcontext-outside-a-controller-in-net-core-2-1/
        // Accessing HttpContext outside of framework components in ASP.NET Core
        // https://www.strathweb.com/2016/12/accessing-httpcontext-outside-of-framework-components-in-asp-net-core/


        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext CurrentHttpContext => _httpContextAccessor.HttpContext;


        public static SessionUser CurrentUser
        {
            get
            {

                if (CurrentHttpContext.Session.Get<SessionUser>("CarPark_CurrentUser") == null)
                {
                    return null;
                }
                else
                {
                    return CurrentHttpContext.Session.Get<SessionUser>("CarPark_CurrentUser");
                }
            }

            set
            {
                CurrentHttpContext.Session.Set<SessionUser>("CarPark_CurrentUser", value);
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                //if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.TID))
                if (CurrentUser != null && CurrentUser.Id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string CurrentLanguage
        {
            get
            {
                if (CurrentHttpContext.Session.GetString("CurrentLanguage") == null || string.IsNullOrEmpty(CurrentHttpContext.Session.GetString("CurrentLanguage").ToString()))
                {
                    string language = "tr-tr";
                    CurrentHttpContext.Session.SetString("CurrentLanguage", language);

                    return language;
                }
                else
                {
                    return CurrentHttpContext.Session.GetString("CurrentLanguage").ToString();
                }
            }

            set
            {
                CurrentHttpContext.Session.SetString("CurrentLanguage", value);
            }
        }

        public static string CurrentLanguageTwoChar
        {
            get
            {
                if (CurrentLanguage == "tr-tr")
                {
                    return "tr";
                }
                else if (CurrentLanguage == "en-us")
                {
                    return "en";
                }
                else
                {
                    return "tr";
                }
            }
        }

        public static void SetCurrentThreadCulture(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }

        // todo: Login metodu yeni projeye gore kodlanacak
        /*
        public static SessionLoginResult Login(string userRegNum, string userPasswordClear, bool isWindowsAuthentication, IUserService userService, IBshCasAuthService bshCasAuthService, ICustomAppUserService customAppUserService, IBshCasProfileService bshCasProfileService, IDepartmentService departmentService)
        */

        public static SessionLoginResult Login(string userName, string userPasswordClear,
            IAuthenticationService authenticationService, IProfileDetailService profileDetailService,
            IProfilePersonnelService profilePersonnelService)
        {

            var requestModel = new TokenRequestModel();
            requestModel.UserName = userName;
            requestModel.Password = userPasswordClear;

          
           var existUser = authenticationService.Token(requestModel).Result;
            if (existUser ==null)
            {
                return new SessionLoginResult(false, "kullanıcı adı hatalı");
            }
            if (existUser.Status != ResultStatusCodeStatic.Success)
            {
                return new SessionLoginResult(false, existUser.Message);
            }

            SessionUser currentUser = new SessionUser();
            currentUser.Id = existUser.Data.Id;
            currentUser.TC = existUser.Data.TC;
            currentUser.Name = existUser.Data.Name;
            currentUser.LastName = existUser.Data.LastName;
            currentUser.Phone = existUser.Data.Phone;
            currentUser.Address = existUser.Data.Address;
            currentUser.UserToken = existUser.Data.UserToken;


            //var apiAuthResponse = profileDetailService.GetAllAuthByCurrentUser(existUser.Data.UserToken, CurrentLanguageTwoChar, existUser.Data.Id);
            var apiAuthResponse = profileDetailService.GetAllAuthByCurrentUser(existUser.Data.Id).Result;
            if (apiAuthResponse.Status != ResultStatusCodeStatic.Success)
            {
                return new SessionLoginResult(false, apiAuthResponse.Message);
            }
            currentUser.AuthList = apiAuthResponse.Data;

            //var apiProfileResponse = profileEmployeeService.GetAllProfileByCurrentUser(existUser.Data.UserToken, CurrentLanguageTwoChar, existUser.Data.Id);
            var apiProfileResponse = profilePersonnelService.GetAllProfileByCurrentUser(existUser.Data.Id).Result;
            if (apiProfileResponse.Status != ResultStatusCodeStatic.Success)
            {
                return new SessionLoginResult(false, apiProfileResponse.Message);
            }
            currentUser.ProfileList = apiProfileResponse.Data;


            CurrentUser = currentUser;

            return new SessionLoginResult(true, null);
        }


        public static SessionLoginResult LoginTest(string userName, string userPasswordClear)
        {
            // check username and password

            //CurrentUser = new User() { REF = 1, TID = "testuser1", NAME = "Test User1", ROLEREF = 1, ROLE = "Admin" };

            return new SessionLoginResult(true, null);
        }


        public static bool Logout()
        {
            var currentLanguage = SessionHelper.CurrentLanguage;
            CurrentHttpContext.Session.Clear();
            foreach (var item in CurrentHttpContext.Session.Keys)
            {
                CurrentHttpContext.Session.Remove(item);
            }
            SessionHelper.CurrentLanguage = currentLanguage;
            return true;
        }

        public static void SetCurrentLanguageAndCurrentThreadCultureByLanguageTwoChar(string languageTwoChar)
        {
            if (string.IsNullOrEmpty(languageTwoChar))
            {
                languageTwoChar = "tr";
            }
            string newLanguage = languageTwoChar.Equals("tr") ? "tr-tr" : "en-us";
            SessionHelper.CurrentLanguage = newLanguage;
            SessionHelper.SetCurrentThreadCulture(newLanguage);
        }

        public static string GetCurrentRequestIpAddress()
        {
            string requestIpAddress = "";


            if (CurrentHttpContext.Connection.RemoteIpAddress != null)
            {
                requestIpAddress = CurrentHttpContext.Connection.RemoteIpAddress.ToString();
            }

            return requestIpAddress;
        }


        public static bool CheckAuthForCurrentUser(params string[] authCodeList)
        {
            bool result = false;

            if (CurrentUser == null || CurrentUser.AuthList == null || CurrentUser.AuthList.Count == 0)
            {
                return result;
            }

            if (CurrentUser.AuthList.Where(r => authCodeList.Contains(r.Code)).Any())
            {
                result = true;
            }

            // todo: yeni projeye gore kodlanacak

            return result;
        }




        public static void SetObject(string key, object value)
        {
            var str = JsonConvert.SerializeObject(value);
            CurrentHttpContext.Session.SetString(key, str);
        }


        public static T GetObject<T>(string key) where T : class
        {
            string objectString = CurrentHttpContext.Session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            else
            {
                T value = JsonConvert.DeserializeObject<T>(objectString);
                return value;
            }
        }


        public static async Task<string> GetUserToken()
        {
            return CurrentUser.UserToken;
        }

    }
}
