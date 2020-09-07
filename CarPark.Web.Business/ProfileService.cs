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
    public class ProfileService : IProfileService
    {
        IProfileService _client;
        public ProfileService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/Profile";

            _client = RestService.For<IProfileService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }

        public async Task<Return<PaginatedList<Profile>>> GetAllPaginatedWithDetailBySearchFilter(int currentPage, int pageSize, string sortOn, string sortDirection, string filterCode, string filterName)
        {
            return await _client.GetAllPaginatedWithDetailBySearchFilter(currentPage, pageSize, sortOn, sortDirection, filterCode, filterName);
        }

        public async Task<Return<List<Profile>>>GetAll() => await _client.GetAll();

        public async Task<Return<Profile>> GetById(int id) => await _client.GetById(id);

        public async Task<Return<Profile>> Add(AddRequestModel addRequestModel)
        {
            Return<Profile> result = new Return<Profile>();
            return await _client.Add(addRequestModel);
        }

        public async Task<Return<Profile>> Edit(int id, AddRequestModel editRequestModel)
        {
            Return<Profile> result = new Return<Profile>();
            return await _client.Edit(editRequestModel.Id, editRequestModel);
        }

        //public async Task<Return<string>> UploadFile(ByteArrayPart imageFile) => await _client.UploadFile(imageFile); //resim eklemek istenilirse
        public async Task<Return<int>> Delete(int id) => await _client.Delete(id);

    }
}
