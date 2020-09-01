using CarPark.Web.Business.Common;
using CarPark.Web.Business.Interfaces;
using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Authentication;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        IAuthenticationService _client;
        public AuthenticationService()
        {
            var host = ConfigHelper.ApiUrl;
            var apiBaseUrl = host + "v1/Authentication";

            _client = RestService.For<IAuthenticationService>(new HttpClient(new AuthenticatedHttpClientHandler(SessionHelper.GetUserToken)) { BaseAddress = new Uri(apiBaseUrl) });
        }

        public async Task<Return<TokenResponseModel>> Token(TokenRequestModel requestModel)
        {
            Return<TokenResponseModel> result = new Return<TokenResponseModel>();
            return await _client.Token(requestModel);
        }


    }
}
