using CarPark.Web.Business.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.Web.Filters
{
    public class AppAuthorizeFilter : Attribute, IAuthorizationFilter
    {

        // Custom AuthorizeAttribute in ASP.NET Core.
        // https://www.dotnetdepth.in/2018/06/authorizeattributemvccore.html
        // https://stackoverflow.com/questions/31464359/how-do-you-create-a-custom-authorizeattribute-in-asp-net-core
        private readonly string[] authCodeList;
        public AppAuthorizeFilter(params string[] authCodeList)
        {
            this.authCodeList = authCodeList;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            var controllerInfo = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (filterContext != null)
            {
                string controllerName = controllerInfo.ControllerName;
                if (!SessionHelper.IsAuthenticated)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login" }));
                }

                else if (SessionHelper.IsAuthenticated &&
                           !(SessionHelper.CurrentUser.AuthList != null
                           && SessionHelper.CurrentUser.AuthList.Count > 0
                           && SessionHelper.CurrentUser.AuthList.Where(r => authCodeList.Contains(r.Code)).Any())
                           )
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "NotAuthorized" }));
                }

            }

        }
    }
}
