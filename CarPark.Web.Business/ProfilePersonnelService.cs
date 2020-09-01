using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Profile;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business
{
    public class ProfilePersonnelService : IProfilePersonnelService
    {
        IProfilePersonnelService _client;
        public ProfilePersonnelService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/ProfilePersonnel";

            _client = RestService.For<IProfilePersonnelService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }

        public async Task<Return<List<Profile>>> GetAllProfileByCurrentUser(int personnelId)
        {
            return await _client.GetAllProfileByCurrentUser(personnelId);
        }

    }
}
