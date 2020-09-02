using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Personnel;
using CarPark.Web.Business.Models.Profile;
using CarPark.Web.Business.Models.ProfilePersonnel;
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

        public async Task<Return<PaginatedList<Personnel>>> GetAllPersonnelPaginatedWithDetail(int profileId, string personnel_Name, string personnel_LastName)
        {
            return await _client.GetAllPersonnelPaginatedWithDetail(profileId, personnel_Name, personnel_LastName);
        }

        public async Task<Return<PaginatedList<Personnel>>> GetAllPersonnelWhichIsNotIncludedPaginatedWithDetail(int profileId, string personnel_Name, string personnel_LastName)
        {
            return await _client.GetAllPersonnelWhichIsNotIncludedPaginatedWithDetail(profileId, personnel_Name, personnel_LastName);
        }

        public async Task<Return<ProfilePersonnel>> Add(Business.Models.ProfilePersonnel.AddRequestModel addRequestModel)
        {
            Return<ProfilePersonnel> result = new Return<ProfilePersonnel>();
            return await _client.Add(addRequestModel);
        }

        public async Task<Return<int>> Delete(int profileId, int personnelId) => await _client.Delete(profileId, personnelId);
    }
}