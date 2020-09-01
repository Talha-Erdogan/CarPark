using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Location;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business
{
    public class LocationService : ILocationService
    {
        ILocationService _client;
        public LocationService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/Location";

            _client = RestService.For<ILocationService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }

        public async Task<Return<PaginatedList<Location>>> GetAllPaginatedWithDetailBySearchFilter(int currentPage, int pageSize, string sortOn, string sortDirection, string filterName)
        {
            return await _client.GetAllPaginatedWithDetailBySearchFilter(currentPage, pageSize, sortOn, sortDirection, filterName);
        }

        public async Task<Return<Location>> GetById(int id) => await _client.GetById(id);

        public async Task<Return<Location>> Add(AddRequestModel addRequestModel)
        {
            Return<Location> result = new Return<Location>();
            return await _client.Add(addRequestModel);
        }

        public async Task<Return<Location>> Edit(int id, AddRequestModel editRequestModel)
        {
            Return<Location> result = new Return<Location>();
            return await _client.Edit(editRequestModel.Id, editRequestModel);
        }

        //public async Task<Return<string>> UploadFile(ByteArrayPart imageFile) => await _client.UploadFile(imageFile); //resim eklemek istenilirse


    }
}
