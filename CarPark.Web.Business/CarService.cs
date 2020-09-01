using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Car;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business
{
    public class CarService : ICarService
    {
        ICarService _client;
        public CarService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/Car";

            _client = RestService.For<ICarService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }

        public async Task<Return<PaginatedList<Car>>> GetAllPaginatedWithDetailBySearchFilter(int currentPage, int pageSize, string sortOn, string sortDirection, string filterPlate)
        {
            return await _client.GetAllPaginatedWithDetailBySearchFilter(currentPage, pageSize, sortOn, sortDirection, filterPlate);
        }

        public async Task<Return<Car>> GetById(int id) => await _client.GetById(id);

        public async Task<Return<Car>> Add(AddRequestModel addRequestModel)
        {
            Return<Car> result = new Return<Car>();
            return await _client.Add(addRequestModel);
        }

        public async Task<Return<Car>> Edit(int id, AddRequestModel editRequestModel)
        {
            Return<Car> result = new Return<Car>();
            return await _client.Edit(editRequestModel.Id, editRequestModel);
        }

        //public async Task<Return<string>> UploadFile(ByteArrayPart imageFile) => await _client.UploadFile(imageFile); //resim eklemek istenilirse
    }
}
