using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Auth;
using CarPark.Web.Business.Models.Profile;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business
{
    public class ProfileDetailService : IProfileDetailService
    {
        IProfileDetailService _client;
        public ProfileDetailService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/ProfileDetail";

            _client = RestService.For<IProfileDetailService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }


        public async Task<Return<List<Auth>>> GetAllAuthByCurrentUser(int personnelId)
        {
            return await _client.GetAllAuthByCurrentUser(personnelId);
        }

        
    }
}
