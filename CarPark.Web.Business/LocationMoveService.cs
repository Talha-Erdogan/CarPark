using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.LocationMove;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business
{
    public class LocationMoveService : ILocationMoveService
    {
        ILocationMoveService _client;
        public LocationMoveService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/LocationMove";

            _client = RestService.For<ILocationMoveService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }

        public async Task<Return<PaginatedList<LocationMoveWithDetail>>> GetAllPaginatedWithDetailBySearchFilter(int currentPage, int pageSize, string sortOn, string sortDirection, string filterPlate, string filterLocationName)
        {
            return await _client.GetAllPaginatedWithDetailBySearchFilter(currentPage, pageSize, sortOn, sortDirection, filterPlate, filterLocationName);
        }

        public async Task<Return<LocationMove>> GetById(int id) => await _client.GetById(id);

        public async Task<Return<LocationMove>> Add(AddRequestModel addRequestModel)
        {
            Return<LocationMove> result = new Return<LocationMove>();
            return await _client.Add(addRequestModel);
        }

        public async Task<Return<LocationMove>> Edit(int id, AddRequestModel editRequestModel)
        {
            Return<LocationMove> result = new Return<LocationMove>();
            return await _client.Edit(editRequestModel.Id, editRequestModel);
        }

        //public async Task<Return<string>> UploadFile(ByteArrayPart imageFile) => await _client.UploadFile(imageFile); //resim eklemek istenilirse
    }
}
