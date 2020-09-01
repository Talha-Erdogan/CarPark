using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Personnel;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business
{
    public class PersonnelService : IPersonnelService
    {
        IPersonnelService _client;
        public PersonnelService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/Personnel";

            _client = RestService.For<IPersonnelService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }

        public async Task<Return<PaginatedList<Personnel>>> GetAllPaginatedWithDetailBySearchFilter(int currentPage, int pageSize, string sortOn, string sortDirection, string filterCode, string filterName)
        {
            return await _client.GetAllPaginatedWithDetailBySearchFilter(currentPage, pageSize, sortOn, sortDirection, filterCode, filterName);
        }

        public async Task<Return<Personnel>> GetById(int id) => await _client.GetById(id);

        public async Task<Return<Personnel>> Add(AddRequestModel addRequestModel)
        {
            Return<Personnel> result = new Return<Personnel>();
            return await _client.Add(addRequestModel);
        }

        public async Task<Return<Personnel>> Edit(int id, AddRequestModel editRequestModel)
        {
            Return<Personnel> result = new Return<Personnel>();
            return await _client.Edit(editRequestModel.Id, editRequestModel);
        }

        //public async Task<Return<string>> UploadFile(ByteArrayPart imageFile) => await _client.UploadFile(imageFile); //resim eklemek istenilirse
    }
}
