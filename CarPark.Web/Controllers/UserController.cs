using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IProfileDetailService _profileDetailService;
        private readonly IProfilePersonnelService _profilePersonnelService;

        public UserController(IAuthenticationService authenticationService, IProfileDetailService profileDetailService, IProfilePersonnelService profilePersonnelService)
        {
            _authenticationService = authenticationService;
            _profileDetailService = profileDetailService;
            _profilePersonnelService = profilePersonnelService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            //GetCaptchaImage();
            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            SessionLoginResult result = SessionHelper.Login(model.UserName, model.Password, _authenticationService, _profileDetailService, _profilePersonnelService);
            if (result.IsSuccess)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            else
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
        }

        // oturum açmadan erişilecek
        [AllowAnonymous]
        public ActionResult Logout()
        {
            // session logout olmalı
            string _UserSessionTrace_SessionTraceGuid = "UserSessionTrace_SessionTraceGuid elde edilemedi";
            if (SessionHelper.CurrentUser != null)
            {

            }
            SessionHelper.Logout();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult NotAuthorized()
        {
            return View();
        }

    }
}
